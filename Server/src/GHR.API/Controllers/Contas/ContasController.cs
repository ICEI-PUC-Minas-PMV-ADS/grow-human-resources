using System;
using System.Threading.Tasks;
using GHR.API.Extensions;
using GHR.API.Helpers;
using GHR.Application.Dtos.Contas;
using GHR.Application.Services.Contracts.Contas;
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
                var user = await _contaService.RecuperarContaAtivaAsync(userName);
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao recuperar a Conta Ativa. Erro {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarContaPorIdAsync(int id) {
            try
            {
                var user = await _contaService.RecuperarContaPorIdAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao recuperar a conta por ID. Erro {ex.Message}");
            }
        }

        [HttpGet("{userName}/userName")]
        public async Task<IActionResult> RecuperarContaPorUserName(string userName) {
            try
            {
                var user = await _contaService.RecuperarContaPorUserNameAsync(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao recuperar a conta por USERNAME. Erro {ex.Message}");
            }
        }

        [HttpPost("CadastrarConta")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarConta(ContaDto model)
        {
            try
            {
                if (await _contaService.VerificarContaExiste(model.UserName))
                    return BadRequest("Conta já cadastrada");

                var conta = await _contaService.CadastrarContaAsync(model);

                if (conta != null)
                    return Ok(new
                    {
                        userName = conta.UserName,
                        nomeCompleto = conta.NomeCompleto,
                        id = conta.Id,
                        visao = conta.Visao,
                        token = _tokenService.CadastrarToken(conta).Result
                    });

            return BadRequest("CONTA não cadastrada!");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao cadastrar a conta. Erro {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ContaLoginDto model)
        {
            try
            {
                var conta = await _contaService
                    .RecuperarContaPorUserNameAsync(model.UserName);

                if (conta == null) return Unauthorized("Conta não cadastrada!");
               
                var result = await _contaService.ValidarContaSenhaAsync(conta, model.Password);

                if (!result.Succeeded)
                    return Unauthorized("Conta ou senha inválidos!");

                return Ok(new {
                    userName = conta.UserName,
                    nomeCompleto = conta.NomeCompleto,
                    id = conta.Id,
                    visao = conta.Visao,
                    token = _tokenService.CadastrarToken(conta).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao realizar LOGIN. Erro {ex.Message}");
            }
        }

        [HttpPut("AlterarContaToken")]
           public async Task<IActionResult> AlterarContaToken(ContaAtualizarDto model)
        {
            try
            {
                var useNameClaim = User.RecuperarUserNameClaim();
                
                if (model.UserName != useNameClaim)
                {
                    return Unauthorized("CONTA inválida para atualizacao");
                }

                var conta = await _contaService.RecuperarContaPorUserNameAsync(useNameClaim);

                if (conta == null)
                    return Unauthorized("Conta inválida");

                var contaAlterada = await _contaService.AlterarContaToken(model);

                if (contaAlterada == null)
                    return NoContent();

                return Ok(new
                {
                    userName = contaAlterada.UserName,
                    nomeCompleto = contaAlterada.NomeCompleto,
                    id = contaAlterada.Id,
                    visao = contaAlterada.Visao,
                    token = _tokenService.CadastrarToken(contaAlterada).Result
                });
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar a conta. Erro {ex.Message}");
            }
        }
        
        [HttpPut("AlterarContaVisao")]
           public async Task<IActionResult> AlterarContaVisao(ContaVisaoDto model)
        {
            try
            {
                var conta = await _contaService.RecuperarContaPorIdAsync(model.Id);

                if (conta == null)
                    return Unauthorized("CONTA não encontrada");

                var contaRetorno = await _contaService.AlterarContaVisao(model);

                if (contaRetorno == null)
                    return NoContent();

                return Ok(model);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao alterar a conta VISAO. Erro {ex.Message}");
            }
        }

        [HttpPost("upload-imagem")]
        public async Task<IActionResult> UploadImagem()
        {
            try
            {
                var conta = await _contaService.RecuperarContaPorIdAsync(User.RecuperarUserIdClaim());
                     
                if (conta == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0) {
                    _utilUpload.ExcluirImagem(conta.ImagemURL, _destino);
                    conta.ImagemURL = await _utilUpload.SalvarImagem(file, _destino);
                }

                return Ok(await _contaService.AlterarContaVisao(conta));
            }
         catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao atualizar a FOTO. Erro {ex.Message}");
            }
        }
    }
}