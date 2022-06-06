using System.Threading.Tasks;
using GHR.Application.Dtos.Contas;

namespace GHR.Application.Services.Contracts.Contas
{
    public interface ITokenService
    {
        Task<string> CriarToken(ContaAtualizarDto contaAtualizarDto, int empresaId);
    }
}