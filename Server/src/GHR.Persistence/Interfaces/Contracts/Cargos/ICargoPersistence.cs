using System.Threading.Tasks;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Cargos
{
    public interface ICargoPersistence : IGlobalPersistence
    {
        Task<Cargo[]> RecuperarCargosPorNomeCargoAsync(string nome);
        Task<Cargo[]> RecuperarCargosAsync();
        Task<Cargo> RecuperarCargoPorIdAsync(int cargoId);
    }
}