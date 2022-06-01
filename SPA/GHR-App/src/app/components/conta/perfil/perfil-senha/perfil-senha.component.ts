import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Conta } from 'src/app/models/contas/Conta';

import { ContaService } from 'src/app/services/contas/Conta.service';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

@Component({
  selector: 'app-perfil-senha',
  templateUrl: './perfil-senha.component.html',
  styleUrls: ['./perfil-senha.component.scss']
})

export class PerfilSenhaComponent implements OnInit {

  public conta = {} as Conta;

  form: FormGroup;

  get ctrForm(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private contaService: ContaService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.validation();
    this.consultarContaAtiva();
  }

  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidadorFormularios
        .CompararSenha('password', 'confirmarPassword')};

    this.form = this.fb.group({
      userName: [''],
      id: [],
      visao: [],
      nomeCompleto: [''],
      email: [''],
      phoneNumber: [''],
      descricao: [''],
      password: ['',[
        Validators.required,
        Validators.minLength(6)]],
      confirmarPassword: [''] }, formOptions);
  }

  validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios
      .verificarErroCampo(campoForm);
  }

  retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios
      .retornarMensagemErro(nomeCampo, nomeElemento);
  }

  private consultarContaAtiva(): void {

    this.spinner.show();

    this.contaService
      .recuperarContaAtiva()
      .subscribe(
        (contaConsulta: Conta) => {
          this.conta = contaConsulta;
          this.form.patchValue(this.conta);
        },

        (error) => {
          console.error(error);
          this.toastr.error("Falaha na carga de usuário", "Erro!");
        })

      .add(() => this.spinner.hide());
  }

  public onSubmit(): void {
    this.atualizarConta();
  }

  public atualizarConta() {

    this.conta = { ...this.form.value };

    this.spinner.show();

    this.contaService
      .alterarConta(this.conta)
      .subscribe(
        () => this.toastr.success("Conta atualizada!", "Sucesso!"),

        (error) => {
          this.toastr.error(error.error);
          console.error(error); })
      .add(() => this.spinner.hide())
  }
}
