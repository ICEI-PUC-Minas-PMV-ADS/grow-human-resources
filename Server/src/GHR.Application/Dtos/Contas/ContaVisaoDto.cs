using System;
namespace GHR.Application.Dtos.Contas
{
    public class ContaVisaoDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string Funcao { get; set; }
        public string Visao { get; set; }
        public string Descricao { get; set; }
        public string ImagemURL { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean Ativa { get; set; }
        public DateTime Cadastro { get; set; }
        public DateTime? Encerramento { get; set; }
    }
}