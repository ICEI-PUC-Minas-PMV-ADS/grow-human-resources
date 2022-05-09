using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IMetaPersistence
    {
        //Metas
        Task<Meta[]> GetAllMetasByNomeMetaAsync(string nome, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasByDescricaoMetaAsync(string descricao, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasByMetaCumpridaAsync(bool metaCumprida, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasByMetaAprovadaAsync(bool metaAprovada, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasAsync(bool incluirFuncionarios = false);
        Task<Meta> GetMetaByIdAsync(int metaId, bool incluirFuncionarios = false);   
    }
}