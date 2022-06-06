using System.Threading.Tasks;
using GHR.Application.Dtos.Contas;
using Microsoft.AspNetCore.Identity;

namespace GHR.Application.Services.Contracts.Contas
{
    public interface IContaService
    {
        Task<ContaAtualizarDto> AlterarConta(int emrpesaId, ContaAtualizarDto contaAtualizarDto);
        Task<ContaVisaoDto> AtualizarConta(ContaVisaoDto contaVisaoDto);
        Task<ContaAtualizarDto> CriarContaAsync(ContaDto contaDto);
        Task<ContaVisaoDto> RecuperarContaAtivaAsync(string userName, int emrpesaId);
        Task<ContaVisaoDto> RecuperarContaPorIdAsync(int userId, int emrpesaId);
        Task<ContaAtualizarDto> RecuperarContaPorUserNameAsync(string userName, int emrpesaId);
        Task<SignInResult> ValidarContaSenhaAsync(ContaAtualizarDto contaAtualizarDto, string password);
        Task<bool> VerificarContaExiste(string userName, int emrpesaId);
    }
}