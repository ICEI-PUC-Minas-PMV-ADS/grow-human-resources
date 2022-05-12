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
    public class FuncionariosMetasController : ControllerBase
    {
        private readonly IFuncionarioMetaService _funcionarioMetaService;
        public FuncionariosMetasController(IFuncionarioMetaService funcionarioMetaService)
        {
            _funcionarioMetaService = funcionarioMetaService;

        }

        [HttpGet("{funcionarioId}")]
        public async Task<IActionResult> GetById(int funcionarioId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService.GetMetasByFuncionarioIdAsync(funcionarioId);

                if (funcionarioMeta == null) return NoContent();

                return Ok(funcionarioMeta.ToArray());
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar metas de um funcionário. Erro: {ex.Message}");
            }
        }

        [HttpGet("{funcionarioId}/{metaId}/meta")]
        public async Task<IActionResult> GetByFuncionarioMeta(int funcionarioId, int metaId)
        {
            try 
            {
                var funcionarioMeta = await _funcionarioMetaService.GetFuncionarioMetaAsync(funcionarioId, metaId);

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
        public async Task<IActionResult> Post(FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService.AddFuncionarioMeta(model);

                if (funcionarioMeta == null) return NoContent();

                return Ok(funcionarioMeta);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar funcionários. Erro: {ex.Message}");
            }
        }
        [HttpPut("{funcionarioId}/{metaId}")]
        public async Task<IActionResult> Put(int funcionarioId, int metaId, FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService.UpdateFuncionarioMeta(funcionarioId, metaId, model);

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
        public async Task<IActionResult> Delete(int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService.GetFuncionarioMetaAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) return NoContent();

                return await _funcionarioMetaService.DeleteFuncionarioMeta(funcionarioId, metaId)
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
