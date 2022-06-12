using Microsoft.AspNetCore.Identity;

namespace GHR.Domain.DataBase.Contas
{
    public class ContaFuncao: IdentityUserRole<int>
    {
      public Conta Contas { get; set; }    
      public Funcao Funcoes { get; set; }  
    }
}