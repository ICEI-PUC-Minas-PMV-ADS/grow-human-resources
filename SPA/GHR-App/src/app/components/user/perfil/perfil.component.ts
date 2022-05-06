import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

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
      cargo: ['', Validators.required],
      nome: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      telefone: ['', Validators.required],
      sobreMim: ['', Validators.required],
      dataAdmissao: ['', Validators.required],
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
