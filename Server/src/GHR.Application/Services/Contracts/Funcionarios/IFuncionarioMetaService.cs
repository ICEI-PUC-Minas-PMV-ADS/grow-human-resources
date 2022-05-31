using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioMetaService
    {
        Task<FuncionarioMetaDto> CriarFuncionarioMeta(FuncionarioMetaDto model);
        Task<FuncionarioMetaDto> AlterarFuncionarioMeta(FuncionarioMetaDto model);
        Task<bool> ExcluirFuncionarioMeta(int funcionarioId, int metaId);
        Task<PaginaLista<FuncionarioMetaDto>> RecuperarMetasPorFuncionarioIdAsync(int funcionarioId, PaginaParametros paginaParametros);
        Task<FuncionarioMetaDto> RecuperarFuncionarioMetaPorIdAsync(int funcionarioId, int metaId);
    }
}