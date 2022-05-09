using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GHR.Application.Dtos
{
    public class MetaDto
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }
        
        [Display(Name = "Meta"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]       
        public string NomeMeta { get; set; }

        [Display(Name = "Descricao Meta"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(50, ErrorMessage = "O campo {0} deve conter no mínimo 50 caracteres."),
        MaxLength(500, ErrorMessage = "O campo {0} deve conter no máximo 500 caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Meta Cumprida?"),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Boolean MetaCumprida { get; set; }

        [Display(Name = "Meta Arpovada?"),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Boolean MetaAprovada { get; set; }

        public string InicioPlanejado { get; set; }
        public string FimPlanejado { get; set; }
        public string InicioRealizado { get; set; }
        public string FimRealizado { get; set; }
        public IEnumerable<FuncionarioDto> Funcionarios { get; set; }
    }
}