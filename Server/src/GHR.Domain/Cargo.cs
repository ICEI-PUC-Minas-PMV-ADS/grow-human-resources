using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Domain
{
    public class Cargo
    {
        public int Id { get; set; }
        public string NomeCargo { get; set; }
        public string Nivel { get; set; }
        public Boolean RecursosHumanos { get; set; }
        public int DepartamentoId { get; set; }
    }
}