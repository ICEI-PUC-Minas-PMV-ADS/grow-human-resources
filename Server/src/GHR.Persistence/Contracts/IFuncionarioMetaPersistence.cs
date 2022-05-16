using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IFuncionarioMetaPersistence : IGlobalPersistence
    {
        //Funcionarios
        Task<FuncionarioMeta[]> GetMetasByFuncionarioIdAsync(int userId, string visao, int funcionarioId);
        Task<FuncionarioMeta> GetFuncionarioMetaAsync(int userId, string visao, int funcionarioId, int metaId);
    }
}