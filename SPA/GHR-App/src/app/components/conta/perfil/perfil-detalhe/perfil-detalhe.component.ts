import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NavigationEnd, provideRoutes, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { Conta } from 'src/app/models/contas/Conta';

import { ContaService } from 'src/app/services/contas/Conta.service';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { ContaVisao } from 'src/app/models/contas/ContaVisao';

@Component({
  selector: 'app-perfil-detalhe',
  templateUrl: './perfil-detalhe.component.html',
  styleUrls: ['./perfil-detalhe.component.scss']
})
export class PerfilDetalheComponent implements OnInit {

  @Output() changeFormValue = new EventEmitter();

  public conta = {} as Conta;
  public contaAtiva = {} as ContaAtiva;
  public contaVisao = {} as ContaVisao;

  form: FormGroup;

  get ctrForm(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private contaService: ContaService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) {
      router.events.subscribe(
        (validaContaAtiva) => {
          if (validaContaAtiva instanceof NavigationEnd) {
            this.contaService.contaAtual$.subscribe(
              (value) => {
                this.contaAtiva = { ...value };})
          }});
      }

  ngOnInit() {

    this.validadarFormulario();
    this.consultarConta();
  }



  public validadarFormulario(): void {

    this.form = this.fb.group({

      userName: [''],
      id: [],
      visao: [],
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)]],
      email: ['', [
        Validators.required,
        Validators.email]],
      phoneNumber: ['', Validators.required],
      descricao: ['', Validators.required],
      imagemURL: [],
    });
    this.verificarFormulario();
  }

  public verificarFormulario(): void {
    this.form.valueChanges.subscribe(
      () => this.changeFormValue.emit({ ... this.form.value }));
  }

  validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios
      .verificarErroCampo(campoForm);
  }

  retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios
      .retornarMensagemErro(nomeCampo, nomeElemento);
  }

  private consultarConta(): void {

    this.spinner.show();

    this.contaService
      .recuperarConta()
      .subscribe(
        (conta: Conta) => {

          this.conta = conta;
          this.form.patchValue(this.conta);
          this.toastr.success("Consulta de conta realizada.", "Sucesso!");},

        (error) => {

          console.error(error);
          this.toastr.error("Falaha na carga de usuário", "Erro!"); })
      .add(() => this.spinner.hide());
  }

  public onSubmit(): void {
    this.atualizarConta();
  }

  public atualizarConta() {

    this.contaVisao = { ...this.form.value };

    this.spinner.show();
    this.contaService.atualizarConta(this.contaVisao).subscribe(

      () => this.toastr.success("Conta atualizada!", "Sucesso!"),

      (error) => {

        this.toastr.error(error.error);
        console.error(error);})

      .add(() => this.spinner.hide())
  }


}
