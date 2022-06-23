using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.API.Models
{
    public class PaginacaoHeaders
    {
        public PaginacaoHeaders(int paginaAtual, int itensPorPagina, int totalItens, int totalDePaginas) 
        {
            this.PaginaAtual = paginaAtual;
            this.ItensPorPagina = itensPorPagina;
            this.TotalItens = totalItens;
            this.TotalDePaginas = totalDePaginas;

        }
                
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
        public int TotalItens { get; set; }
        public int TotalDePaginas { get; set; }
    }
}