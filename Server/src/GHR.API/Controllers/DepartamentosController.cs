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
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;
        public DepartamentosController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var departamentos = await _departamentoService.GetAllDepartamentosAsync();

                if (departamentos == null) return NoContent();

                return Ok(departamentos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar departamentos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);

                if (departamento == null) return NoContent();

                return Ok(departamento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar departamento por Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("{nome}/nome")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var departamento = await _departamentoService.GetAllDepartamentosByNomeDepartamentoAsync(nome);

                if (departamento == null) return NoContent();

                return Ok(departamento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar departamentos. Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(DepartamentoDto model)
        {
            try
            {
                var departamento = await _departamentoService.AddDepartamento(model);

                if (departamento == null) return NoContent();

                return Ok(departamento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar departamento. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DepartamentoDto model)
        {
            try
            {
                var departamento = await _departamentoService.UpdateDepartamento(id, model);

                if (departamento == null) return NoContent();

                return Ok(departamento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar departamento. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);

                if (departamento == null) return NoContent();

                return await _departamentoService.DeleteDepartamento(id)
                    ? Ok(new { message = "Excluído" })
                    : throw new Exception("Ocorreu ma falaha ao tentar deletar o departamento.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir departamento {id}. Erro: {ex.Message}");
            }
        }
    }
}
