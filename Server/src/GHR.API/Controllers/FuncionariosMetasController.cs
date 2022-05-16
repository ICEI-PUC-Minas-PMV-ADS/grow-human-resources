using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Contracts;
using GHR.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GHR.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GHR.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosMetasController : ControllerBase
    {
        private readonly IFuncionarioMetaService _funcionarioMetaService;
        private readonly IAccountService _acccountService;

        public FuncionariosMetasController(IFuncionarioMetaService funcionarioMetaService,
                                           IAccountService acccountService)
        {
            _funcionarioMetaService = funcionarioMetaService;
            _acccountService = acccountService;
        }

        [HttpGet("{funcionarioId}")]
        public async Task<IActionResult> GetById(int funcionarioId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaService.GetMetasByFuncionarioIdAsync(User.GetUserId(), User.GetVisao(), funcionarioId);

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
                var funcionarioMeta = await _funcionarioMetaService.GetFuncionarioMetaAsync(User.GetUserId(), User.GetVisao(), funcionarioId, metaId);

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
                var funcionarioMeta = await _funcionarioMetaService.AddFuncionarioMeta(User.GetUserId(), User.GetVisao(), model);

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
                var funcionarioMeta = await _funcionarioMetaService.UpdateFuncionarioMeta(User.GetUserId(), User.GetVisao(), funcionarioId, metaId, model);

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
                var funcionarioMeta = await _funcionarioMetaService.GetFuncionarioMetaAsync(User.GetUserId(), User.GetVisao(), funcionarioId, metaId);

                if (funcionarioMeta == null) return NoContent();

                return await _funcionarioMetaService.DeleteFuncionarioMeta(User.GetUserId(), User.GetVisao(), funcionarioId, metaId)
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
