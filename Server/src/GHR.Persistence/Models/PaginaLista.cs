using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Models
{
    public class PaginaLista<T> : List<T>
    {
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
        public int TamanhoDaPagina { get; set; }
        public int ContadorTotal { get; set; }

        public PaginaLista()
        {
            
        }

        public PaginaLista(List<T> itens, int contador, int numeroDaPagina, int tamanhoDaPagina)
        {
            ContadorTotal = contador;
            TamanhoDaPagina = tamanhoDaPagina;
            PaginaAtual = numeroDaPagina;
            TotalDePaginas = (int)Math.Ceiling(contador / (double)tamanhoDaPagina);
            AddRange(itens);
        }
        public static async Task<PaginaLista<T>> CriarPaginaAsync(
            IQueryable<T> source, int numeroDaPagina, int tamanhoDaPagina) 
        {
            var contador = await source.CountAsync();
            var itens = await source.Skip((numeroDaPagina - 1) * tamanhoDaPagina)
                                    .Take(tamanhoDaPagina)
                                    .ToListAsync();
            return new PaginaLista<T>(itens, contador, numeroDaPagina, tamanhoDaPagina);
        }
    }
}