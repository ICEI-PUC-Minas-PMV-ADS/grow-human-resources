using System.Threading.Tasks;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.Application.Services.Contracts.Funcionarios
{
    public interface IFuncionarioDadoPessoalService
    {
        Task<DadoPessoalDto> CriarDadoPessoalAsync(DadoPessoalDto model);

        Task<DadoPessoalDto>
        AlterarDadoPessoalAsync(
            int dadoPessoalId, DadoPessoalDto model
        );

        Task<bool> ExcluirDadoPessoalAsync(int dadoPeassoalId);

        Task<DadoPessoalDto> RecuperarDadoPessoalPorIdAsync(int dadoPessoalId);
    }
}
