using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Services;
using System.Net.Http;

namespace WebRhProject.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly Contexto _context;
        private readonly EmpresaService _empresaService;

        public EmpresasController(Contexto context, EmpresaService empresaService)
        {
            _context = context;
            _empresaService = empresaService;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            return _context.Empresa != null ?
                View(await _context.Empresa.ToListAsync()) :
                Problem("Entity set 'Contexto.Empresa'  is null.");
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresa == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cnpj,RazaoSocial,NomeFantasia, CEP")] Empresa empresa)
        {
            bool cnpjExists = await _context.Empresa.AnyAsync(u => u.Cnpj == empresa.Cnpj);

            if (cnpjExists)
            {
                ModelState.AddModelError("Cnpj", "Já existe uma empresa cadastrada com esse CNPJ.");
                return View(empresa);
            }

            bool razaoExists = await _context.Empresa.AnyAsync(u => u.RazaoSocial == empresa.RazaoSocial);

            if (razaoExists)
            {
                ModelState.AddModelError("RazaoSocial", "Já existe uma empresa cadastrada com essa razão social.");
                return View(empresa);
            }

            bool nomeExists = await _context.Empresa.AnyAsync(u => u.NomeFantasia == empresa.NomeFantasia);

            if (nomeExists)
            {
                ModelState.AddModelError("NomeFantasia", "Já existe uma empresa cadastrada com esse nome fantasia.");
                return View(empresa);
            }

            string cep = empresa.CEP;
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var addressData = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                    empresa.Logradouro = addressData.logradouro;
                    empresa.Bairro = addressData.bairro;
                    empresa.Cidade = addressData.localidade;
                    empresa.Estado = addressData.uf;
                }
            }

            _empresaService.Insert(empresa); 
            return RedirectToAction(nameof(Index));
        }


            // GET: Empresas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Empresa == null)
        {
            return NotFound();
        }

        var empresa = await _context.Empresa.FindAsync(id);
        if (empresa == null)
        {
            return NotFound();
        }
    
        return View(empresa);
    }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Empresa empresa)
            {
                if (id != empresa.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        string cep = empresa.CEP;
                        string url = $"https://viacep.com.br/ws/{cep}/json/";

                        using (HttpClient client = new HttpClient())
                        {
                            var response = await client.GetAsync(url);
                            if (response.IsSuccessStatusCode)
                            {
                                var jsonResult = await response.Content.ReadAsStringAsync();
                                var addressData = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                                empresa.Logradouro = addressData.logradouro;
                                empresa.Bairro = addressData.bairro;
                                empresa.Cidade = addressData.localidade;
                                empresa.Estado = addressData.uf;
                            }
                        }

                        
                        ModelState.Remove("CEP");

                        _context.Update(empresa);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmpresaExists(empresa.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(empresa);
            }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresa == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresa
                .Include(e => e.Colaboradores)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (empresa == null)
            {
                return NotFound();
            }

            if (empresa.Colaboradores.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir a empresa porque ela possui colaboradores vinculados.");
                return View(empresa);
            }

            return View(empresa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresa
                .Include(e => e.Colaboradores)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empresa == null)
            {
                return NotFound();
            }

            if (empresa.Colaboradores.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir a empresa porque ela possui colaboradores vinculados.");
                ViewData["ShowErrorMessage"] = true;
                return View("Delete", empresa);
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return (_context.Empresa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task ObterInformacoesEndereco(Empresa empresa)
        {
            string cep = empresa.CEP;
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    dynamic addressData = JsonConvert.DeserializeObject(jsonResult);

                    empresa.Logradouro = addressData.logradouro;
                    empresa.Bairro = addressData.bairro;
                    empresa.Cidade = addressData.localidade;
                    empresa.Estado = addressData.uf;
                }
            }
        }

    }
}
