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
        public Cargo Cargo { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string ImagemURL { get; set; }
        public Boolean FuncionarioAtivo { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public Supervisor Supervisor { get; set; }
        public Login Login { get; set; }
        public IEnumerable<FuncionarioMeta> FuncionariosMetas { get; set; }
    }
}