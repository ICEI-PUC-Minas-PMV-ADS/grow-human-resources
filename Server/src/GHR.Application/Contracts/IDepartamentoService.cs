using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IDepartamentoService
    {
        Task<DepartamentoDto> AddDepartamento(DepartamentoDto model);
        Task<DepartamentoDto> UpdateDepartamento(int departamentoId, DepartamentoDto model);
        Task<bool> DeleteDepartamento(int departamentoId);
        Task<DepartamentoDto[]> GetAllDepartamentosByNomeDepartamentoAsync(string nome);
        Task<DepartamentoDto[]> GetAllDepartamentosAsync();
        Task<DepartamentoDto> GetDepartamentoByIdAsync(int departamentoId);
    }
}