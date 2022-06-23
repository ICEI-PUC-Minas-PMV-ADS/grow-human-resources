using System.Collections.Generic;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Empresas;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Empresas

{
    public interface IEmpresaPersistence : IGlobalPersistence
    {
        Task<Empresa> RecuperarEmpresaPorIdAsync(int id);
        Task<IEnumerable<Empresa>> RecuperarEmpresasAsync();
    }
}