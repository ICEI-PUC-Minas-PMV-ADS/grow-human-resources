using System.ComponentModel.DataAnnotations;

namespace GHR.Application.Dtos.Departamentos
{
    public class DepartamentoDto
    {
        public int Id { get; set; }

        [Display(Name = "Departamento"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]
        public string NomeDepartamento { get; set; }

        [Display(Name = "Sigla"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(2, ErrorMessage = "O campo {0} deve conter no mínimo 2 caracteres."),
        MaxLength(5, ErrorMessage = "O campo {0} deve conter no máximo 5 caracteres")]
        public string SiglaDepartamento { get; set; }
        public int MetaId { get; set; }
    }
}