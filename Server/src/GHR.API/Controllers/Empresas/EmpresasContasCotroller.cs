using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos.Empresas;
using GHR.Application.Services.Contracts.Empresas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace GHR.API.Controllers.Empresas
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasContasController : ControllerBase
    {
        private readonly IEmpresaContaService _empresaContaService;
        public EmpresasContasController(
            IEmpresaContaService empresaContaService)
        {
            _empresaContaService = empresaContaService;
        }
        
/*        [HttpGet("{empresaId}/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> RecuperarEmpreasaContaPorEmpresaIdUserNameAsync(string userName) {
            try
            {
 //               var empresaConta = await _empresaContaService.RecuperarEmpresaContaPorEmpresaIdUserNameAsync(userName);
 //               
 //               if (empresaConta == null) return NoContent();
                
  //              return Ok(empresaConta);
                return Ok(null);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar Empresa/Conta. Erro: {ex.Message}");
            }
        }
*/
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(EmpresaContaDto model)
        {
            try
            {
                var empresaContaRetorno = await _empresaContaService.CriarEmpresaContaAsync(model);

                if (empresaContaRetorno != null)
                    return Ok(empresaContaRetorno);

                return BadRequest("Empresa/Conta não cadastrada, tente novamente!");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar Empresa/Conta. Erro {ex.Message}");
            }
        }
        
 /*       [HttpPut]
           public async Task<IActionResult> AtualizarEmpresa(EmpresaDto empresaDto)
        {
            try
            {
                var empresa = await _empresaContaService.RecuperarEmpresaPorIdAsync(empresaDto.Id);

                if (empresa == null)
                    return Unauthorized("Empresa não encontrada");

                var empresaRetorno = await _empresaContaService.AtualizarEmpresa(empresaContaDto);

                if (empresaRetorno == null)
                    return NoContent();

                return Ok(empresaRetorno);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar empresa. Erro {ex.Message}");
            }
        }
*/
    }
}