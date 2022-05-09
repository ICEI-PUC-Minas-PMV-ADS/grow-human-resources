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
                var metas = await _metaService.GetAllMetasAsync(true);

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
                var meta = await _metaService.GetMetaByIdAsync(id, true);

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
                var metas = await _metaService.GetAllMetasByNomeMetaAsync(nome, true);

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
                var metas = await _metaService.GetAllMetasByDescricaoMetaAsync(descricao, true);

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
                var metas = await _metaService.GetAllMetasByMetaCumpridaAsync(metaCumprida, true);

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
                var metas = await _metaService.GetAllMetasByMetaAprovadaAsync(metaAprovada, true);

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
                var meta = await _metaService.AddMeta(model);

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
                var meta = await _metaService.UpdateMeta(id, model);

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
                var meta = await _metaService.GetMetaByIdAsync(id, true);

                if (meta == null) return NoContent();

                return await _metaService.DeleteMeta(id)
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
