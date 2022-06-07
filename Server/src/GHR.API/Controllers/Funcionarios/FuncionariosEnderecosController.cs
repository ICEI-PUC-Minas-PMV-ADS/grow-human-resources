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
    public class FuncionariosEnderecosController : ControllerBase
    {
        private readonly IFuncionarioEnderecoService _funcionarioEnderecoService;

        public FuncionariosEnderecosController(IFuncionarioEnderecoService funcionarioEnderecoService)
        {
            _funcionarioEnderecoService = funcionarioEnderecoService;
        }

        [HttpGet("{enderecoId}")]
        public async Task<IActionResult> RecuperarEnderecoPorId(int enderecoId, int funcionarioId)
        {
            try
            {
                var endereco = await _funcionarioEnderecoService
                    .RecuperarEnderecoPorIdAsync(enderecoId);

                if (endereco == null) return NoContent();

                return Ok(endereco);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao recuperar Endereco. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarEndereco(EnderecoDto model)
        {
            try
            {
                var endereco = await _funcionarioEnderecoService
                    .CriarEnderecoAsync(model);

                if (endereco == null) return NoContent();

                return Ok(endereco);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar adicionar Endereco. Erro: {ex.Message}");
            }
        }
        [HttpPut("{enderecoId}")]
        public async Task<IActionResult> AlterarEndereco(int enderecoId, EnderecoDto model)
        {
            try
            {
                var endereco = await _funcionarioEnderecoService
                    .AlterarEnderecoAsync(enderecoId, model);

                if (endereco == null) return NoContent();

                return Ok(endereco);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao alterar Endereco. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{enderecoId}")]
        public async Task<IActionResult> ExcluirEndereco(int enderecoId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioEnderecoService
                    .RecuperarEnderecoPorIdAsync(enderecoId);

                if (funcionarioMeta == null) return NoContent();

                return await _funcionarioEnderecoService.ExcluirEnderecoAsync(enderecoId)
                    ? Ok(new { message = "Excluído"}) 
                    : throw new Exception("Falha ao excluir Endereco.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Falha ao excluir Endereco {enderecoId}. Erro: {ex.Message}");
            }
        }
    }
}
