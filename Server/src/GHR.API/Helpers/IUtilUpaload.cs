using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GHR.API.Helpers
{
    public interface IUtilUpload
    {
        Task<string> SalvarImagem(IFormFile arquivoImagem, string destino);
        void ExcluirImagem(string nomeImagem, string destino); 
    }
}