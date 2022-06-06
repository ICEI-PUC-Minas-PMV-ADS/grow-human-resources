using System;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Dtos.Contas
{
    public class ContaDto
    {
        public int EmpresaId { get; set; }
        public EmpresaDto Empresas { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string NomeCompleto { get; set; }
        public string Funcao { get; set; }
        public string Visao { get; set; }
        public string ImagemURL { get; set; }
        public DateTime Cadastro { get; set; }
        public DateTime? Encerramento { get; set; }
        public Boolean Ativa { get; set; }
    }
}