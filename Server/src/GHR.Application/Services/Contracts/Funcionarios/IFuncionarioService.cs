using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDto> CriarFuncionarios(int userId, string visao, FuncionarioDto model);
        Task<FuncionarioDto> AlterarFuncionario(int userId, string visao, int funcionarioId, FuncionarioDto model);
        Task<bool> ExcluirFuncionario(int userId, string visao, int funcionarioId);
        Task<FuncionarioDto[]> RecuperarFuncionariosPorNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false);
        Task<FuncionarioDto[]> RecuperarFuncionariosAsync(int userId, string visao, bool incluirMetas = false);
        Task<FuncionarioDto> RecuperarFuncionarioPorIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false);
    }
}