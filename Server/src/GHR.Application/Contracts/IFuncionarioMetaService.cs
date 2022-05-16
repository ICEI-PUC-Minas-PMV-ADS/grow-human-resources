using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IFuncionarioMetaService
    {
        Task<FuncionarioMetaDto> AddFuncionarioMeta(int userId, string visao, FuncionarioMetaDto model);
        Task<FuncionarioMetaDto> UpdateFuncionarioMeta(int userId, string visao, int funcionarioId, int metaId, FuncionarioMetaDto model);
        Task<bool> DeleteFuncionarioMeta(int userId, string visao, int funcionarioId, int metaId);
        Task<FuncionarioMetaDto[]> GetMetasByFuncionarioIdAsync(int userId, string visao, int funcionarioId);
        Task<FuncionarioMetaDto> GetFuncionarioMetaAsync(int userId, string visao, int funcionarioId, int metaId);
    }
}