using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Services;

namespace WebRhProject.Controllers
{
    public class CargosController : Controller
    {
        private readonly Contexto _context;
        private readonly CargoService _cargoService;

        public CargosController(Contexto context, CargoService cargoService)
        {
            _context = context;
            _cargoService = cargoService;
        }

        // GET: Cargos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cargo.ToListAsync());
        }

        // GET: Cargos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cargos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Cargo cargo)
        {
            bool nomeExists = await _context.Cargo.AnyAsync(c => c.Nome == cargo.Nome);

            if (nomeExists)
            {
                ModelState.AddModelError("Nome", "Já existe um cargo cadastrado com esse nome.");
                return View(cargo);
            }
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Cargos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        // POST: Cargos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.Id))
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
            return View(cargo);
        }
        // GET: Cargos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cargo == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .Include(e => e.Colaboradores)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cargo == null)
            {
                return NotFound();
            }

            if (cargo.Colaboradores.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir o cargo porque ela possui colaboradores vinculados.");
                return View(cargo);
            }

            return View(cargo);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargo
                .Include(e => e.Colaboradores)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (cargo == null)
            {
                return NotFound();
            }

            if (cargo.Colaboradores.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Não é possível excluir o cargo porque ela possui colaboradores vinculados.");
                ViewData["ShowErrorMessage"] = true;
                return View("Delete", cargo);
            }

            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CargoExists(int id)
        {
            return _context.Cargo.Any(e => e.Id == id);
        }
    }
}
