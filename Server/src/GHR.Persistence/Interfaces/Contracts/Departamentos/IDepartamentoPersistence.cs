using System.Threading.Tasks;
using GHR.Domain.DataBase.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Contracts.Departamentos
{
    public interface IDepartamentoPersistence : IGlobalPersistence 
    {
        //Funcionarios
        Task<PaginaLista<Departamento>> RecuperarDepartamentosAsync(PaginaParametros paginaParametros);
        Task<Departamento> RecuperarDepartamentoPorIdAsync(int departamentoId);
    }
}