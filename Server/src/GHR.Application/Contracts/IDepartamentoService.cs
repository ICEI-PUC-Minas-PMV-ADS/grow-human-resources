using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IDepartamentoService
    {
        Task<DepartamentoDto> AddDepartamento(int userId, string visao, DepartamentoDto model);
        Task<DepartamentoDto> UpdateDepartamento(int userId, string visao, int departamentoId, DepartamentoDto model);
        Task<bool> DeleteDepartamento(int userId, string visao, int departamentoId);
        Task<DepartamentoDto[]> GetAllDepartamentosByNomeDepartamentoAsync(int userId, string visao, string nome);
        Task<DepartamentoDto[]> GetAllDepartamentosAsync(int userId, string visao);
        Task<DepartamentoDto> GetDepartamentoByIdAsync(int userId, string visao, int departamentoId);
    }
}