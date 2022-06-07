using System;
using System.Threading.Tasks;
using GHR.Application.Dtos.Empresas;
using GHR.Application.Services.Contracts.Empresas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GHR.API.Controllers.Empresas
{
     [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        public EmpresasController(
            IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RecuperarEmpreasas() {
            try
            {
                var empresas = await _empresaService.RecuperarEmpresasAsync();
                return Ok(empresas);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar empreasa. Erro {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarEmpresaPorIdAsync(int id) {
            try
            {
                var empresa = await _empresaService.RecuperarEmpresaPorIdAsync(id);
                return Ok(empresa);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar empresa por Id. Erro {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarEmpresa(EmpresaDto empresaDto)
        {
            try
            {
                var empresaRetorno = await _empresaService.CriarEmpresaAsync(empresaDto);

                if (empresaRetorno != null)
                    return Ok(empresaRetorno);

                return BadRequest("Empresa não cadastrada, tente novamente!");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar Empresa. Erro {ex.Message}");
            }
        }
        
        [HttpPut]
           public async Task<IActionResult> AtualizarEmpresa(EmpresaDto empresaDto)
        {
            try
            {
                var empresa = await _empresaService.RecuperarEmpresaPorIdAsync(empresaDto.Id);

                if (empresa == null)
                    return Unauthorized("Empresa não encontrada");

                var empresaRetorno = await _empresaService.AtualizarEmpresa(empresaDto);

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

    }
}