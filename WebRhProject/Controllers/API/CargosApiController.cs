using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebRhProject.Models;
using WebRhProject.Models.dto;
using WebRhProject.Services;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Controllers.API
{
    [ApiController]
    [Route("api/Cargos")]
    public class CargosApiController : ControllerBase
    {
        private readonly CargoService _cargoService;


        public CargosApiController(CargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet]
        public ActionResult<List<Cargo>> Get()
        {
            var cargos = _cargoService.FindAll();
            return Ok(cargos);
        }

        [HttpGet("{id}")]
        public ActionResult<Cargo> Get(int id)
        {
            var cargo = _cargoService.FindById(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return Ok(cargo);
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] Cargo cargo)
        {
            try
            {
                _cargoService.Insert(cargo);
                return CreatedAtAction(nameof(Get), new { id = cargo.Id }, cargo);
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
        public IActionResult Put(int id, [FromBody] CargoDTO cargoDTO)
        {
            try
            {
                var existingCargo = _cargoService.FindById(id);

                if (existingCargo == null)
                {
                    return NotFound();
                }

                // Atualize os dados do cargo com os valores do DTO
                existingCargo.Nome = cargoDTO.Nome;

                _cargoService.Update(existingCargo);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(cargoDTO);
        }


    }
}
