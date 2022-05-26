using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Contracts.Funcionarios
{
    public interface IFuncionarioPersistence: IGlobalPersistence
    {
        Task<PaginaLista<Funcionario>> RecuperarFuncionariosAsync(PaginaParametros paginaParametros, bool incluirMetas = false);
        Task<Funcionario> RecuperarFuncionarioPorIdAsync(int funcionarioId, bool incluirMetas = false);
        Task<Funcionario> RecuperarFuncionarioPorContaIdAsync(int contaId);
    }
}