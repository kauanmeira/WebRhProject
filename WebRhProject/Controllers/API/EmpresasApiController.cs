using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebRhProject.Models;
using WebRhProject.Models.dto;
using WebRhProject.Services;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Controllers.API
{
    [ApiController]
    [Route("api/Empresas")]
    public class EmpresasApiController : ControllerBase
    {
        private readonly EmpresaService _empresaService;


        public EmpresasApiController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public ActionResult<List<Empresa>> Get()
        {
            var empresas = _empresaService.FindAll();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public ActionResult<Empresa> Get(int id)
        {
            var empresa = _empresaService.FindById(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return Ok(empresa);
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] Empresa empresa)
        {
            try
            {
                _empresaService.Insert(empresa);
                return CreatedAtAction(nameof(Get), new { id = empresa.Id }, empresa);
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
        public IActionResult Put(int id, [FromBody] EmpresaDTO empresaDTO)
        {
            try
            {
                var existingEmpresa = _empresaService.FindById(id);

                if (existingEmpresa == null)
                {
                    return NotFound();
                }

                existingEmpresa.Cnpj = empresaDTO.Cnpj;
                existingEmpresa.RazaoSocial = empresaDTO.RazaoSocial;
                existingEmpresa.NomeFantasia = empresaDTO.NomeFantasia;
                existingEmpresa.CEP = empresaDTO.CEP;
                existingEmpresa.Numero = empresaDTO.Numero;





                _empresaService.Update(existingEmpresa);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(empresaDTO);
        }
    }
}
