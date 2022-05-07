using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Application.Dtos
{
    public class MetaDto
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }
        public SupervisorDto Supervisor { get; set; }
        public string NomeMeta { get; set; }
        public string Descricao { get; set; }
        public Boolean MetaCumprida { get; set; }
        public Boolean MetaAprovada { get; set; }
        public string InicioPlanejado { get; set; }
        public string FimPlanejado { get; set; }
        public string InicioRealizado { get; set; }
        public string FimRealizado { get; set; }
        public IEnumerable<FuncionarioDto> Funcionarios { get; set; }
    }
}