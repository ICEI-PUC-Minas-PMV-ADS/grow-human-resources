using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

      [Display(Name = "e-mail"), 
      Required(ErrorMessage = "O campo {0} é obrigatório."),
      EmailAddress(ErrorMessage = "Campo {0} inválido.")]
            public string Email { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório."),
      Phone(ErrorMessage = "O campo {0} está inválido")]
      public string Telefone { get; set; }

      [Display(Name = "Salário"), Required(ErrorMessage = "O campo {0} é obrigatório."),
      Range(100, 9999999999, ErrorMessage = "O campo {0} não pode ser inferior a R$ 100,00")]
        public float Salario { get; set; }

      [Display(Name = "Data Admissão"), 
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string DataAdmissao { get; set; }

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

      public SupervisorDto Supervisor { get; set; }

      [Display(Name = "Login"),
      Required(ErrorMessage = "É necessário informa um {0}.")] 
      public int LoginId { get; set; }

      public LoginDto Login { get; set; }

      public IEnumerable<MetaDto> Metas { get; set; }

    }
}