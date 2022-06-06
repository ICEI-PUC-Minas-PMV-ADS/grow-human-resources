using System.Threading.Tasks;
using GHR.Application.Dtos.Cargos;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Cargos
{
    public interface ICargoService
    {
        Task<CargoDto> CriarCargo(CargoDto model);
        Task<CargoDto> AlterarCargo(int cargoId, int empresaId, CargoDto model);
        Task<bool> ExcluirCargo(int cargoId, int empresaId);
        Task<PaginaLista<CargoDto>> RecuperarCargosAsync(int empresaId, PaginaParametros paginaParametros);
        Task<CargoDto> RecuperarCargoPorIdAsync(int cargoId, int empresaId);
        Task<CargoDto[]> RecuperarCargosPorDepartamentoIdAsync(int departamentoId, int empresaId);
    } 
}