using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDto> CriarFuncionarios(FuncionarioDto model);
        Task<FuncionarioDto> AlterarFuncionario( int funcionarioId, int empresaId, FuncionarioDto model);
        Task<bool> ExcluirFuncionario( int funcionarioId, int empresaId);
        Task<PaginaLista<FuncionarioDto>> RecuperarFuncionariosAsync(int empresaId, PaginaParametros paginaParametros, bool incluirMetas = false);
        Task<FuncionarioDto> RecuperarFuncionarioPorIdAsync(int funcionarioId, int empresaId, bool incluirMetas = false);
        Task<FuncionarioDto> RecuperarFuncionarioPorContaIdAsync(int contaId, int empresaId);
        Task<FuncionarioDto[]> RecuperarFuncionarioPorDepartamentoIdAsync(int departamentoId, int empresaId);
    }
}