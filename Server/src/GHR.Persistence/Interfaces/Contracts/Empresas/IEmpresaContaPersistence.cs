using System.Collections.Generic;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Empresas;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Empresas

{
    public interface IEmpresaContaPersistence : IGlobalPersistence
    {
        Task<Empresa> RecuperarEmpresaPorIdAsync(int id);
        Task<EmpresaConta> RecuperarEmpresaContaPorEmpresaIdUserNameAsync(int empresaId, string userName);
    }
}