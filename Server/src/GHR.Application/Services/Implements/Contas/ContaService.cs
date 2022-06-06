using System;
using System.Threading.Tasks;

using AutoMapper;

using GHR.Application.Dtos.Contas;
using GHR.Application.Services.Contracts.Contas;
using GHR.Domain.DataBase.Contas;
using GHR.Persistence.Interfaces.Contracts.Contas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GHR.Application.Services.Implements.Contas
{
    public class ContaService : IContaService
    {
        private readonly UserManager<Conta> _userManager;
        private readonly SignInManager<Conta> _signInManager;
        private readonly IMapper _mapper;
        private readonly IContaPersistence _contaPersistence;

        public ContaService(UserManager<Conta> userManager,
            SignInManager<Conta> signInManager,
            IMapper mapper,
            IContaPersistence contaPersistence)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _contaPersistence = contaPersistence;
        }

        public async Task<ContaVisaoDto> AtualizarConta(ContaVisaoDto contaVisaoDto)
        {
            try
            {
                var conta = await _contaPersistence.RecuperarContaPorIdAsync(contaVisaoDto.Id);

                if (conta == null) return null;
                    
                contaVisaoDto.Id = conta.Id;

                _mapper.Map(contaVisaoDto, conta);

                _contaPersistence.Alterar<Conta>(conta);

                if (await _contaPersistence.SalvarAsync()) {
                    var contaRetorno = await _contaPersistence.RecuperarContaPorIdAsync(conta.Id);

                    return _mapper.Map<ContaVisaoDto>(contaRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao salvar usuário. Erro: {ex.Message}");
            }    
        }
        public async Task<ContaAtualizarDto> CriarContaAsync(ContaDto contaDto)
        {
            try
            {
                var conta = _mapper.Map<Conta>(contaDto);
                
                var contaCriada = await _userManager.CreateAsync(conta, contaDto.Password);

                if (contaCriada.Succeeded) {
                    var retornaConta = _mapper.Map<ContaAtualizarDto>(conta);
                    return retornaConta;
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao criar conta. Erro: {ex.Message}");
            }
        }

        public async Task<ContaVisaoDto> RecuperarContaPorIdAsync(int userId, int empresaId)
        {
            try
            {
                var conta = await _contaPersistence.RecuperarContaPorIdAsync(userId);

                if (conta == null) return null;

                var contaVisaoDto = _mapper.Map<ContaVisaoDto>(conta);

                return contaVisaoDto;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao recuperar conta por userId. Erro: {ex.Message}");
            }
        }


        public async Task<ContaAtualizarDto> RecuperarContaPorUserNameAsync(string userName, int empresaId)
        {
            try
            {
                var conta = await _contaPersistence.RecuperarContaPorUserNameAsync(userName);

                if (conta == null) return null;

                var ContaAtualizarDto = _mapper.Map<ContaAtualizarDto>(conta);

                return ContaAtualizarDto;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao recuperar usuário por username. Erro: {ex.Message}");
            }
        }

        public async Task<ContaVisaoDto> RecuperarContaAtivaAsync(string userName, int empresaId)
        {
            try
            {
                var conta = await _contaPersistence.RecuperarContaPorUserNameAsync(userName);

                if (conta == null) return null;

                var ContaVisaoDto = _mapper.Map<ContaVisaoDto>(conta);

                return ContaVisaoDto;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao recuperar usuário por username. Erro: {ex.Message}");
            }
        }

        public async Task<ContaAtualizarDto> AlterarConta(int empresaId, ContaAtualizarDto contaAtualizarDto)
        {
            try
            {
                var conta = await _contaPersistence.RecuperarContaPorUserNameAsync(contaAtualizarDto.UserName);

                if (conta == null) return null;
                    
                contaAtualizarDto.Id = conta.Id;

                _mapper.Map(contaAtualizarDto, conta);

                var token = await _userManager.GeneratePasswordResetTokenAsync(conta);

                await _userManager.ResetPasswordAsync(conta, token, contaAtualizarDto.Password);

                _contaPersistence.Alterar<Conta>(conta);

                if (await _contaPersistence.SalvarAsync()) {
                    var contaRetorno = await _contaPersistence.RecuperarContaPorUserNameAsync(conta.UserName);

                    return _mapper.Map<ContaAtualizarDto>(contaRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao salvar usuário. Erro: {ex.Message}");
            }    
        }

        public async Task<bool> VerificarContaExiste(string userName, int empresaId)
        {
            try
            {
                return await _userManager
                    .Users                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                    .AnyAsync(user => user.UserName == userName.ToLower() );
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao verificar se a conta existe. Erro: {ex.Message}");
            }
        }

        public async Task<SignInResult> ValidarContaSenhaAsync(ContaAtualizarDto contaAtualizarDto, string password)
        {
            try
            {
                var conta = await _userManager
                    .Users
                    .SingleOrDefaultAsync(conta =>
                        conta.UserName == contaAtualizarDto.UserName.ToLower() );

                return await _signInManager
                    .CheckPasswordSignInAsync(conta, password, false);
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Falha ao validar Conta e Senha. Erro: {ex.Message}");
            }
        }
    }
}