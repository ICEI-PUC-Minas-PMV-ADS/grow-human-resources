using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GHR.API.Extensions;
using GHR.Application.Contracts;
using GHR.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GHR.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                 ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
        
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUSer() {
            try
            {
                var userName = User.GetUserName();

                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta. Erro {ex.Message}");
            }
        }

        [HttpPost("CadastrarConta")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarConta(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Conta já cadastrada");

                var userRetorno = await _accountService.CreateAccountAsync(userDto);

                if (userRetorno != null)
                    return Ok(new
                    {
                        userName = userRetorno.UserName,
                        nomeCompleto = userRetorno.NomeCompleto,
                        funcao = userRetorno.Funcao,
                        visao = userRetorno.Visao,
                        token = _tokenService.CreateToken(userRetorno).Result
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
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLoginDto.UserName);

                if (user == null) return Unauthorized("Conta não cadastrada!");

                var result = await _accountService.CheckUserPasswordAsync(user, userLoginDto.Password);

                if (!result.Succeeded)
                    return Unauthorized("Conta ou senha inválidos!");

                return Ok(new {
                    userName = user.UserName,
                    nomeCompleto = user.NomeCompleto,
                    funcao = user.Funcao,
                    visao = user.Visao,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao recuperar a conta. Erro {ex.Message}");
            }
        }

        [HttpPut("AlterarConta")]
           public async Task<IActionResult> AlterarConta(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto.UserName != User.GetUserName())
                {
                    return Unauthorized("Conta inválida para atualizacao");
                }

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());

                if (user == null)
                    return Unauthorized("Conta inválida");

                var userRetorno = await _accountService.UpdateAccount(userUpdateDto);

                if (userRetorno == null)
                    return NoContent();

                return Ok(new
                {
                    userName = userRetorno.UserName,
                    nomeCompleto = userRetorno.NomeCompleto,
                    funcao = userRetorno.Funcao,
                    visao = userRetorno.Visao,
                    token = _tokenService.CreateToken(userRetorno).Result
                });
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao atualizar a conta. Erro {ex.Message}");
            }
        }
    }
}