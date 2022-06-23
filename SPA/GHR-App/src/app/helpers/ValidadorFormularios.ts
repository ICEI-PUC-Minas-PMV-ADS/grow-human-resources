import { AbstractControl, FormControl, FormGroup } from "@angular/forms";

export class ValidadorFormularios {
  static CompararSenha(frase: string, confirmarFrase: string): any {
    return (group: AbstractControl) => {
      const formGroup = group as FormGroup;
      const control = formGroup.controls[frase];
      const matchingControl = formGroup.controls[confirmarFrase];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        return null;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true })
      } else {
        matchingControl.setErrors(null);
      }
      return null;
    };
  }
  static verificarErroCampo(nomeCampo: FormControl): any {
    return {'is-invalid': nomeCampo.errors && nomeCampo.touched };
  }
  static retornarMensagemErro(nomeCampo: FormControl, elementoCampo: string): any {

    if (nomeCampo.errors?.["required"])
    {
      return `Este campo é obrigatório.`;
    }

    if (nomeCampo.errors?.["minlength"])
    {
      return `Este campo deve conter no mínimo ${nomeCampo.errors?.["minlength"].requiredLength} caracteres.`;
    }
    if (nomeCampo.errors?.["maxlength"])
    {
      return `Este campo deve conter no máximo ${nomeCampo.errors?.["maxlength"].requiredLength} caracteres.`;
    }
    if (nomeCampo.errors?.["max"])
    {
      return `Este campo está limitado a ${nomeCampo.errors?.["max"].max} unidades.`;
    }
    if (nomeCampo.errors?.["email"] )
    {
      return `Este campo está inválido.`;
    }

    if (elementoCampo == "Confirmar Senha")
    {

      if (nomeCampo.errors)
      {
        return "Confirmação de Senha inválido.";
        }
      }
  }
}

