using System;
using System.Collections.Generic;
using System.Dynamic;
using GHR.Domain.DataBase.Contas;

namespace GHR.Domain.DataBase.Empresas
{
    public class Empresa
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public Boolean Ativa { get; set; } 
        public DateTime DataCadastro { get; set; }    
        public DateTime? Desativacao { get; set; }    
        public string Logotipo { get; set; }
        public string SiglaEmpresa { get; set; }
        public Boolean Filial { get; set; }
        public  int? MatrizId { get; set; }
        public IEnumerable<EmpresaConta> EmpresasContas { get; set; }

    }
}