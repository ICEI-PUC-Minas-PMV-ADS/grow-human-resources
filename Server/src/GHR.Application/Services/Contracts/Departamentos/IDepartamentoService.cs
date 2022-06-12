using System.Threading.Tasks;
using GHR.Application.Dtos.Departamentos;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Departamentos
{
    public interface IDepartamentoService
    {
        Task<DepartamentoDto> CriarDepartamento(DepartamentoDto model);
        Task<DepartamentoDto> AlterarDepartamento(int departamentoId, DepartamentoDto model);
        Task<bool> ExcluirDepartamento( int departamentoId);
        Task<PaginaLista<DepartamentoDto>> RecuperarDepartamentosAsync(PaginaParametros paginaParametros);
        Task<DepartamentoDto> RecuperarDepartamentoPorIdAsync(int departamentoId);
    }
}