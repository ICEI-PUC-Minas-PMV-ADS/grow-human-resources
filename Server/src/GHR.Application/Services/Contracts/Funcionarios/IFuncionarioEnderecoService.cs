using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioEnderecoService
    {
        Task<EnderecoDto> CriarEnderecoAsync(EnderecoDto model);

        Task<EnderecoDto>
        AlterarEnderecoAsync(
            int enderecoId, EnderecoDto model
        );

        Task<bool> ExcluirEnderecoAsync(int enderecoId);

        Task<EnderecoDto> RecuperarEnderecoPorIdAsync(int enderecoId);
    }
}
