using System;
using GHR.Domain.DataBase.Contas;
using GHR.Domain.DataBase.Empresas;

namespace GHR.Application.Dtos.Empresas
{
    public class EmpresaContaDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Conta Contas { get; set; }
        public int EmpresaId { get; set; } 
        public Empresa Empresas { get; set; }
        public Boolean Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }    
        public DateTime? Desativacao { get; set; }   
    }
}