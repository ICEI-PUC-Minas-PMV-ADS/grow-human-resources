using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IFuncionarioPersistence
    {
        //Funcionarios
        Task<Funcionario[]> GetAllFuncionariosByNomeCompletoAsync(string nome, bool incluirMetas = false);
        Task<Funcionario[]> GetAllFuncionariosAsync(bool incluirMetas = false);
        Task<Funcionario> GetFuncionarioByIdAsync(int funcionarioId, bool incluirMetas = false);
    }
}