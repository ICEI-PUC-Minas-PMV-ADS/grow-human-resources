using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.API.Controllers.Funcionarios
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosDadosPessoaisController : ControllerBase
    {
        private readonly IFuncionarioDadoPessoalService _funcionarioDadoPessoalService;

        public FuncionariosDadosPessoaisController(IFuncionarioDadoPessoalService funcionarioDadoPessoalService)
        {
            _funcionarioDadoPessoalService = funcionarioDadoPessoalService;
        }

        [HttpGet("{dadoPessoalId}")]
        public async Task<IActionResult> RecuperarDadosPessoaisPorId(int dadoPessoalId)
        {
            try
            {
                var dadoPessoal = await _funcionarioDadoPessoalService
                    .RecuperarDadoPessoalPorIdAsync(dadoPessoalId);

                if (dadoPessoal == null) return NoContent();

                return Ok(dadoPessoal);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao recuperar Dado Pessoal. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarDadosPessoais(DadoPessoalDto model)
        {
            try
            {
                var dadoPessoal = await _funcionarioDadoPessoalService
                    .CriarDadoPessoalAsync(model);

                if (dadoPessoal == null) return NoContent();

                return Ok(dadoPessoal);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Dados Pessoal. Erro: {ex.Message}");
            }
        }
        [HttpPut("{dadoPessoalId}")]
        public async Task<IActionResult> SalvarDadosPessoais(int dadoPessoalId, DadoPessoalDto model)
        {
            try
            {
                var dadoPessoal = await _funcionarioDadoPessoalService
                    .AlterarDadoPessoalAsync(dadoPessoalId, model);

                if (dadoPessoal == null) return NoContent();

                return Ok(dadoPessoal);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao alterar Dado Pessoal. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{dadoPessoalId}")]
        public async Task<IActionResult> ExcluirDados(int dadoPessoalId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioDadoPessoalService
                    .RecuperarDadoPessoalPorIdAsync(dadoPessoalId);

                if (funcionarioMeta == null) return NoContent();

                return await _funcionarioDadoPessoalService.ExcluirDadoPessoalAsync(dadoPessoalId)
                    ? Ok(new { message = "Excluído"}) 
                    : throw new Exception("Falha ao excluir Dado Pessoal.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Falha ao excluir Dado Pessoal {dadoPessoalId}. Erro: {ex.Message}");
            }
        }
    }
}
