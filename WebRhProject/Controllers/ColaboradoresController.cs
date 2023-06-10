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
    }
}
