using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebRhProject.Models;
using WebRhProject.Models.ViewModels;
using WebRhProject.Services;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Controllers
{
    public class ColaboradoresController : Controller
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly CargoService _cargoService;
        private readonly EmpresaService _empresaService;
        private readonly ILogger<ColaboradoresController> _logger;


        public ColaboradoresController(ColaboradorService colaboradorService, CargoService cargoService, EmpresaService empresaService, ILogger<ColaboradoresController> logger)
        {
            _colaboradorService = colaboradorService;
            _cargoService = cargoService;
            _empresaService = empresaService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list = _colaboradorService.FindAllActive();
            return View(list);
        }


        public IActionResult Create()
        {
            var cargos = _cargoService.FindAll();
            var empresas = _empresaService.FindAll();
            var viewModel = new ColaboradorFormViewModel { Cargos = cargos, Empresas = empresas };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Colaborador colaborador)
        {
            if (string.IsNullOrEmpty(colaborador.CPF))
            {
                ModelState.AddModelError(nameof(Colaborador.CPF), "O campo CPF é obrigatório");
            }
            if (colaborador.SalarioBase == null || colaborador.SalarioBase == 0.0)
            {
                ModelState.AddModelError(nameof(Colaborador.SalarioBase), "O campo Salário Base é obrigatório");
            }

            bool colaboradorExists = _colaboradorService.Exists(colaborador.CPF);
            if (colaboradorExists)
            {
                ModelState.AddModelError(string.Empty, "Já existe um colaborador com o mesmo CPF");
                var cargos = _cargoService.FindAll();
                var empresas = _empresaService.FindAll();
                var viewModel = new ColaboradorFormViewModel { Cargos = cargos, Empresas = empresas, Colaborador = colaborador };
                return View(viewModel);
            }

            string cep = colaborador.CEP;
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var addressData = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                    colaborador.Logradouro = addressData.logradouro;
                    colaborador.Bairro = addressData.bairro;
                    colaborador.Cidade = addressData.localidade;
                    colaborador.Estado = addressData.uf;
                }
            }

                _colaboradorService.Insert(colaborador);
                return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _colaboradorService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Verificar se o colaborador está vinculado a uma empresa
            bool hasCompany = _colaboradorService.HasCompanyEmpresa(id) || _colaboradorService.HasCompanyCargo(id);
            if (hasCompany)
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir o colaborador porque ele está vinculado a uma empresa e/ou cargo.");
                var obj = _colaboradorService.FindById(id);
                return View("Delete", obj);
            }

            _colaboradorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _colaboradorService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _colaboradorService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var cargos = _cargoService.FindAll();
            var empresas = _empresaService.FindAll();
            var viewModel = new ColaboradorFormViewModel { Colaborador = obj, Cargos = cargos, Empresas = empresas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Colaborador colaborador)
        {
            if (string.IsNullOrEmpty(colaborador.CPF))
            {
                ModelState.AddModelError(nameof(Colaborador.CPF), "O campo CPF é obrigatório");
            }
            if (colaborador.SalarioBase == null || colaborador.SalarioBase == 0.0)
            {
                ModelState.AddModelError(nameof(Colaborador.SalarioBase), "O campo Salário Base é obrigatório");
            }

            bool colaboradorExists = _colaboradorService.Exists(colaborador.CPF);
            if (colaboradorExists)
            {
                ModelState.AddModelError(string.Empty, "Já existe um colaborador com o mesmo CPF");
                var cargos = _cargoService.FindAll();
                var empresas = _empresaService.FindAll();
                var viewModel = new ColaboradorFormViewModel { Cargos = cargos, Empresas = empresas, Colaborador = colaborador };
                return View(viewModel);
            }

            string cep = colaborador.CEP;
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var addressData = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                    colaborador.Logradouro = addressData.logradouro;
                    colaborador.Bairro = addressData.bairro;
                    colaborador.Cidade = addressData.localidade;
                    colaborador.Estado = addressData.uf;
                }
            }

            _colaboradorService.Update(colaborador);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        public IActionResult SelectColaborador()
        {
            var viewModel = new DemissaoViewModel
            {
                Colaboradores = _colaboradorService.FindAllActive()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectColaborador(DemissaoViewModel viewModel)
        {
            if (viewModel.ColaboradorId == 0)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            return RedirectToAction(nameof(Demissao), new { id = viewModel.ColaboradorId });
        }

        public IActionResult Demissao(int id)
        {
            var obj = _colaboradorService.FindById(id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var viewModel = new DemissaoViewModel
            {
                ColaboradorId = obj.Id,
                Colaboradores = _colaboradorService.FindAllActive(),
                // Passe as informações do colaborador para a view
                Colaborador = obj
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Demissao(DemissaoViewModel viewModel)
        {
            if (viewModel.ColaboradorId == 0)
            {
                ModelState.AddModelError(string.Empty, "Nenhum colaborador foi selecionado.");
                viewModel.Colaboradores = _colaboradorService.FindAllActive(); // Alterado para buscar apenas colaboradores ativos
                return View(viewModel);
            }

            int colaboradorId = viewModel.ColaboradorId;
            _colaboradorService.Demitir(colaboradorId);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListarColaboradoresAtivos()
        {
            List<Colaborador> colaboradores = _colaboradorService.FindAllActive();

            foreach (var colaborador in colaboradores)
            {
                _logger.LogInformation($"ID: {colaborador.Id}, Nome: {colaborador.Nome}, Departamento: {colaborador.Cargo?.Nome}");
            }

            return View(colaboradores);
        }

    }
}