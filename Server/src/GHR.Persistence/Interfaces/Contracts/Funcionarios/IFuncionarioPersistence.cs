using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Contracts.Funcionarios
{
    public interface IFuncionarioPersistence: IGlobalPersistence
    {
        Task<PaginaLista<Funcionario>> RecuperarFuncionariosAsync(int empresaId, PaginaParametros paginaParametros, bool incluirMetas = false);
        Task<Funcionario> RecuperarFuncionarioPorIdAsync(int funcionarioId, int empresaId, bool incluirMetas = false);
        Task<Funcionario> RecuperarFuncionarioPorContaIdAsync(int contaId, int empresaId);
        Task<Funcionario[]> RecuperarFuncionarioPorDepartamentoIdAsync(int departamentoId, int empresaId);
    }
}