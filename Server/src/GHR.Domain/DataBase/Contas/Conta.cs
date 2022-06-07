using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using GHR.Domain.DataBase.Empresas;

namespace GHR.Domain.DataBase.Contas
{
    public class Conta : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public string Visao { get; set; }
        public string Descricao { get; set; }
        public string ImagemURL { get; set; }
        public DateTime Cadastro { get; set; }
        public DateTime? Encerramento { get; set; }
        public Boolean Ativa { get; set; }
        public IEnumerable<ContaFuncao> ContasFuncoes { get; set; }
        public IEnumerable<EmpresaConta> EmpresasContas { get; set; }
    }
}   