using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.API.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int SetorId { get; set; }
        public float Salario { get; set; }
        public int CargoId { get; set; }
        public string DataAdmissao { get; set; }
        public string DataDemissao { get; set; }
        public string ImagemURL { get; set; }
    }
}