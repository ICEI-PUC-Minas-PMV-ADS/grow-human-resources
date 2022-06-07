
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GHR.API.Helpers
{
    public interface IUtilUpload
    {
        void ExcluirImagem(string nomeImagem, string destino); 
        Task<string> SalvarImagem(IFormFile arquivoImagem, string destino);
    }
}