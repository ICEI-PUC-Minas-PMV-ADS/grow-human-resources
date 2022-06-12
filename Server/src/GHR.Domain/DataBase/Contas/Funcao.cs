using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GHR.Domain.DataBase.Contas
{
    public class Funcao : IdentityRole<int>
    {
        public string NomeFuncao { get; set; }
        public IEnumerable<ContaFuncao> ContasFuncoes { get; set; }
    }
}