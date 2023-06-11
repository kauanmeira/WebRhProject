using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public ColaboradoresController(ColaboradorService colaboradorService, CargoService cargoService)
        {
            _colaboradorService = colaboradorService;
            _cargoService = cargoService;
        }

        public IActionResult Index()
        {
            var list = _colaboradorService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var cargos = _cargoService.FindAll();
            var viewModel = new ColaboradorFormViewModel { Cargos = cargos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Colaborador colaborador)
        {
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

            List<Cargo> cargos = _cargoService.FindAll();
            ColaboradorFormViewModel viewModel = new ColaboradorFormViewModel { Colaborador = obj, Cargos = cargos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Colaborador colaborador)
        {
            if (id != colaborador.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _colaboradorService.Update(colaborador);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
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
    }
}
