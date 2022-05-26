using System.Threading.Tasks;
using GHR.Application.Dtos.Contas;
using Microsoft.AspNetCore.Identity;

namespace GHR.Application.Services.Contracts.Contas
{
    public interface IContaService
    {
        Task<bool> VerificarContaExiste(string userName);
        Task<ContaAtualizarDto> RecuperarContaPorUserNameAsync(string userName);
        Task<ContaVisaoDto> RecuperarContaPorIdAsync(int userId);
        Task<SignInResult> ValidarContaSenhaAsync(ContaAtualizarDto contaAtualizarDto, string password);
        Task<ContaAtualizarDto> CriarContaAsync(ContaDto contaDto);
        Task<ContaAtualizarDto> AlterarConta(ContaAtualizarDto contaAtualizarDto);
        Task<ContaVisaoDto> AtualizarConta(ContaVisaoDto contaVisaoDto);
    }
}