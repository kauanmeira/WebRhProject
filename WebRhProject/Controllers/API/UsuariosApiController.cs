using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebRhProject.Models;
using WebRhProject.Services;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Controllers.API
{
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuariosApiController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly ColaboradorService _colaboradorService;



        public UsuariosApiController(UsuarioService usuarioService, ColaboradorService colaboradorService)
        {
            _usuarioService = usuarioService;
            _colaboradorService = colaboradorService;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            var usuarios = _usuarioService.FindAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _usuarioService.FindById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioService.Insert(usuario);
                return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }
            try
            {
                _usuarioService.Update(usuario);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
