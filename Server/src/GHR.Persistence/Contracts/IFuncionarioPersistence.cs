using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IFuncionarioPersistence: IGlobalPersistence
    {
        //Funcionarios
        Task<Funcionario[]> GetAllFuncionariosByNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false);
        Task<Funcionario[]> GetAllFuncionariosAsync(int userId, string visao, bool incluirMetas = false);
        Task<Funcionario> GetFuncionarioByIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false);
    }
}