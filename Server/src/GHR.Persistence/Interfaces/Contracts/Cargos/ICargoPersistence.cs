using System.Threading.Tasks;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Contracts.Cargos
{
    public interface ICargoPersistence : IGlobalPersistence
    {
        Task<PaginaLista<Cargo>> RecuperarCargosAsync(PaginaParametros paginaParametros);
        Task<Cargo> RecuperarCargoPorIdAsync(int cargoId);
        Task<Cargo[]> RecuperarCargosPorDepartamentoIdAsync(int departamentoId);
    }
}