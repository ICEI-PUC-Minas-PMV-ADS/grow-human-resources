using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Cargos;
using GHR.Application.Dtos.Cargos;

namespace GHR.API.Controllers.Cargos
{
    [Authorize]
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
                var cargos = await _cargoService.RecuperarCargosAsync();

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
                var cargo = await _cargoService.RecuperarCargoPorIdAsync(id);

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
                var cargos = await _cargoService.RecuperarCargosPorNomeCargoAsync(nome);

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
                var cargo = await _cargoService.CriarCargo(model);

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
                var cargo = await _cargoService.AlterarCargo(id, model);

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
                var cargo = await _cargoService.RecuperarCargoPorIdAsync(id);

                if (cargo == null) return NoContent();

                return await _cargoService.ExcluirCargo(id)
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
