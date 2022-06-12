using System;
using GHR.Domain.DataBase.Contas;

namespace GHR.Domain.DataBase.Empresas
{
    public class EmpresaConta
    {   
        public int Id { get; set; }
        public string UserName { get; set; }
        public Conta Contas { get; set; }
        public int? EmpresaId { get; set; } 
        public Empresa Empresas { get; set; }
  
    }
}