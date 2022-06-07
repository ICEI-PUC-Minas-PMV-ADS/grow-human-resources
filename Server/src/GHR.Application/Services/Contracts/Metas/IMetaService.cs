using System.Threading.Tasks;
using GHR.Application.Dtos.Metas;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Metas
{
    public interface IMetaService
    {
        Task<MetaDto> CriarMeta(MetaDto model);
        Task<MetaDto> AlterarMeta(int metaId, MetaDto model);
        Task<bool> ExcluirMeta(int metaId);
        Task<PaginaLista<MetaDto>> RecuperarMetasAsync(PaginaParametros paginaParametros, bool incluirFuncionarios = false);
        Task<MetaDto> RecuperarMetaPorIdAsync(int metaId, bool incluirFuncionarios = false);
        Task<MetaDto[]> RecuperarMetasAtivasAsync();
    }
}