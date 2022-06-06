using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GHR.Application.Dtos.Cargos;
using GHR.Application.Dtos.Contas;
using GHR.Application.Dtos.Departamentos;
using GHR.Application.Dtos.Empresas;
using GHR.Application.Dtos.Metas;
using GHR.Domain.DataBase.Cargos;
using GHR.Domain.DataBase.Contas;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Funcionarios;

namespace GHR.Application.Dtos.Funcionarios
{
    public class FuncionarioDto
    {             
      public int Id { get; set; }
     public int EmpresaId { get; set; }
     public EmpresaDto Empresas { get; set; }

//      [Display(Name = "Salário"), Required(ErrorMessage = "O campo {0} é obrigatório."),
 //     Range(100, 9999999999, ErrorMessage = "O campo {0} não pode ser inferior a R$ 100,00")]
      public float Salario { get; set; }

 //     [Display(Name = "Data Admissão"), 
 //     Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public DateTime DataAdmissao { get; set; }

      public DateTime DataDemissao { get; set; }

 //     [Display(Name = "Funcionario Ativo"),
 //     Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public Boolean FuncionarioAtivo { get; set; }

 //     [Display(Name = "Cargo"),
 //     Required(ErrorMessage = "É necessário informa um {0}.")] 
      public int CargoId { get; set; }
      public CargoDto Cargos { get; set; }

 //     [Display(Name = "Departamento"),
 //     Required(ErrorMessage = "É necessário informa um {0}.")]
      public int DepartamentoId { get; set; }
      public DepartamentoDto Departamentos { get; set; }
      
//      [Display(Name = "Conta"),
//     Required(ErrorMessage = "É necessário informa um {0}.")]
      public int ContaId { get; set; }
      public ContaDto Contas { get; set; }


 //     [Display(Name = "Endereco"),
 //     Required(ErrorMessage = "É necessário informa um {0}.")]
      public int EnderecoId { get; set; }
      public EnderecoDto   Enderecos { get; set; }


 //     [Display(Name = "Dados Pessoais"),
 //     Required(ErrorMessage = "É necessário informa {0}.")]
      public int DadosPessoaisId { get; set; }
       public DadoPessoalDto DadosPessoais { get; set; }

      public IEnumerable<MetaDto> Metas { get; set; }
       }
}