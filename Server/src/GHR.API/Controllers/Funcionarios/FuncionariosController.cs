using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.API.Extensions;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.API.Controllers.Funcionarios
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;

        }

        [HttpGet]
        public async Task<IActionResult> RecuperarFuncionarios()
        {
            try
            {

                var funcionarios = await _funcionarioService
                    .RecuperarFuncionariosAsync(User.RecuperarUserId(), User.RecuperarVisao(), true);

                if (funcionarios == null) return NoContent();

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar funcionários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarFuncionarioPorId(int id)
        {
            try
            {
                var funcionario = await _funcionarioService
                    .RecuperarFuncionarioPorIdAsync(User.RecuperarUserId(), User.RecuperarVisao(), id, true);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar funcionários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{contaId}/contaId")]
        public async Task<IActionResult> RecuperarFuncionarioPorUserId(int contaId)
        {
            try
            {
                var funcionario = await _funcionarioService
                    .RecuperarFuncionarioPorContaIdAsync(contaId);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar funcionários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{nome}/nome")]
        public async Task<IActionResult> RecuperarFuncionarioPorNome(string nome)
        {
            try
            {
                var funcionario = await _funcionarioService
                .RecuperarFuncionariosPorNomeCompletoAsync(User.RecuperarUserId(), User.RecuperarVisao(), nome, true);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar funcionários. Erro: {ex.Message}");
            }
        }   

        [HttpPost]
        public async Task<IActionResult> CriarFuncionario(FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioService
                    .CriarFuncionarios(User.RecuperarUserId(), User.RecuperarVisao(), model);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar adicionar funcionários. Erro: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> SalvarFuncionario(int id, FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioService
                    .AlterarFuncionario(User.RecuperarUserId(), User.RecuperarVisao(), id, model);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar alterar funcionários. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirFuncioanrio(int id)
        {
            try
            {
                var funcionario = await _funcionarioService
                    .RecuperarFuncionarioPorIdAsync(User.RecuperarUserId(), User.RecuperarVisao(), id, true);

                if (funcionario == null) return NoContent();

                
                if (await _funcionarioService.ExcluirFuncionario(User.RecuperarUserId(), User.RecuperarVisao(), id)){
                    return Ok(new { message = "Excluído" });
                }
                    throw new Exception("Ocorreu ma falaha ao tentar deletar o funcionario.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar excluir funcionário {id}. Erro: {ex.Message}");
            }
        }
    }
}
