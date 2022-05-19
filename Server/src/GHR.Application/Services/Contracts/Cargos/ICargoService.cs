using System.Threading.Tasks;
using GHR.Application.Dtos.Cargos;

namespace GHR.Application.Services.Contracts.Cargos
{
    public interface ICargoService
    {
        Task<CargoDto> CriarCargo(CargoDto model);
        Task<CargoDto> AlterarCargo(int cargoId, CargoDto model);
        Task<bool> ExcluirCargo(int cargoId);
        Task<CargoDto[]> RecuperarCargosPorNomeCargoAsync(string nome);
        Task<CargoDto[]> RecuperarCargosAsync();
        Task<CargoDto> RecuperarCargoPorIdAsync(int cargoId);
    } 
}