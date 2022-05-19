using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Domain.DataBase.Metas;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Implements.Contracts.Metas
{
    public interface IMetaPersistence : IGlobalPersistence
    {
        //Metas
        Task<Meta[]> RecuperarMetasPorNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = false);
        Task<Meta[]> RecuperarMetasPorDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false);
        Task<Meta[]> RecuperarMetasPorMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios = false);
        Task<Meta[]> RecuperarMetasPorMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios = false);
        Task<Meta[]> RecuperarMetasAsync(int userId, string visao, bool incluirFuncionarios = false);
        Task<Meta> RecuperarMetaPorIdAsync(int metaId, bool incluirFuncionarios = false);   
    }
}