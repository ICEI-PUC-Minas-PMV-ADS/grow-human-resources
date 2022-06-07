using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GHR.Application.Dtos.Cargos;
using GHR.Application.Dtos.Departamentos;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Dtos.Funcionarios
{
    public class DadoPessoalDto
    {
      public int Id { get; set; }
      public string CPF { get; set; }
      public string TituloEleitor { get; set; }
      public string Identidade { get; set; }
      public DateTime? DataExpedicaoIdentidade { get; set; }
      public string OrgaoExpedicaoIdentidade { get; set; }
      public string UfIdentidade { get; set; }
      public string EstadoCivil { get; set; }     
      public string CarteiraTrabalho { get; set; }
      public DateTime? DataExpedicaoCarteiraTrabalho { get; set; }
    }
}