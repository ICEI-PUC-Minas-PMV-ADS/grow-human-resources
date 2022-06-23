using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Persistence.Interfaces.Contracts.Funcionarios
{
    public interface IFuncionarioDadoPessoalPersistence: IGlobalPersistence
    {
        Task<DadoPessoal> RecuperarDadosPessoaisPorIdAsync(int id);
    }
}