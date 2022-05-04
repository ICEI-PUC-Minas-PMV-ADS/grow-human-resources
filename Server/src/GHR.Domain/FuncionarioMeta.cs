using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Domain
{
    public class FuncionarioMeta
    {
        public int MetaId { get; set; }
        public Meta Meta { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public Boolean MetaCumprida { get; set; }
        public DateTime? InicioAcordado { get; set; }
        public DateTime? FimAcordado { get; set; }
        public DateTime? InicioRealizado { get; set; }
        public DateTime? FimRealizado { get; set; }
        public int? SupervisorId { get; set; }
    }
}