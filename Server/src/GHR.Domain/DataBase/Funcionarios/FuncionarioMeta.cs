using System;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Metas;

namespace GHR.Domain.DataBase.Funcionarios
{
    public class FuncionarioMeta
    {
        public int MetaId { get; set; }
        public Meta Metas { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionarios { get; set; }
        public Boolean MetaCumprida { get; set; }
        public string InicioAcordado { get; set; }
        public string FimAcordado { get; set; }
        public string InicioRealizado { get; set; }
        public string FimRealizado { get; set; }
        public string Supervisor { get; set; }
        public Departamento Departamentos { get; set; }
    }
}