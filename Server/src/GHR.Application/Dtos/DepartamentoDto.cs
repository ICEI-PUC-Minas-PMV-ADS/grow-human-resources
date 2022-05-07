using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Application.Dtos
{
    public class DepartamentoDto
    {
        public int Id { get; set; }
        public string NomeDepartamento { get; set; }
        public int SupervisorId { get; set; }
        public SupervisorDto Supervisor { get; set;  }
        public int MetaId { get; set; }
        public MetaDto Meta { get; set; }
    }
}