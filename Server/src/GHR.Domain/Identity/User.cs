using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GHR.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public string Funcao { get; set; }
        public string Visao { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}   