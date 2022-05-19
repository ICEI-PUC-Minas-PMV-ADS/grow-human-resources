using System.Threading.Tasks;

namespace GHR.Persistence.Interfaces.Contracts.Global
{
    public interface IGlobalPersistence
    {
        //Geral
        void Cadastrar<T>(T entity) where T : class;
        void Alterar<T>(T entity) where T : class;
        void Excluir<T>(T entity) where T : class;
        void ExcluirIntervalo<T>(T[] entity) where T : class;
        Task<bool> SalvarAsync();
    }
}