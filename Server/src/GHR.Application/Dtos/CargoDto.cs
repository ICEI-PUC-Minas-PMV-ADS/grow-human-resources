using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Application.Dtos
{
    public class CargoDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Cargo"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]
        public string NomeCargo { get; set; }
    }
}