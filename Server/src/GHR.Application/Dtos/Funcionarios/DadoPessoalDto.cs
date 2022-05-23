using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GHR.Application.Dtos.Cargos;
using GHR.Application.Dtos.Departamentos;

namespace GHR.Application.Dtos.Funcionarios
{
    public class DadoPessoalDto
    {
      public int Id { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string CPF { get; set; }

      [Display(Name = "Título de Eleitor"), 
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string TituloEleitor { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Identidade { get; set; }

      [Display(Name = "Data Expedição"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string DataExpedicaoIdentidade { get; set; }

      [Display(Name = "Órgão Expedição"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string OrgaoExpedicaoIdentidade { get; set; }


      [Display(Name = "UF Expedição"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string UfIdentidade { get; set; }
 
      [Display(Name = "Estado Civil"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string EstadoCivil { get; set; }     
 
      [Display(Name = "Carteira de Trabalho"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string CarteiraTrabalho { get; set; }

      [Display(Name = "Data Expedicao Carteira de Trabalho"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")] 
      public string DataExpedicaoCarteiraTrabalho { get; set; }
    }
}