using System;
using GHR.Application.Dtos.Empresas;
using GHR.Application.Dtos.Metas;

namespace GHR.Application.Dtos.Funcionarios
{
    public class FuncionarioMetaDto
    {
      public int Id { get; set; }
      public int MetaId { get; set; }
      public MetaDto Meta { get; set; }

      public int FuncionarioId { get; set; }

      public FuncionarioDto Funcionario { get; set; }

      public Boolean MetaCumprida { get; set; }

      public DateTime InicioAcordado { get; set; }
      
      public DateTime FimAcordado { get; set; }
      
      public DateTime InicioRealizado { get; set; }
      
      public DateTime FimRealizado { get; set; }
      
      public string Supervisor { get; set; }

    }
}