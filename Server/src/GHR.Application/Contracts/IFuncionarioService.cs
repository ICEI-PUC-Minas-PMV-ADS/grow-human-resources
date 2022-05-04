using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Application.Contracts
{
    public interface IFuncionarioService
    {
        Task<Funcionario> AddFuncionarios(Funcionario model);
        Task<Funcionario> UpdateFuncionario(int funcionarioId, Funcionario model);
        Task<bool> DeleteFuncionario(int funcionarioId);
        Task<Funcionario[]> GetAllFuncionariosByNomeCompletoAsync(string nome, bool incluirMetas = false);
        Task<Funcionario[]> GetAllFuncionariosAsync(bool incluirMetas = false);
        Task<Funcionario> GetFuncionarioByIdAsync(int funcionarioId, bool incluirMetas = false);
    }
}