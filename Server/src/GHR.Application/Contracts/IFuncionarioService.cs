using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDto> AddFuncionarios(FuncionarioDto model);
        Task<FuncionarioDto> UpdateFuncionario(int funcionarioId, FuncionarioDto model);
        Task<bool> DeleteFuncionario(int funcionarioId);
        Task<FuncionarioDto[]> GetAllFuncionariosByNomeCompletoAsync(string nome, bool incluirMetas = false);
        Task<FuncionarioDto[]> GetAllFuncionariosAsync(bool incluirMetas = false);
        Task<FuncionarioDto> GetFuncionarioByIdAsync(int funcionarioId, bool incluirMetas = false);
    }
}