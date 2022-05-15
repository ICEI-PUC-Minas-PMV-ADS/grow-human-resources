using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Contracts;
using GHR.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GHR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        public readonly IWebHostEnvironment _hostEnvironment;
        public FuncionariosController(
            IFuncionarioService funcionarioService,
            IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
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

        [HttpPost("upload-image/{funcionarioId}")]
        public async Task<IActionResult> UploadImage(int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(funcionarioId, false);

                if (funcionario == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0) {
                    DeleteImage(funcionario.ImagemURL);
                    funcionario.ImagemURL = await SaveImage(file);
                }

                var funcionarioRetorno = await _funcionarioService.UpdateFuncionario(funcionarioId, funcionario);

                return Ok(funcionarioRetorno);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao salvar imagem. Erro: {ex.Message}");
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

                
                if (await _funcionarioService.DeleteFuncionario(id)){
                    DeleteImage(funcionario.ImagemURL);
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
        [NonAction]
        public void DeleteImage(string imageName){
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            if (System.IO.File.Exists(imagePath)){
                System.IO.File.Delete(imagePath);
            }
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                .Take(20)
                .ToArray()
            ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmddfff")}{Path.GetExtension(imageFile.FileName)}";
            
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create)) {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
