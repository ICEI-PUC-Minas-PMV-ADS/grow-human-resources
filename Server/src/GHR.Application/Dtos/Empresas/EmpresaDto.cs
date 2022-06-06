using System;

namespace GHR.Application.Dtos.Empresas
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public Boolean Ativa { get; set; } 
        public DateTime DataCadastro { get; set; }    
        public DateTime? Desativacao { get; set; }   
        public string Logotipo { get; set; } 
    
    }
}