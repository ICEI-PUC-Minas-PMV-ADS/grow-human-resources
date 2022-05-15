using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Domain
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public float Salario { get; set; }
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public string DataAdmissao { get; set; }
        public string DataDemissao { get; set; }
        public string ImagemURL { get; set; }
        public Boolean FuncionarioAtivo { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
        public int LoginId { get; set; }
        public Login Login { get; set; }
        public IEnumerable<FuncionarioMeta> FuncionariosMetas { get; set; }
    }
}