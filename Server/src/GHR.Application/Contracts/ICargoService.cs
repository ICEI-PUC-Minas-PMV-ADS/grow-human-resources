using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface ICargoService
    {
        Task<CargoDto> AddCargo(CargoDto model);
        Task<CargoDto> UpdateCargo(int cargoId, CargoDto model);
        Task<bool> DeleteCargo(int cargoId);
        Task<CargoDto[]> GetAllCargosByNomeCargoAsync(string nome);
        Task<CargoDto[]> GetAllCargosAsync();
        Task<CargoDto> GetCargoByIdAsync(int cargoId);
    } 
}