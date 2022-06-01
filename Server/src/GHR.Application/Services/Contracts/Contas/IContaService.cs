using System.Threading.Tasks;
using GHR.Application.Dtos.Contas;
using Microsoft.AspNetCore.Identity;

namespace GHR.Application.Services.Contracts.Contas
{
    public interface IContaService
    {
        Task<ContaAtualizarDto> AlterarConta(ContaAtualizarDto contaAtualizarDto);
        Task<ContaVisaoDto> AtualizarConta(ContaVisaoDto contaVisaoDto);
        Task<ContaAtualizarDto> CriarContaAsync(ContaDto contaDto);
        Task<ContaVisaoDto> RecuperarContaAtivaAsync(string userName);
        Task<ContaVisaoDto> RecuperarContaPorIdAsync(int userId);
        Task<ContaAtualizarDto> RecuperarContaPorUserNameAsync(string userName);
        Task<SignInResult> ValidarContaSenhaAsync(ContaAtualizarDto contaAtualizarDto, string password);
        Task<bool> VerificarContaExiste(string userName);
    }
}