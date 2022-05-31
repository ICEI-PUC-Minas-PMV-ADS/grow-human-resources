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

      public DateTime? InicioAcordado { get; set; }
      
      public DateTime? FimAcordado { get; set; }
      
      public DateTime? InicioRealizado { get; set; }
      
      public DateTime? FimRealizado { get; set; }
      
      public string Supervisor { get; set; }

    }
}