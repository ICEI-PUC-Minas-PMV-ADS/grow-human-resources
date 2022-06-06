using System;
using System.ComponentModel.DataAnnotations;
using GHR.Application.Dtos.Departamentos;
using GHR.Application.Dtos.Empresas;

namespace GHR.Application.Dtos.Cargos
{
    public class CargoDto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public EmpresaDto Empresas { get; set; }
        
        [Display(Name = "Cargo"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]
        public string NomeCargo { get; set; }

        [Display(Name = "Funcao no Cargo"),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Funcao { get; set; }
        public int DepartamentoId { get; set; }
        public DepartamentoDto Departamentos { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEncerramento { get; set; }
    }
}