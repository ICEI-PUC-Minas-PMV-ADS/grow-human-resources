using System.Threading.Tasks;
using GHR.Application.Dtos.Departamentos;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Departamentos
{
    public interface IDepartamentoService
    {
        Task<DepartamentoDto> CriarDepartamento(DepartamentoDto model);
        Task<DepartamentoDto> AlterarDepartamento(int departamentoId, int empresaId, DepartamentoDto model);
        Task<bool> ExcluirDepartamento( int departamentoId, int empresaId);
        Task<PaginaLista<DepartamentoDto>> RecuperarDepartamentosAsync(int empresaId, PaginaParametros paginaParametros);
        Task<DepartamentoDto> RecuperarDepartamentoPorIdAsync(int departamentoId, int empresaId);
    }
}