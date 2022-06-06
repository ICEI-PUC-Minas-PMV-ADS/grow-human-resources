using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Contracts.Funcionarios
{
    public interface IFuncionarioMetaPersistence : IGlobalPersistence
    {
        Task<PaginaLista<FuncionarioMeta>> RecuperarMetasPorFuncionarioIdAsync(int funcionarioId, int empresaId, PaginaParametros paginaParametros);
        Task<FuncionarioMeta> RecuperarFuncionarioMetaAsync(int funcionarioId, int metaId, int empresaId);
        Task<FuncionarioMeta[]> RecuperarFuncionariosMetasAsync(int empresaId);
    }
}