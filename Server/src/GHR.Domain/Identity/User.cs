using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace GHR.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public Funcao Funcao { get; set; }
        public Visao Visao { get; set; }
        public string ImagemUrl { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}   