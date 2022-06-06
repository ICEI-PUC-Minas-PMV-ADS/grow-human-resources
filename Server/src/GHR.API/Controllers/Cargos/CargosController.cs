using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Cargos;
using GHR.Application.Dtos.Cargos;
using GHR.Persistence.Models;
using GHR.API.Extensions;

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
        public async Task<IActionResult> RecuperarCargos([FromQuery]PaginaParametros paginaParametros)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var cargos = await _cargoService.RecuperarCargosAsync(empresaId, paginaParametros);

                if (cargos == null) return NoContent();

                Response.CriarPaginacao(cargos.PaginaAtual, 
                    cargos.TamanhoDaPagina, 
                    cargos.ContadorTotal, 
                    cargos.TotalDePaginas);

                return Ok(cargos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar cargos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarCargosPorId(int id)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var cargo = await _cargoService.RecuperarCargoPorIdAsync(id, empresaId);

                if (cargo == null) return NoContent();

                return Ok(cargo);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar cargo por Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("{DepartamentoId}/departamentoId")]
        public async Task<IActionResult> RecuperarCargosPorDepartamentoId(int departamentoId)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var cargo = await _cargoService
                .RecuperarCargosPorDepartamentoIdAsync(departamentoId, empresaId);

                if (cargo == null) return NoContent();

                return Ok(cargo);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar cargo por departamentoId. Erro: {ex.Message}");
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
        public async Task<IActionResult> SalvarCargo(int id, CargoDto model)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var cargo = await _cargoService.AlterarCargo(id, empresaId, model);

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
        public async Task<IActionResult> ExcluirCargo(int id)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var cargo = await _cargoService.RecuperarCargoPorIdAsync(id, empresaId);

                if (cargo == null) return NoContent();

                return await _cargoService.ExcluirCargo(id, empresaId)
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
