using System.Text.Json;
using GHR.API.Models;
using Microsoft.AspNetCore.Http;

namespace GHR.API.Extensions
{
    public static class Paginacao
    {
        public static void CriarPaginacao(this HttpResponse retorno, 
            int paginaAtual, int itensPorPagina, int totalItens, int totalDePaginas)
        {
            var paginacao = new PaginacaoHeaders(paginaAtual,
                itensPorPagina,
                totalItens,
                totalDePaginas);
        

            var options = new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            retorno.Headers.Add("Paginacao", JsonSerializer.Serialize(paginacao, options));
            retorno.Headers.Add("Access-Control-Expose-Headers", "Paginacao");
        }
    }
}