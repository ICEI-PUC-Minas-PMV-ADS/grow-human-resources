using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Application.Dtos
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string DataCadastro { get; set; }
        public int FuncionarioId { get; set; }
        public FuncionarioDto Funcionario { get; set; }
    }
}