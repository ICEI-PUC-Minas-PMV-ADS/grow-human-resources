using System.Collections.Generic;
using System.Threading.Tasks;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Services.Contracts.Empresas
{
    public interface IEmpresaService
    {
        Task<EmpresaDto> AtualizarEmpresa(EmpresaDto empresaDto);

        Task<EmpresaDto> CriarEmpresaAsync(EmpresaDto empresaDto);

        Task<EmpresaDto> RecuperarEmpresaPorIdAsync(int empresaId);

        Task<IEnumerable<EmpresaDto>> RecuperarEmpresasAsync();
    }
}
