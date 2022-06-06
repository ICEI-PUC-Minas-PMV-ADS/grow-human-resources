using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Dtos.Contas
{
    public class ContaLoginDto
    {
        public int EmpresaId { get; set; }
        public EmpresaDto Empresas { get; set; }
        public string UserName { get; set; }    
        public string Password { get; set; }
    }
}