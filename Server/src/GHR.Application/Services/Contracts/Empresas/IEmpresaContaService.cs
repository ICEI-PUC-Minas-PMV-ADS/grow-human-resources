using System.Collections.Generic;
using System.Threading.Tasks;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Services.Contracts.Empresas
{
    public interface IEmpresaContaService
    {
        Task<EmpresaContaDto> AtualizarEmpresa(EmpresaContaDto empresaDto);

        Task<EmpresaContaDto> CriarEmpresaContaAsync(EmpresaContaDto empresaContaDto);
        Task<EmpresaContaDto> RecuperarEmpresaContaPorEmpresaIdUserNameAsync(int empresaId, string userName);
        
    }
}
