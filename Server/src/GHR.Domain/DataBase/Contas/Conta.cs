using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GHR.Domain.DataBase.Contas
{
    public class Conta : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public string Visao { get; set; }
        public string Descricao { get; set; }
        public string ImagemURL { get; set; }
        public IEnumerable<ContaFuncao> ContasFuncoes { get; set; }
    }
}   