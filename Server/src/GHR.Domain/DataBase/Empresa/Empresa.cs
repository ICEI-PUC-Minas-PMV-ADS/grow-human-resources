using System;

namespace GHR.Domain.DataBase.Empresa
{
    public class Empresa
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeFantadia { get; set; }
        public Boolean Ativa { get; set; } 
        public DateTime DataCadastro { get; set; }    
        public DateTime Desativacao { get; set; }    
    }
}