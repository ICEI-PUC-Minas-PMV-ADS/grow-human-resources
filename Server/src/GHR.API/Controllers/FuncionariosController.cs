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
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var funcionarios = await _funcionarioService.GetAllFuncionariosAsync(true);

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
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id, true);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar funcionários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{nome}/nome")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var funcionario = await _funcionarioService.GetAllFuncionariosByNomeCompletoAsync(nome, true);

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
        public async Task<IActionResult> Post(FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioService.AddFuncionarios(model);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar funcionários. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioService.UpdateFuncionario(id, model);

                if (funcionario == null) return NoContent();

                return Ok(funcionario);
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
                var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id, true);

                if (funcionario == null) return NoContent();

                return await _funcionarioService.DeleteFuncionario(id)
                    ? Ok(new { message = "Excluído"}) 
                    : throw new Exception("Ocorreu ma falaha ao tentar deletar o funcionario.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar excluir funcionário {id}. Erro: {ex.Message}");
            }
        }
    }
}
