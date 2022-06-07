using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDto> CriarFuncionarios(FuncionarioDto model);
        Task<FuncionarioDto> AlterarFuncionario( int funcionarioId, FuncionarioDto model);
        Task<bool> ExcluirFuncionario( int funcionarioId);
        Task<PaginaLista<FuncionarioDto>> RecuperarFuncionariosAsync( PaginaParametros paginaParametros, bool incluirMetas = false);
        Task<FuncionarioDto> RecuperarFuncionarioPorIdAsync(int funcionarioId, bool incluirMetas = false);
        Task<FuncionarioDto> RecuperarFuncionarioPorContaIdAsync(int contaId);
        Task<FuncionarioDto[]> RecuperarFuncionarioPorDepartamentoIdAsync(int departamentoId);
    }
}