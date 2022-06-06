using System;
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
        public int UserId { get; set; }
        public Conta Contas { get; set; }
    }
}