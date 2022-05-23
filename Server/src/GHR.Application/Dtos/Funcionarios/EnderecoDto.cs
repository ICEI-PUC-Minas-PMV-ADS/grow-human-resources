using System.ComponentModel.DataAnnotations;

namespace GHR.Application.Dtos.Funcionarios
{
    public class EnderecoDto
    {
      public int Id { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string CEP { get; set; }

      [Display(Name = "Rua"), 
      Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Logradouro { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Numero { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Complemento { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Bairro { get; set; }

      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Cidade { get; set; }
 
      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string UF { get; set; }     
 
      [Required(ErrorMessage = "O campo {0} é obrigatório.")]
      public string Pais { get; set; }

      [Display(Name = "Caixa Postal"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")] 
      public string CaixaPostal { get; set; }

      [Display(Name = "Complemento Endereco"),
      Required(ErrorMessage = "O campo {0} é obrigatório.")] 
      public string ComplementoEndereco { get; set; }
       }
}