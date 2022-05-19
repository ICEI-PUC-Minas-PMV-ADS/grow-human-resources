using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Serices.Contracts.Metas;
using GHR.Application.Services.Contracts.Contas;
using GHR.API.Extensions;
using GHR.Application.Dtos.Metas;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                var metas = await _metaService
                    .RecuperarMetasAsync(User.RecuperarUserId(), User.RecuperarVisao(), true);

                if (metas == null) return NoContent();

                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar meta. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var meta = await _metaService
                    .RecuperarMetaPorIdAsync(User.RecuperarUserId(), User.RecuperarVisao(), id, true);

                if (meta == null) return NoContent();

                return Ok(meta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar metas: {ex.Message}");
            }
        }

        [HttpGet("{nome}/nome")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var metas = await _metaService
                    .RecuperarMetasPorNomeMetaAsync(User.RecuperarUserId(), User.RecuperarVisao(), nome, true);

                if (metas == null) return NoContent();

                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar meta. Erro: {ex.Message}");
            }
        }
        [HttpGet("{descricao}/descricao")]
        public async Task<IActionResult> GetByDescricao(string descricao)
        {
            try
            {
                var metas = await _metaService
                    .RecuperarMetasPorDescricaoMetaAsync(User.RecuperarUserId(), User.RecuperarVisao(), descricao, true);

                if (metas == null) return NoContent();

                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar meta. Erro: {ex.Message}");
            }
        }
        [HttpGet("{metaCumprida}/metaCumprida")]
        public async Task<IActionResult> GetByMetaCumprida(bool metaCumprida)
        {
            try
            {
                var metas = await _metaService
                    .RecuperarMetasPorMetaCumpridaAsync(User.RecuperarUserId(), User.RecuperarVisao(), metaCumprida, true);

                if (metas == null) return NoContent();

                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar meta. Erro: {ex.Message}");
            }
        }
        [HttpGet("{metaAprovada}/metaAprovada")]
        public async Task<IActionResult> GetByMetaAprovada(bool metaAprovada)
        {
            try
            {
                var metas = await _metaService
                    .RecuperarMetasPorMetaAprovadaAsync(User.RecuperarUserId(), User.RecuperarVisao(), metaAprovada, true);

                if (metas == null) return NoContent();

                return Ok(metas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar meta. Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(MetaDto model)
        {
            try
            {
                var meta = await _metaService.CriarMeta(User.RecuperarUserId(), User.RecuperarVisao(), model);

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
        public async Task<IActionResult> Put(int id, MetaDto model)
        {
            try
            {
                var meta = await _metaService
                    .AlterarMeta(User.RecuperarUserId(), User.RecuperarVisao(), id, model);

                if (meta == null) return NoContent();

                return Ok(meta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar funcionários. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var meta = await _metaService
                    .RecuperarMetaPorIdAsync(User.RecuperarUserId(), User.RecuperarVisao(), id, true);

                if (meta == null) return NoContent();

                return await _metaService.ExcluirMeta(User.RecuperarUserId(), User.RecuperarVisao(), id)
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
