using System;
using GHR.Application.Dtos.Empresas;
using GHR.Application.Dtos.Metas;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Domain.DataBase.Metas;

namespace GHR.Application.Dtos.Funcionarios
{
    public class FuncionarioMetaDto
    {
      public int MetaId { get; set; }
      public Meta Metas { get; set; }

      public int FuncionarioId { get; set; }

      public Funcionario Funcionarios { get; set; }

      public Boolean MetaCumprida { get; set; }

      public DateTime InicioAcordado { get; set; }
      
      public DateTime FimAcordado { get; set; }
      
      public DateTime InicioRealizado { get; set; }
      
      public DateTime FimRealizado { get; set; }
      
      public string Supervisor { get; set; }
      public Departamento Departamentos { get; set; }

    }
}