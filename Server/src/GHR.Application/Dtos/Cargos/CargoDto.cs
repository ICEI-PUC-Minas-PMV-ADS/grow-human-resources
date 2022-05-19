using System.ComponentModel.DataAnnotations;
using GHR.Domain.DataBase.Departamentos;

namespace GHR.Application.Dtos.Cargos
{
    public class CargoDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Cargo"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]
        public string NomeCargo { get; set; }

        [Display(Name = "Funcao no Cargo"),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Funcao { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamentos { get; set; }
    }
}