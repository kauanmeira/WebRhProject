using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebRhProject.Models;
using WebRhProject.Models.dto;
using WebRhProject.Services;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Controllers.API
{
    [ApiController]
    [Route("api/Colaboradores")]
    public class ColaboradoresApiController : ControllerBase
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly CargoService _cargoService;
        private readonly EmpresaService _empresaService;

        public ColaboradoresApiController(ColaboradorService colaboradorService, CargoService cargoService, EmpresaService empresaService)
        {
            _colaboradorService = colaboradorService;
            _cargoService = cargoService;
            _empresaService = empresaService;
        }

        [HttpGet]
        public ActionResult<List<Colaborador>> Get()
        {
            var colaboradores = _colaboradorService.FindAll();
            return Ok(colaboradores);
        }

        [HttpGet("{id}")]
        public ActionResult<Colaborador> Get(int id)
        {
            var colaborador = _colaboradorService.FindById(id);
            if (colaborador == null)
            {
                return NotFound();
            }
            return Ok(colaborador);
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] Colaborador colaborador)
        {
            try
            {
                _colaboradorService.Insert(colaborador);
                return CreatedAtAction(nameof(Get), new { id = colaborador.Id }, colaborador);
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
        public IActionResult Put(int id, [FromBody] ColaboradorDTO colaboradorDTO)
        {
            try
            {
                var existingColaborador = _colaboradorService.FindById(id);

                if (existingColaborador == null)
                {
                    return NotFound();
                }

                existingColaborador.CPF = colaboradorDTO.Cpf;
                existingColaborador.Nome = colaboradorDTO.Nome;
                existingColaborador.Sobrenome = colaboradorDTO.Sobrenome;
                existingColaborador.SalarioBase = colaboradorDTO.SalarioBase;
                existingColaborador.DataNascimento = colaboradorDTO.DataNascimento;
                existingColaborador.DataAdmissao = colaboradorDTO.DataAdmissao;
                existingColaborador.Dependentes = colaboradorDTO.Dependentes;
                existingColaborador.Filhos = colaboradorDTO.Filhos;
                existingColaborador.CargoId = colaboradorDTO.CargoId;
                existingColaborador.EmpresaId = colaboradorDTO.EmpresaId;
                existingColaborador.CEP = colaboradorDTO.CEP;

                _colaboradorService.Update(existingColaborador);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(colaboradorDTO);
        }



    }
}
