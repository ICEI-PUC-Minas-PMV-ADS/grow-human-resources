using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDto> AddFuncionarios(int userId, string visao, FuncionarioDto model);
        Task<FuncionarioDto> UpdateFuncionario(int userId, string visao, int funcionarioId, FuncionarioDto model);
        Task<bool> DeleteFuncionario(int userId, string visao, int funcionarioId);
        Task<FuncionarioDto[]> GetAllFuncionariosByNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false);
        Task<FuncionarioDto[]> GetAllFuncionariosAsync(int userId, string visao, bool incluirMetas = false);
        Task<FuncionarioDto> GetFuncionarioByIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false);
    }
}