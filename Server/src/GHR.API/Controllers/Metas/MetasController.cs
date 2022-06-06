using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Metas;
using GHR.Application.Dtos.Metas;
using GHR.Persistence.Models;
using GHR.API.Extensions;

namespace GHR.API.Controllers.Metas
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MetasController : ControllerBase
    {
        private readonly IMetaService _metaService;

        public MetasController(IMetaService metaService)
        {
            _metaService = metaService;
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarMetas([FromQuery]PaginaParametros paginaParametros)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var metas = await _metaService
                    .RecuperarMetasAsync(empresaId, paginaParametros, true);

                if (metas == null) return NoContent();

                Response.CriarPaginacao(metas.PaginaAtual, 
                    metas.TamanhoDaPagina, 
                    metas.ContadorTotal, 
                    metas.TotalDePaginas);
                
                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar meta. Erro: {ex.Message}");
            }
        }

        [HttpGet("ativas")]
        public async Task<IActionResult> RecuperarMetasAtivas()
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var metas = await _metaService
                    .RecuperarMetasAtivasAsync(empresaId);

                if (metas == null) return NoContent();

                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar metas. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarMetaPorId(int id)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var meta = await _metaService
                    .RecuperarMetaPorIdAsync(id, empresaId, true);

                if (meta == null) return NoContent();

                return Ok(meta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar metas: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarMeta(MetaDto model)
        {
            try
            {
                var meta = await _metaService.CriarMeta(model);

                if (meta == null) return NoContent();

                return Ok(meta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar adicionar meta. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> SalvarMeta(int id, MetaDto model)
        {
            try
            {
                var meta = await _metaService
                    .AlterarMeta(id, model);

                if (meta == null) return NoContent();

                return Ok(meta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar funcionários. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirMeta(int id)
        {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var meta = await _metaService
                    .RecuperarMetaPorIdAsync(id, empresaId, true);

                if (meta == null) return NoContent();

                return await _metaService.ExcluirMeta(id, empresaId)
                    ? Ok(new { message = "Excluído" })
                    : throw new Exception("Ocorreu ma falaha ao tentar deletar a meta.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir meta {id}. Erro: {ex.Message}");
            }
        }
    }
}
