using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.Identity;

namespace GHR.Application.Dtos
{
    public class FuncionarioDto
    {
      public int Id { get; set; }

      [Display(Name = "Nome"), 
      Required(ErrorMessage = "O campo {0} é obrigatório."),
      MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
      MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo 50 caracteres")]
      public string NomeCompleto { get; set; }

      [Display(Name = "Salário"), Required(ErrorMessage = "O campo {0} é obrigatório."),
      Range(100, 9999999999, ErrorMessage = "O campo {0} não pode ser inferior a R$ 100,00")]
        public float Salario { get; set; }

      [Display(Name = "Data Admissão"), 
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public DateTime DataAdmissao { get; set; }

      public string DataDemissao { get; set; }

      [Display(Name = "Foto"),
      RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "a {0} só pode ser do tipo (gif, jpg, jpeg, bmp ou png")] 
      public string ImagemURL { get; set; }

      [Display(Name = "Funcionario Ativo"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Boolean FuncionarioAtivo { get; set; }

      [Display(Name = "Cargo"),
      Required(ErrorMessage = "É necessário informa um {0}.")] 
      public int CargoId { get; set; }

      public CargoDto Cargo { get; set; }

      [Display(Name = "Departamento"),
      Required(ErrorMessage = "É necessário informa um {0}.")]
      public int DepartamentoId { get; set; }

      public DepartamentoDto DDepartamento { get; set; }

      [Display(Name = "Supervisor"),
      Required(ErrorMessage = "É necessário informa um {0}.")]
      public int SupervisorId { get; set; }


      public IEnumerable<MetaDto> Metas { get; set; }
      public int UserId { get; set; }
      public User User { get; set; }

    }
}