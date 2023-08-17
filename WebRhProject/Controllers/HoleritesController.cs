using Microsoft.AspNetCore.Mvc;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Models.ViewModels;
using WebRhProject.Services;
using Microsoft.Extensions.Logging;


namespace WebRhProject.Controllers
{
    public class HoleritesController : Controller
    {
        private readonly HoleriteService _holeriteService;
        private readonly ColaboradorService _colaboradorService;
        private readonly Contexto _context;

        private readonly ILogger<HoleritesController> _logger;

        public HoleritesController(HoleriteService holeriteService, ColaboradorService colaboradorService, Contexto context, ILogger<HoleritesController> logger)
        {
            _holeriteService = holeriteService;
            _colaboradorService = colaboradorService;
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var holerites = _holeriteService.GetAllHolerites();
            return View(holerites);
        }

        public IActionResult Create()
        {
            var colaboradores = _colaboradorService.FindAllActive();
            var viewModel = new HoleriteFormViewModel { Colaboradores = colaboradores };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HoleriteFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Colaboradores = _colaboradorService.FindAllActive();
                return View(viewModel);
            }

            var colaborador = _colaboradorService.FindById(viewModel.Holerite.ColaboradorId);

            var holerite = new Holerite
            {
                ColaboradorId = viewModel.Holerite.ColaboradorId,
                MesAno = viewModel.Holerite.MesAno,
                HorasNormais = viewModel.Holerite.HorasNormais,
                DependentesHolerite = viewModel.Holerite.DependentesHolerite,
                Tipo = viewModel.Holerite.Tipo,
                SalarioBruto = colaborador.SalarioBase
            };

            // Realize os cálculos para os valores de desconto e salário líquido
            holerite.CalcularHolerite();

            _holeriteService.InsertHolerite(holerite);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int id)
        {
            var holerite = _holeriteService.GetHoleriteById(id);
            if (holerite == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Holerite not found" });
            }

            return View(holerite);
        }

        public IActionResult Edit(int id)
        {
            var holerite = _holeriteService.GetHoleriteById(id);
            if (holerite == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Holerite not found" });
            }

            var colaboradores = _colaboradorService.FindAllActive();
            var viewModel = new HoleriteFormViewModel { Colaboradores = colaboradores, Holerite = holerite };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Holerite holerite)
        {
            if (id != holerite.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (!ModelState.IsValid)
            {
                var colaboradores = _colaboradorService.FindAllActive();
                var viewModel = new HoleriteFormViewModel { Colaboradores = colaboradores, Holerite = holerite };
                return View(viewModel);
            }

            holerite.CalcularHolerite();

            _holeriteService.UpdateHolerite(holerite);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetSalarioBase(int colaboradorId)
        {
            try
            {
                var colaborador = _colaboradorService.FindById(colaboradorId);
                return Json(new { success = true, salarioBase = colaborador.SalarioBase });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public IActionResult GetSalarioBaseAndDescontos(int colaboradorId)
        {
            try
            {
                var colaborador = _colaboradorService.FindById(colaboradorId);
                double descontoINSS = CalculaDescontoINSS(colaborador.SalarioBase);
                double descontoIRRF = CalculaDescontoIRRF(colaborador.SalarioBase, descontoINSS);

                return Json(new
                {
                    success = true,
                    salarioBase = colaborador.SalarioBase,
                    descontoINSS,
                    descontoIRRF,
                    salarioLiquido = colaborador.SalarioBase - descontoINSS - descontoIRRF
                });
            }
            catch
            {
                return Json(new { success = false });
            }
        }


        private double CalculaDescontoINSS(double salarioBase)
        {
            double descontoINSS = 0.0;

            if (salarioBase <= 1100.00)
            {
                descontoINSS = salarioBase * 0.075; // Alíquota de 7.5% para salários até R$ 1.100,00
            }
            else if (salarioBase <= 2203.48)
            {
                descontoINSS = salarioBase * 0.09; // Alíquota de 9% para salários de R$ 1.100,01 até R$ 2.203,48
            }
            else if (salarioBase <= 3305.22)
            {
                descontoINSS = salarioBase * 0.12; // Alíquota de 12% para salários de R$ 2.203,49 até R$ 3.305,22
            }
            else if (salarioBase <= 6433.57)
            {
                descontoINSS = salarioBase * 0.14; // Alíquota de 14% para salários de R$ 3.305,23 até R$ 6.433,57
            }
            else
            {
                descontoINSS = 751.99; // Valor fixo para salários acima de R$ 6.433,57 (teto máximo)
            }

            return descontoINSS;
        }

        private double CalculaDescontoIRRF(double salarioBase, double descontoINSS)
        {
            double descontoIRRF = 0.0;
            double salarioBaseAjustado = salarioBase - descontoINSS;

            if (salarioBaseAjustado <= 1903.98)
            {
                descontoIRRF = 0; // Isento de imposto de renda para salários até R$ 1.903,98
            }
            else if (salarioBaseAjustado <= 2826.65)
            {
                descontoIRRF = (salarioBaseAjustado * 0.075) - 142.80; // Alíquota de 7.5% para salários de R$ 1.903,99 até R$ 2.826,65
            }
            else if (salarioBaseAjustado <= 3751.05)
            {
                descontoIRRF = (salarioBaseAjustado * 0.15) - 354.80; // Alíquota de 15% para salários de R$ 2.826,66 até R$ 3.751,05
            }
            else if (salarioBaseAjustado <= 4664.68)
            {
                descontoIRRF = (salarioBaseAjustado * 0.225) - 636.13; // Alíquota de 22.5% para salários de R$ 3.751,06 até R$ 4.664,68
            }
            else
            {
                descontoIRRF = (salarioBaseAjustado * 0.275) - 869.36; // Alíquota de 27.5% para salários acima de R$ 4.664,68
            }

            return descontoIRRF;
        }
        public IActionResult GetDependentes(int colaboradorId)
        {
            var colaborador = _colaboradorService.FindById(colaboradorId);
            if (colaborador != null)
            {
                // Adicione logs para verificar o valor da propriedade "Dependentes"
                _logger.LogInformation("Colaborador ID: {Id}, Dependentes: {Dependentes}", colaborador.Id, colaborador.Dependentes);

                return Json(colaborador.Dependentes);
            }

            // Adicione logs para identificar quando o colaborador é nulo
            _logger.LogWarning("Colaborador não encontrado para o ID: {Id}", colaboradorId);

            return Json(null);
        }




        public IActionResult Delete(int id)
        {
            var holerite = _holeriteService.GetHoleriteById(id);
            if (holerite == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Holerite not found" });
            }

            return View(holerite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, bool confirmDelete)
        {
            if (!confirmDelete)
            {
                return RedirectToAction(nameof(Index));
            }

            _holeriteService.DeleteHolerite(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message };
            return View(viewModel);
        }
    }
}