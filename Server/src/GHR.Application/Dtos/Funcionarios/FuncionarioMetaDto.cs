using System;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Domain.DataBase.Metas;

namespace GHR.Application.Dtos.Funcionarios
{
    public class FuncionarioMetaDto
    {
      public int MetaId { get; set; }

      public Meta Meta { get; set; }

      public int FuncionarioId { get; set; }

      public Funcionario Funcionario { get; set; }

      public Boolean MetaCumprida { get; set; }

      public string InicioAcordado { get; set; }
      
      public string FimAcordado { get; set; }
      
      public string InicioRealizado { get; set; }
      
      public string FimRealizado { get; set; }
      
      public string Supervisor { get; set; }

    }
}