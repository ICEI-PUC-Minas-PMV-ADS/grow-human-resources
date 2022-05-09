using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IMetaService
    {
        Task<MetaDto> AddMeta(MetaDto model);
        Task<MetaDto> UpdateMeta(int metaId, MetaDto model);
        Task<bool> DeleteMeta(int metaId);
        Task<MetaDto[]> GetAllMetasByDescricaoMetaAsync(string descricao, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasByMetaAprovadaAsync(bool metaAprovada, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasByMetaCumpridaAsync(bool metaCumprida, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasByNomeMetaAsync(string nome, bool incluirFuncionarios = false);
        Task<MetaDto[]> GetAllMetasAsync(bool incluirFuncionarios = false);
        Task<MetaDto> GetMetaByIdAsync(int metaId, bool incluirFuncionarios = false);
    }
}