using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Metas;

namespace GHR.Domain.DataBase.Funcionarios
{
    public class FuncionarioMeta
    {
        public int Id { get; set; }
        public int MetaId { get; set; }
        public Meta Meta { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public Boolean MetaConcluida { get; set; }
        public string InicioAcordado { get; set; }
        public string FimAcordado { get; set; }
        public string InicioRealizado { get; set; }
        public string FimRealizado { get; set; }
        public string Supervisor { get; set; }
    }
}