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
    public class HoleritesController : Controller
    {
        private readonly Contexto _context;

        public HoleritesController(Contexto context)
        {
            _context = context;
        }

        // GET: Holerites
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Holerite.Include(h => h.Colaborador);
            return View(await contexto.ToListAsync());
        }

        // GET: Holerites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Holerite == null)
            {
                return NotFound();
            }

            var holerite = await _context.Holerite
                .Include(h => h.Colaborador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (holerite == null)
            {
                return NotFound();
            }

            return View(holerite);
        }

        public IActionResult Create()
        {
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColaboradorId,Mes,SalarioBruto,DescInss,DescIrrf,HorasNormais,SalarioLiquido,Dependentes,Tipo")] Holerite holerite)
        {
            if (ModelState.IsValid)
            {
                // Obter o colaborador selecionado
                var colaborador = await _context.Colaborador.FindAsync(holerite.ColaboradorId);

                // Preencher o salário bruto com o salário base do colaborador
                holerite.SalarioBruto = colaborador.SalarioBase;

                // Calcular o desconto do INSS
                holerite.CalcularDescontoInss();

                // Calcular o desconto do IRRF
                holerite.CalcularDescontoIrrf();

                // Preencher o salário líquido
                holerite.SalarioLiquido = holerite.SalarioBruto - holerite.DescInss - holerite.DescIrrf;

                _context.Add(holerite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "Id", holerite.ColaboradorId);
            return View(holerite);
        }



        // GET: Holerites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Holerite == null)
            {
                return NotFound();
            }

            var holerite = await _context.Holerite.FindAsync(id);
            if (holerite == null)
            {
                return NotFound();
            }
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "Id", holerite.ColaboradorId);
            return View(holerite);
        }

        // POST: Holerites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ColaboradorId,Mes,SalarioBruto,DescInss,DescIrrf,HorasNormais,SalarioLiquido,Dependentes,Tipo")] Holerite holerite)
        {
            if (id != holerite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(holerite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoleriteExists(holerite.Id))
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
            ViewData["ColaboradorId"] = new SelectList(_context.Colaborador, "Id", "Id", holerite.ColaboradorId);
            return View(holerite);
        }

        // GET: Holerites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Holerite == null)
            {
                return NotFound();
            }

            var holerite = await _context.Holerite
                .Include(h => h.Colaborador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (holerite == null)
            {
                return NotFound();
            }

            return View(holerite);
        }

        // POST: Holerites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Holerite == null)
            {
                return Problem("Entity set 'Contexto.Holerite'  is null.");
            }
            var holerite = await _context.Holerite.FindAsync(id);
            if (holerite != null)
            {
                _context.Holerite.Remove(holerite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoleriteExists(int id)
        {
          return (_context.Holerite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
