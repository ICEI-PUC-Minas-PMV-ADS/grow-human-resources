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
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;
        private readonly IAccountService _acccountService;

        public DepartamentosController(IDepartamentoService departamentoService,
                                       IAccountService acccountService)
        {
            _departamentoService = departamentoService;
            _acccountService = acccountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var departamentos = await _departamentoService.GetAllDepartamentosAsync(User.GetUserId(), User.GetVisao());

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
                var departamento = await _departamentoService.GetDepartamentoByIdAsync(User.GetUserId(), User.GetVisao(), id);

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
                var departamento = await _departamentoService.GetAllDepartamentosByNomeDepartamentoAsync(User.GetUserId(), User.GetVisao(), nome);

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
                var departamento = await _departamentoService.AddDepartamento(User.GetUserId(), User.GetVisao(), model);

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
                var departamento = await _departamentoService.UpdateDepartamento(User.GetUserId(), User.GetVisao(), id, model);

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
                var departamento = await _departamentoService.GetDepartamentoByIdAsync(User.GetUserId(), User.GetVisao(), id);

                if (departamento == null) return NoContent();

                return await _departamentoService.DeleteDepartamento(User.GetUserId(), User.GetVisao(), id)
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
