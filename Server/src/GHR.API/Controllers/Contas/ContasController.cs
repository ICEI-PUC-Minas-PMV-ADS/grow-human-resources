using System;
using System.Threading.Tasks;
using GHR.API.Extensions;
using GHR.API.Helpers;
using GHR.Application.Dtos.Contas;
using GHR.Application.Services.Contracts.Contas;
using GHR.Application.Services.Contracts.Empresas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GHR.API.Controllers.Contas
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : Controller
    {
        private readonly IContaService _contaService;
        private readonly ITokenService _tokenService;
        private readonly IUtilUpload _utilUpload;
        private readonly string _destino = "Fotos";
        public ContasController(IContaService contaService,
                                IUtilUpload utilUpload,
                                ITokenService tokenService)
        {
            _contaService = contaService;
            _tokenService = tokenService;
            _utilUpload = utilUpload;
        }
        
        [HttpGet("RecuperarContaAtiva")]
        public async Task<IActionResult> RecuperarContaAtiva() {
            try
            {
                var userName = User.RecuperarUserNameClaim();
                var empresaId = User.RecuperarEmpresaIdClaim();

                var user = await _contaService.RecuperarContaAtivaAsync(userName, empresaId);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta. Erro {ex.Message}");
            }
        }

        [HttpGet("{userName}/userName")]
        public async Task<IActionResult> RecuperarContaPorUserName(string userName) {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var user = await _contaService.RecuperarContaPorUserNameAsync(userName, empresaId);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta. Erro {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarContaPorIdAsync(int id) {
            try
            {
                var empresaId = User.RecuperarEmpresaIdClaim();
                var user = await _contaService.RecuperarContaPorIdAsync(id, empresaId);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta por Id. Erro {ex.Message}");
            }
        }

        [HttpPost("CadastrarConta")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarConta(ContaDto contaDto, int empresaId)
        {
            try
            {
                if (await _contaService.VerificarContaExiste(contaDto.UserName, empresaId))
                    return BadRequest("Conta já cadastrada");

                var contaRetorno = await _contaService.CriarContaAsync(contaDto);

                if (contaRetorno != null)
                    return Ok(new
                    {
                        userName = contaRetorno.UserName,
                        nomeCompleto = contaRetorno.NomeCompleto,
                        funcao = contaRetorno.Funcao,
                        visao = contaRetorno.Visao,
                        emp = empresaId,
                        nomeFantasia = contaRetorno.Empresas.NomeFantasia,
                        token = _tokenService.CriarToken(contaRetorno, empresaId).Result
                    });

            return BadRequest("Conta não cadastrada, tente novamente!");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta. Erro {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ContaLoginDto contaLoginDto, int empresaId)
        {
            try
            {
                var conta = await _contaService
                    .RecuperarContaPorUserNameAsync(contaLoginDto.UserName, empresaId);

                if (conta == null) return Unauthorized("Conta não cadastrada!");
                var result = await _contaService.ValidarContaSenhaAsync(conta, contaLoginDto.Password);

                if (!result.Succeeded)
                    return Unauthorized("Conta ou senha inválidos!");

                return Ok(new {
                    userName = conta.UserName,
                    nomeCompleto = conta.NomeCompleto,
                    funcao = conta.Funcao,
                    visao = conta.Visao,
                    empId = empresaId,
                    nomeFantasia = conta.Empresas.NomeFantasia,
                    token = _tokenService.CriarToken(conta, empresaId).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta. Erro {ex.Message}");
            }
        }

        [HttpPut("AlterarConta")]
           public async Task<IActionResult> AlterarConta(ContaAtualizarDto contaAtualizarDto, int empresaId)
        {
            try
            {
                if (contaAtualizarDto.UserName != User.RecuperarUserNameClaim())
                {
                    return Unauthorized("Conta inválida para atualizacao");
                }

                var conta = await _contaService.RecuperarContaPorUserNameAsync(User.RecuperarUserNameClaim(), empresaId);

                if (conta == null)
                    return Unauthorized("Conta inválida");

                var contaRetorno = await _contaService.AlterarConta(empresaId, contaAtualizarDto);

                if (contaRetorno == null)
                    return NoContent();

                return Ok(new
                {
                    userName = contaRetorno.UserName,
                    nomeCompleto = contaRetorno.NomeCompleto,
                    funcao = contaRetorno.Funcao,
                    visao = contaRetorno.Visao,
                    empId = empresaId,
                    nomeFantasia = contaRetorno.Empresas.NomeFantasia,
                    token = _tokenService.CriarToken(contaRetorno, empresaId).Result
                });
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar a conta. Erro {ex.Message}");
            }
        }
        
        [HttpPut("AtualizarConta")]
           public async Task<IActionResult> AtualizarConta(ContaVisaoDto contaVisaoDto)
        {
            try
            {
                var conta = await _contaService.RecuperarContaPorIdAsync(contaVisaoDto.Id, contaVisaoDto.EmpresaId);

                if (conta == null)
                    return Unauthorized("Conta não encontrada");

                var contaRetorno = await _contaService.AtualizarConta(contaVisaoDto);

                if (contaRetorno == null)
                    return NoContent();

                return Ok(contaVisaoDto);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar a conta. Erro {ex.Message}");
            }
        }

        [HttpPost("upload-imagem")]
        public async Task<IActionResult> UploadImagem()
        {
            try
            {
                var contaUserName = await _contaService.RecuperarContaPorUserNameAsync(User.RecuperarUserNameClaim(), User.RecuperarEmpresaIdClaim());
                
                var conta = await _contaService.RecuperarContaPorIdAsync(contaUserName.Id, contaUserName.EmpresaId);
                
                if (conta == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0) {
                    _utilUpload.ExcluirImagem(conta.ImagemURL, _destino);
                    conta.ImagemURL = await _utilUpload.SalvarImagem(file, _destino);
                }

                var contaRetorno = await _contaService.AtualizarConta(conta);

                return Ok(contaRetorno);

            }
         catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar a foto. Erro {ex.Message}");
            }
        }

    }
}