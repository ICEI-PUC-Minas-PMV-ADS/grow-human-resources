using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Empresas;
using GHR.Domain.DataBase.Funcionarios;

namespace GHR.Domain.DataBase.Metas
{
    public class Meta
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresas { get; set; }
        public string Supervisor { get; set; }
        public string NomeMeta { get; set; }
        public string Descricao { get; set; }
        public Boolean MetaCumprida { get; set; }
        public Boolean MetaAprovada { get; set; }
        public string InicioPlanejado { get; set; }
        public string FimPlanejado { get; set; }
        public string InicioRealizado { get; set; }
        public string FimRealizado { get; set; }
        public IEnumerable<FuncionarioMeta> FuncionariosMetas { get; set; }
    }
}