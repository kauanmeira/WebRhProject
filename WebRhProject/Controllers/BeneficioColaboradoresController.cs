using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRhProject.Data;
using WebRhProject.Models;

namespace WebRhProject.Controllers
{
    public class BeneficioColaboradoresController : Controller
    {
        private readonly Contexto _context;

        public BeneficioColaboradoresController(Contexto context)
        {
            _context = context;
        }

        // GET: BeneficioColaboradores
        public async Task<IActionResult> Index()
        {
            var contexto = _context.BeneficioColaborador.Include(b => b.Beneficio).Include(b => b.Colaborador);
            return View(await contexto.ToListAsync());
        }

        // GET: BeneficioColaboradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BeneficioColaborador == null)
            {
                return NotFound();
            }

            var beneficioColaborador = await _context.BeneficioColaborador
                .Include(b => b.Beneficio)
                .Include(b => b.Colaborador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficioColaborador == null)
            {
                return NotFound();
            }

            return View(beneficioColaborador);
        }

        // GET: BeneficioColaboradores/Create
        public IActionResult Create()
        {
            ViewData["BeneficioId"] = new SelectList(_context.Beneficio, "Id", "Id");
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "CEP");
            return View();
        }

        // POST: BeneficioColaboradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColaboradorId,BeneficioId")] BeneficioColaborador beneficioColaborador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beneficioColaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficioId"] = new SelectList(_context.Beneficio, "Id", "Id", beneficioColaborador.BeneficioId);
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "CEP", beneficioColaborador.ColaboradorId);
            return View(beneficioColaborador);
        }

        // GET: BeneficioColaboradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BeneficioColaborador == null)
            {
                return NotFound();
            }

            var beneficioColaborador = await _context.BeneficioColaborador.FindAsync(id);
            if (beneficioColaborador == null)
            {
                return NotFound();
            }
            ViewData["BeneficioId"] = new SelectList(_context.Beneficio, "Id", "Id", beneficioColaborador.BeneficioId);
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "CEP", beneficioColaborador.ColaboradorId);
            return View(beneficioColaborador);
        }

        // POST: BeneficioColaboradores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ColaboradorId,BeneficioId")] BeneficioColaborador beneficioColaborador)
        {
            if (id != beneficioColaborador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beneficioColaborador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficioColaboradorExists(beneficioColaborador.Id))
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
            ViewData["BeneficioId"] = new SelectList(_context.Beneficio, "Id", "Id", beneficioColaborador.BeneficioId);
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "CEP", beneficioColaborador.ColaboradorId);
            return View(beneficioColaborador);
        }

        // GET: BeneficioColaboradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BeneficioColaborador == null)
            {
                return NotFound();
            }

            var beneficioColaborador = await _context.BeneficioColaborador
                .Include(b => b.Beneficio)
                .Include(b => b.Colaborador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficioColaborador == null)
            {
                return NotFound();
            }

            return View(beneficioColaborador);
        }

        // POST: BeneficioColaboradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BeneficioColaborador == null)
            {
                return Problem("Entity set 'Contexto.BeneficioColaborador'  is null.");
            }
            var beneficioColaborador = await _context.BeneficioColaborador.FindAsync(id);
            if (beneficioColaborador != null)
            {
                _context.BeneficioColaborador.Remove(beneficioColaborador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficioColaboradorExists(int id)
        {
          return (_context.BeneficioColaborador?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
