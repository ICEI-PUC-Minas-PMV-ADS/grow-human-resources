using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Application.Dtos
{
    public class SupervisorDto
    {
        public int Id { get; set; }
        public int MetaId { get; set; }
        public int FuncionarioId { get; set; }
        public int DepartamentoId { get; set; }
    }
}