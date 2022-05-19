using System.ComponentModel.DataAnnotations;

namespace GHR.Application.Dtos.Funcionarios
{
    public class Endereco
    {
      public int Id { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public float CEP { get; set; }

      [Display(Name = "Rua"), 
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Logradouro { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public int Numero { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Complemento { get; set; }

      [Required(ErrorMessage = "É necessário informa um {0}.")]
      public string Bairro { get; set; }

      [Required(ErrorMessage = "É necessário informa um {0}.")]
      public string Cidade { get; set; }
 
      [Required(ErrorMessage = "É necessário informa um {0}.")]
      public string UF { get; set; }     
 
      [Required(ErrorMessage = "É necessário informa um {0}.")]
      public string Pais { get; set; }

      [Display(Name = "Caixa Postal"),
      Required(ErrorMessage = "É necessário informa um {0}.")] 
      public string CaixaPostal { get; set; }
       }
}