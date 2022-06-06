using System;
using System.ComponentModel.DataAnnotations;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Dtos.Departamentos
{
    public class DepartamentoDto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public EmpresaDto Empresas { get; set; }

        [Display(Name = "Departamento"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]
        public string NomeDepartamento { get; set; }

        [Display(Name = "Sigla"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(2, ErrorMessage = "O campo {0} deve conter no mínimo 2 caracteres."),
        MaxLength(7, ErrorMessage = "O campo {0} deve conter no máximo 5 caracteres")]
        public string SiglaDepartamento { get; set; }
        public int MetaId { get; set; }
        public string Diretor { get; set; }
        public string Gerente { get; set; }    
        public string Supervisor { get; set; }     
        public DateTime DataHoraCriacao { get; set; }   
        public DateTime DataHoraEncerramento { get; set; }
        public Boolean Ativo { get; set; }   
    }
}