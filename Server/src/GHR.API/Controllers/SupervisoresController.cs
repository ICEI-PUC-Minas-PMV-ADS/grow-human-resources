using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Contracts;
using GHR.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GHR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupervisoresController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;
        public SupervisoresController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var supervisores = await _supervisorService.GetAllSupervisoresAsync();

                if (supervisores == null) return NoContent();

                return Ok(supervisores);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar supervisor. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supervisor = await _supervisorService.GetSupervisorByIdAsync(id);

                if (supervisor == null) return NoContent();

                return Ok(supervisor);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar supervisor. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(SupervisorDto model)
        {
            try
            {
                var supervisor = await _supervisorService.AddSupervisor(model);

                if (supervisor == null) return NoContent();

                return Ok(supervisor);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar supervisores. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SupervisorDto model)
        {
            try
            {
                var supervisor = await _supervisorService.UpdateSupervisor(id, model);

                if (supervisor == null) return NoContent();

                return Ok(supervisor);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar supervisores. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var supervisor = await _supervisorService.GetSupervisorByIdAsync(id);

                if (supervisor == null) return NoContent();

                return await _supervisorService.DeleteSupervisor(id)
                    ? Ok(new { message = "Excluído" })
                    : throw new Exception("Ocorreu ma falaha ao tentar deletar o supervisor.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir supervisor{id}. Erro: {ex.Message}");
            }
        }
    }
}