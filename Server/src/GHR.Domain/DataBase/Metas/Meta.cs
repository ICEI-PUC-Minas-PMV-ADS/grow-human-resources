using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;

namespace GHR.Domain.DataBase.Metas
{
    public class Meta
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }
        public int UserId { get; set; }
        public string NomeMeta { get; set; }
        public string Descricao { get; set; }
        public Boolean MetaCumprida { get; set; }
        public Boolean MetaAprovada { get; set; }
        public DateTime? InicioPlanejado { get; set; }
        public DateTime? FimPlanejado { get; set; }
        public DateTime? InicioRealizado { get; set; }
        public DateTime? FimRealizado { get; set; }
        public IEnumerable<FuncionarioMeta> FuncionariosMetas { get; set; }
    }
}