using System.Threading.Tasks;
using GHR.Application.Dtos.Departamentos;

namespace GHR.Application.Services.Contracts.Departamentos
{
    public interface IDepartamentoService
    {
        Task<DepartamentoDto> CriarDepartamento(DepartamentoDto model);
        Task<DepartamentoDto> AlterarDepartamento(int departamentoId, DepartamentoDto model);
        Task<bool> ExcluirDepartamento( int departamentoId);
        Task<DepartamentoDto[]> RecuperarDepartamentosPorNomeDepartamentoAsync( string nome);
        Task<DepartamentoDto[]> RecuperarDepartamentosAsync();
        Task<DepartamentoDto> RecuperarDepartamentoPorIdAsync(int departamentoId);
    }
}