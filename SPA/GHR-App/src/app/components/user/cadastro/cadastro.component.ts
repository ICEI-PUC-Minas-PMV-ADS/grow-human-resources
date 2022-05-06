import { AbstractControl, AbstractControlOptions, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})
export class CadastroComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  this.validation();
  }
  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidadorFormularios.CompararSenha('senha', 'confirmarSenha')
    };

    this.form = this.fb.group({
      usuario: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(15)
      ]],
      nome: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      senha: ['', [
        Validators.required,
        Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required],
    }, formOptions);
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
