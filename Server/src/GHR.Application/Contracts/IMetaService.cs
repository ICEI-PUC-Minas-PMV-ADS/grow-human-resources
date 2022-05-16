using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IMetaService
    {
        Task<MetaDto> AddMeta(int userId, string visao, MetaDto model);
        Task<MetaDto> UpdateMeta(int userId, string visao, int metaId, MetaDto model);
        Task<bool> DeleteMeta(int userId, string visao, int metaId);
        Task<MetaDto[]> GetAllMetasByDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasByMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasByMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasByNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasAsync(int userId, string visao, bool incluirFuncionarios = false);
        Task<MetaDto> GetMetaByIdAsync(int userId, string visao, int metaId, bool incluirFuncionarios = false);
    }
}