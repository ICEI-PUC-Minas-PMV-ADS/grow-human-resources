import { UserUpdate } from './../../../models/identity/UserUpdate';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/Account.service';
import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { NavigationEnd, provideRoutes, Router } from '@angular/router';
import { User } from 'src/app/models/identity/User';
@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  userUpdate = {} as UserUpdate;
  contaLogada = false;
  userLogado = {} as User;
  form: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) {
    router.events.subscribe(
      (val) => {
        if (val instanceof NavigationEnd) {
          this.accountService.currentUser$.subscribe(
            (value) => {
              this.contaLogada = value !== null;
              this.userLogado = { ...value };
              console.log(value.visao);
            }
          )
          console.log(this.contaLogada, this.userLogado, this.userLogado.visao);
        }
      }
    );
  }

  ngOnInit() {
    this.validation();
    this.consultarUsuario();
  }
  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidadorFormularios.CompararSenha('password', 'confirmarPassword')
    };

    this.form = this.fb.group({
      cargo: [''],
      userName: [''],
      funcao: [''],
      visao: [''],
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)]],
      email: ['', [
        Validators.required,
        Validators.email]],
      phoneNumber: ['', Validators.required],
      descricao: ['', Validators.required],
      dataAdmissao: [''],
      password: ['', Validators.minLength(6)],
      confirmarPassword: [''],
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

  private consultarUsuario(): void {

    this.spinner.show();

    this.accountService.getUser().subscribe(
      (userRetorno: UserUpdate) => {
        console.log(userRetorno);
        this.userUpdate = userRetorno;
        this.form.patchValue(this.userUpdate);
        this.toastr.success("Consulta de conta realizada.", "Sucesso!");
      },
      (error) => {
        console.error(error);
        this.toastr.error("Falaha na carga de usuário", "Erro!");
      }
    ).add(() => this.spinner.hide());
  }
  public onSubmit(): void {
    this.atualizarConta();
  }
  public atualizarConta() {

    this.userUpdate = { ...this.form.value };

    this.spinner.show();

    this.accountService.updateUser(this.userUpdate).subscribe(
      () => this.toastr.success("Conta atualizada!", "Sucesso!"),
      (error) => {
        this.toastr.error(error.error);
        console.error(error);
       }
    ).add(() => this.spinner.hide())
  }
}
