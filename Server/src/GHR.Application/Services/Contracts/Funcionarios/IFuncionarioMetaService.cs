using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioMetaService
    {
        Task<FuncionarioMetaDto> CriarFuncionarioMeta(int userId, string visao, FuncionarioMetaDto model);
        Task<FuncionarioMetaDto> AlterarFuncionarioMeta(int userId, string visao, int funcionarioId, int metaId, FuncionarioMetaDto model);
        Task<bool> ExcluirFuncionarioMeta(int userId, string visao, int funcionarioId, int metaId);
        Task<FuncionarioMetaDto[]> RecuperarMetasPorFuncionarioIdAsync(int userId, string visao, int funcionarioId);
        Task<FuncionarioMetaDto> RecuperarFuncionarioMetaPorIdAsync(int userId, string visao, int funcionarioId, int metaId);
    }
}