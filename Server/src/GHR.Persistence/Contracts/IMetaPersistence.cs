using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IMetaPersistence : IGlobalPersistence
    {
        //Metas
        Task<Meta[]> GetAllMetasByNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasByDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasByMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasByMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios = false);
        Task<Meta[]> GetAllMetasAsync(int userId, string visao, bool incluirFuncionarios = false);
        Task<Meta> GetMetaByIdAsync(int userId, string visao, int metaId, bool incluirFuncionarios = false);   
    }
}