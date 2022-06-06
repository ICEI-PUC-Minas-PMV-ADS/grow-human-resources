using System.Threading.Tasks;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Contracts.Cargos
{
    public interface ICargoPersistence : IGlobalPersistence
    {
        Task<PaginaLista<Cargo>> RecuperarCargosAsync(PaginaParametros paginaParametros, int empresaId);
        Task<Cargo> RecuperarCargoPorIdAsync(int cargoId, int empresaId);
        Task<Cargo[]> RecuperarCargosPorDepartamentoIdAsync(int departamentoId, int empresaId);
    }
}