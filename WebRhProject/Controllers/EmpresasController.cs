using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRhProject.Data;
using WebRhProject.Data.Migrations;
using WebRhProject.Models;
using WebRhProject.Services;

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Empresas/Create
        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cnpj,RazaoSocial,NomeFantasia")] Empresa empresa)
        {
            bool cnpjExists = await _context.Empresa.AnyAsync(u => u.Cnpj == empresa.Cnpj);

            if (cnpjExists)
            {
                ModelState.AddModelError("Cnpj", "Já existe uma empresa cadastrada com esse cnpj.");
                return View(empresa);
            }
            bool razaoExists = await _context.Empresa.AnyAsync(u => u.RazaoSocial == empresa.RazaoSocial);

            if (razaoExists)
            {
                ModelState.AddModelError("Razão Social", "Já existe uma empresa cadastrada com essa razão social");
                return View(empresa);
            }
            bool nomeExists = await _context.Empresa.AnyAsync(u => u.NomeFantasia == empresa.NomeFantasia);

            if (nomeExists)
            {
                ModelState.AddModelError("Nome Fantasia", "Já existe uma empresa cadastrada com esse nome fantasia");
                return View(empresa);
            }
            if (ModelState.IsValid)
            {
                _context.Empresa.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Se houver erros de validação, exiba as mensagens de erro
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return View(empresa);
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

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cnpj,RazaoSocial,NomeFantasia")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empresa == null)
            {
                return Problem("Entity set 'Contexto.Empresa'  is null.");
            }
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresa.Remove(empresa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return (_context.Empresa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
