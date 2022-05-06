import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-funcionario-detalhe',
  templateUrl: './funcionario-detalhe.component.html',
  styleUrls: ['./funcionario-detalhe.component.scss']
})
export class FuncionarioDetalheComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }
  public validation(): void {
    this.form = this.fb.group({
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      telefone: ['', Validators.required],
      salario: ['', Validators.required],
      dataAdmissao: ['', Validators.required],
      imagemURL: ['', Validators.required],
    });
  }
  public limparFormulario(): void {
    this.form.reset();
  }
  validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios.verificarErroCampo(campoForm);
  }
  retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }
}

