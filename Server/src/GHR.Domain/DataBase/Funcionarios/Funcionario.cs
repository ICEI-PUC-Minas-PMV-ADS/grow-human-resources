using System;
using System.Collections.Generic;
using GHR.Domain.DataBase.Cargos;
using GHR.Domain.DataBase.Contas;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Empresas;

namespace  GHR.Domain.DataBase.Funcionarios

{
    public class Funcionario
    {
        public int Id { get; set; }
        public float Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public Boolean Ativo { get; set; }
        public int? CargoId { get; set; }
        public Cargo Cargos { get; set; }
        public int? DepartamentoId { get; set; }
        public Departamento Departamentos { get; set; }
        public int? ContaId { get; set; }
        public Conta Contas { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Enderecos { get; set; }
        public int? DadosPessoaisId { get; set; }
        public DadoPessoal DadosPessoais { get; set; }                    
        public IEnumerable<FuncionarioMeta> FuncionariosMetas { get; set; }
    }
}