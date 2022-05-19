using System.Threading.Tasks;
using GHR.Domain.DataBase.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Departamentos
{
    public interface IDepartamentoPersistence : IGlobalPersistence 
    {
        //Funcionarios
        Task<Departamento[]> RecuperarDepartamentosPorNomeDepartamentoAsync(string nome);
        Task<Departamento[]> RecuperarDepartamentosAsync();
        Task<Departamento> RecuperarDepartamentoPorIdAsync(int departamentoId);
    }
}