using System.Threading.Tasks;
using GHR.Application.Dtos.Metas;

namespace GHR.Application.Serices.Contracts.Metas
{
    public interface IMetaService
    {
        Task<MetaDto> CriarMeta(int userId, string visao, MetaDto model);
        Task<MetaDto> AlterarMeta(int userId, string visao, int metaId, MetaDto model);
        Task<bool> ExcluirMeta(int userId, string visao, int metaId);
        Task<MetaDto[]> RecuperarMetasPorDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false);
        Task<MetaDto[]> RecuperarMetasPorMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios = false);
        Task<MetaDto[]> RecuperarMetasPorMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios = false);
        Task<MetaDto[]> RecuperarMetasPorNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = false);
        Task<MetaDto[]> RecuperarMetasAsync(int userId, string visao, bool incluirFuncionarios = false);
        Task<MetaDto> RecuperarMetaPorIdAsync(int userId, string visao, int metaId, bool incluirFuncionarios = false);
    }
}