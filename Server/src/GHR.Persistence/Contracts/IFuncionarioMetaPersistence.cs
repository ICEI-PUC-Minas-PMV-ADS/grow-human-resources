using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IFuncionarioMetaPersistence
    {
        //Funcionarios
        Task<FuncionarioMeta[]> GetMetasByFuncionarioIdAsync(int funcionarioId);
        Task<FuncionarioMeta> GetFuncionarioMetaAsync(int funcionarioId, int metaId);
    }
}