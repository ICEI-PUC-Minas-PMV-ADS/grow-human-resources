using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Funcionarios
{
    public interface IFuncionarioPersistence: IGlobalPersistence
    {
        Task<Funcionario[]> RecuperarFuncionariosPorNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false);
        Task<Funcionario[]> RecuperarFuncionariosAsync(int userId, string visao, bool incluirMetas = false);
        Task<Funcionario> RecuperarFuncionarioPorIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false);
    }
}