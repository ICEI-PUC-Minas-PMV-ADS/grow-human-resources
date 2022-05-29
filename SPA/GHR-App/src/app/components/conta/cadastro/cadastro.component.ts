import { ToastrService } from 'ngx-toastr';
import { AbstractControl, AbstractControlOptions, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { Router } from '@angular/router';
import { ContaService } from 'src/app/services/contas/Conta.service';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})
export class CadastroComponent implements OnInit {

  contaCadastro = {} as ContaAtiva;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private accountService: ContaService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
  this.validation();
  }
  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidadorFormularios.CompararSenha('password', 'confirmarPassowrd')
    };

    this.form = this.fb.group({
      userName: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(15)]],
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)]],
      email: ['', [
        Validators.required,
        Validators.email ]],
      password: ['', [
        Validators.required,
        Validators.minLength(6)]],
      confirmarPassowrd: ['', Validators.required],
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

  cadastrarConta(): void {

    this.contaCadastro = { ...this.form.value };

    this.accountService
      .cadastrarConta(this.contaCadastro)
      .subscribe(
        () => this.router.navigateByUrl('/home'),

        (error: any) => {

          this.toastr.error(error.error, "Erro!");
          console.log(error);  })
  }
}
