using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface ICargoService
    {
        Task<CargoDto> AddCargo(int userId, string visao, CargoDto model);
        Task<CargoDto> UpdateCargo(int userId, string visao, int cargoId, CargoDto model);
        Task<bool> DeleteCargo(int userId, string visao, int cargoId);
        Task<CargoDto[]> GetAllCargosByNomeCargoAsync(int userId, string visao, string nome);
        Task<CargoDto[]> GetAllCargosAsync(int userId, string visao);
        Task<CargoDto> GetCargoByIdAsync(int userId, string visao, int cargoId);
    } 
}