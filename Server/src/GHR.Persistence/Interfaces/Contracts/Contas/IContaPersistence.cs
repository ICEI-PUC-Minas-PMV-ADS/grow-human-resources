using System.Collections.Generic;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Contas;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Contas
{
    public interface IContaPersistence : IGlobalPersistence
    {
        Task<IEnumerable<Conta>> RecuperarContasAsync();
        Task<Conta> RecuperarContaPorIdAsync(int id);
        Task<Conta> RecuperarContaPorUserNameAsync(string userName);
    }
}