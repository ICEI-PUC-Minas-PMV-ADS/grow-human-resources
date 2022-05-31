using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Application.Dtos.Funcionarios;
using GHR.Persistence.Models;
using GHR.API.Extensions;

namespace GHR.API.Controllers.Funcionarios
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosMetasController : ControllerBase
    {
        private readonly IFuncionarioMetaService _funcionarioMetaService;

        public FuncionariosMetasController(IFuncionarioMetaService funcionarioMetaService)
        {
            _funcionarioMetaService = funcionarioMetaService;
        }

        [HttpGet("{funcionarioId}")]
        public async Task<IActionResult> RecuperarMetasPorFuncionarioIdAsync(int funcionarioId, [FromQuery]PaginaParametros paginaParametros)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService
                    .RecuperarMetasPorFuncionarioIdAsync(funcionarioId, paginaParametros);

                if (funcionarioMeta == null) return NoContent();

                Response.CriarPaginacao(funcionarioMeta.PaginaAtual, 
                    funcionarioMeta.TamanhoDaPagina, 
                    funcionarioMeta.ContadorTotal, 
                    funcionarioMeta.TotalDePaginas);

                return Ok(funcionarioMeta.ToArray());
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar metas de um funcionário. Erro: {ex.Message}");
            }
        }

        [HttpGet("{funcionarioId}/{metaId}/meta")]
        public async Task<IActionResult> RecuperarFuncionarioMetaAsync(int funcionarioId, int metaId)
        {
            try 
            {
                var funcionarioMeta = await _funcionarioMetaService
                    .RecuperarFuncionarioMetaPorIdAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) return NoContent();

                return Ok(funcionarioMeta);
            }
            catch (Exception ex)
           {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar metas de um funcionário. Erro: {ex.Message}");
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> CriarFuncioanrioMeta(FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService
                    .CriarFuncionarioMeta( model);

                if (funcionarioMeta == null) return NoContent();

                return Ok(funcionarioMeta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar funcionários. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> AlterarFuncionarioMeta(FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService
                    .AlterarFuncionarioMeta(model);

                if (funcionarioMeta == null) return NoContent();

                return Ok(funcionarioMeta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar alterar metas de um funcionário. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{funcionarioId}/{metaId}")]
        public async Task<IActionResult> ExcluirFuncionarioMetaAsync(int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService
                    .RecuperarFuncionarioMetaPorIdAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) return NoContent();

                return await _funcionarioMetaService.ExcluirFuncionarioMeta(funcionarioId, metaId)
                    ? Ok(new { message = "Excluído"}) 
                    : throw new Exception("Ocorreu ma falaha ao tentar deletar meta de um funcionário.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar excluir meta {metaId}. Erro: {ex.Message}");
            }
        }
    }
    
}
