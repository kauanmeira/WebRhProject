using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Models.ViewModels;
using WebRhProject.Services;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Controllers

{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly ColaboradorService _colaboradorService;
        private readonly Contexto _context;


        public UsuariosController(UsuarioService usuarioService, ColaboradorService colaboradorService)
        {
            _usuarioService = usuarioService;
            _colaboradorService = colaboradorService;
           
        }

        public IActionResult Index()
        {
            var list = _usuarioService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var colaboradores = _colaboradorService.FindAllActive();
            var viewModel = new UsuarioFormViewModel { Colaboradores = colaboradores };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (_usuarioService.EmailExists(usuario.Email))
            {
                ModelState.AddModelError("Email", "Email já cadastrado");
                var colaboradores = _colaboradorService.FindAll();
                var viewModel = new UsuarioFormViewModel { Usuario = usuario, Colaboradores = colaboradores };
                return View(viewModel);
            }
            if (usuario.Senha != usuario.ConfirmarSenha)
            {
                ModelState.AddModelError("ConfirmarSenha", "A senha e a confirmação de senha não correspondem");
                var colaboradores = _colaboradorService.FindAll();
                var viewModel = new UsuarioFormViewModel { Usuario = usuario, Colaboradores = colaboradores };
                return View(viewModel);
            }


            _usuarioService.Insert(usuario);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _usuarioService.FindById(id.Value);
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
            _usuarioService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _usuarioService.FindById(id.Value);
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

            var obj = _usuarioService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Colaborador> colaboradores = _colaboradorService.FindAll();
            UsuarioFormViewModel viewModel = new UsuarioFormViewModel { Usuario = obj, Colaboradores = colaboradores };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _usuarioService.Update(usuario);
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (IsValidLogin(login.Email, login.Senha))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenciais inválidas. Verifique seu email e senha.");
                }
            }

            return View(login);
        }

        private bool IsValidLogin(string email, string senha)
        {
            var usuario = _usuarioService.FindByEmailAndPassword(email, senha);

            if (usuario != null)
            {
                // Credenciais válidas
                return true;
            }

            // Credenciais inválidas
            return false;
        }
    }

}

