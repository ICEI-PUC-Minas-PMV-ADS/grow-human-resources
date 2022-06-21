using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Persistence.Models
{
    public class PaginaParametros
    {
        public const int TamanhoMaximoPagina = 50;
        public int NumeroDaPagina { get; set; } = 1;
        public int tamanhoDaPagina = 10;
        public int TamanhoDaPagina
        {
            get { return tamanhoDaPagina; }
            set { tamanhoDaPagina = (value > TamanhoMaximoPagina) ? TamanhoMaximoPagina : value; }
        }
    public string Termo { get; set; } = string.Empty;
    }
}