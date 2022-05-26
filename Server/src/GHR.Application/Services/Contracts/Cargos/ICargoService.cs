using System.Threading.Tasks;
using GHR.Application.Dtos.Cargos;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Cargos
{
    public interface ICargoService
    {
        Task<CargoDto> CriarCargo(CargoDto model);
        Task<CargoDto> AlterarCargo(int cargoId, CargoDto model);
        Task<bool> ExcluirCargo(int cargoId);
        Task<PaginaLista<CargoDto>> RecuperarCargosAsync(PaginaParametros paginaParametros);
        Task<CargoDto> RecuperarCargoPorIdAsync(int cargoId);
    } 
}