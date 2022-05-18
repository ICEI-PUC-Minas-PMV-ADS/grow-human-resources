import { ToastrService } from 'ngx-toastr';
import { AbstractControl, AbstractControlOptions, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { User } from 'src/app/models/identity/User';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/Account.service';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})
export class CadastroComponent implements OnInit {

  user = {} as User;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
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
        Validators.maxLength(15)
      ]],
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
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
    this.user = { ...this.form.value };
    console.log(this.user)
    this.accountService.cadastrarConta(this.user).subscribe(
      () => this.router.navigateByUrl('/funcionarios'),
      (error: any) => {
        this.toastr.error(error.error, "Erro!");
        console.log(error);
        }
    )
  }
}
