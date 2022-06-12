using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GHR.API.Helpers
{
    public class UtilUpload : IUtilUpload
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UtilUpload(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public void ExcluirImagem(string nomeImagem, string destino)
        {
            if (!string.IsNullOrEmpty(nomeImagem)) {

                var imagemCaminho = Path.Combine(_hostEnvironment.ContentRootPath, @$"Recursos/{destino}", nomeImagem);

                if (System.IO.File.Exists(imagemCaminho))
                    System.IO.File.Delete(imagemCaminho);
            }
        }

        public async Task<string> SalvarImagem(IFormFile arquivoImagem, string destino)
        {
            string nomeImagem = new String(Path.GetFileNameWithoutExtension(arquivoImagem.FileName)
                .Take(15)
                .ToArray()
                ).Replace(' ', '-');

            nomeImagem = $"{nomeImagem}{DateTime.UtcNow:yymmssfff}{Path.GetExtension(arquivoImagem.FileName)}";

            var imagemCaminho = Path.Combine(_hostEnvironment.ContentRootPath, @$"Recursos/{destino}", nomeImagem);

            using (var fileStream = new FileStream(imagemCaminho, FileMode.Create)) {
                await arquivoImagem.CopyToAsync(fileStream);
            }
            
            return nomeImagem;
        }
    }
}