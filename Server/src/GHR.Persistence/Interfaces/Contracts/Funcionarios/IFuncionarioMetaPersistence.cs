using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Funcionarios
{
    public interface IFuncionarioMetaPersistence : IGlobalPersistence
    {
        Task<FuncionarioMeta[]> RecuperarMetasPorFuncionarioIdAsync(int userId, string visao, int funcionarioId);
        Task<FuncionarioMeta> RecuperarFuncionarioMetaAsync(int userId, string visao, int funcionarioId, int metaId);
    }
}