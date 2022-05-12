using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface IFuncionarioMetaService
    {
        Task<FuncionarioMetaDto> AddFuncionarioMeta(FuncionarioMetaDto model);
        Task<FuncionarioMetaDto> UpdateFuncionarioMeta(int funcionarioId, int metaId, FuncionarioMetaDto model);
        Task<bool> DeleteFuncionarioMeta(int funcionarioId, int metaId);
        Task<FuncionarioMetaDto[]> GetMetasByFuncionarioIdAsync(int funcionarioId);
        Task<FuncionarioMetaDto> GetFuncionarioMetaAsync(int funcionarioId, int metaId);
    }
}