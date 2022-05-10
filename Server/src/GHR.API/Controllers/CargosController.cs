using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Contracts;
using GHR.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GHR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargosController : ControllerBase
    {
        private readonly ICargoService _cargoService;
        public CargosController(ICargoService cargoService)
        {
            _cargoService = cargoService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cargos = await _cargoService.GetAllCargosAsync();

                if (cargos == null) return NoContent();

                return Ok(cargos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar cargos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cargo = await _cargoService.GetCargoByIdAsync(id);

                if (cargo == null) return NoContent();

                return Ok(cargo);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar cargo por Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("{nome}/nome")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var cargos = await _cargoService.GetAllCargosByNomeCargoAsync(nome);

                if (cargos == null) return NoContent();

                return Ok(cargos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar cargos. Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(CargoDto model)
        {
            try
            {
                var cargo = await _cargoService.AddCargo(model);

                if (cargo == null) return NoContent();

                return Ok(cargo);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar adicionar cargo. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CargoDto model)
        {
            try
            {
                var cargo = await _cargoService.UpdateCargo(id, model);

                if (cargo == null) return NoContent();

                return Ok(cargo);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar alterar cargo. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cargo = await _cargoService.GetCargoByIdAsync(id);

                if (cargo == null) return NoContent();

                return await _cargoService.DeleteCargo(id)
                    ? Ok(new { message = "Excluído" })
                    : throw new Exception("Ocorreu ma falaha ao tentar deletar o cargo.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir cargo {id}. Erro: {ex.Message}");
            }
        }
    }
}
