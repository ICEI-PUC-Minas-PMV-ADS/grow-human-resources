
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.API.Extensions;
using GHR.Application.Dtos.Funcionarios;
using GHR.Persistence.Models;


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
        public async Task<IActionResult> RecuperarFuncionarios([FromQuery]PaginaParametros paginaParametros)
        {
            try
            {

                var funcionarios = await _funcionarioService
                    .RecuperarFuncionariosAsync(paginaParametros, true);

                if (funcionarios == null) return NoContent();

                Response.CriarPaginacao(funcionarios.PaginaAtual, 
                    funcionarios.TamanhoDaPagina, 
                    funcionarios.ContadorTotal, 
                    funcionarios.TotalDePaginas);

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
                    .RecuperarFuncionarioPorIdAsync(id, true);

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

        [HttpPost]
        public async Task<IActionResult> CriarFuncionario(FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioService
                    .CriarFuncionarios( model);

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
                    .AlterarFuncionario(id, model);

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
                    .RecuperarFuncionarioPorIdAsync(id, true);

                if (funcionario == null) return NoContent();

                
                if (await _funcionarioService.ExcluirFuncionario( id)){
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
