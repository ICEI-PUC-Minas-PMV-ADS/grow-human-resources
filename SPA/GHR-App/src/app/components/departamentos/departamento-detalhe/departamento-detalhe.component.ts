import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Departamento } from 'src/app/models/departamentos/Departamento';
import { DepartamentoService } from 'src/app/services/departamentos/departamento.service';

@Component({
  selector: 'app-departamento-detalhe',
  templateUrl: './departamento-detalhe.component.html',
  styleUrls: ['./departamento-detalhe.component.scss']
})
export class DepartamentoDetalheComponent implements OnInit {

  form!: FormGroup;

  locale = 'pt-br';

  departamento = {} as Departamento;

  estadoSalvar: string = "cadastrarDepatamento";

  get f(): any
  {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private departamentoService: DepartamentoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService)
  {   }


  ngOnInit(): void
  {

    this.carregarDepartamento();
    this.validation();

  }

  public validation(): void
  {

    this.form = this.fb.group( {
        nomeDepartamento: ['', [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50)]],
        siglaDepartamento: ['', [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(5)]],});
  }

  public limparFormulario(): void
  {
    this.form.reset();
  }

  public validarCampo(campoForm: FormControl): any
  {
    return ValidadorFormularios
      .verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any
  {
    return ValidadorFormularios
      .retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public carregarDepartamento(): void
  {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');

    if (eventoIdParam !== null)
    {
      this.spinner.show();

      this.estadoSalvar = "alterarDepartamento";

      this.departamentoService
        .recuperarDepartamentoById(+eventoIdParam)
        .subscribe(
          (departamento: Departamento) => {
            this.departamento = departamento;
            this.form.patchValue(this.departamento);},

          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de departamento", "Erro!");
            console.error(error); })

        .add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void   {

    this.spinner.show();

    if (this.form.valid)     {
      this.departamento = (this.estadoSalvar === 'cadastrarDepatamento')
        ? { ...this.form.value }
        : { id: this.departamento.id, ...this.form.value };

      this.departamentoService[this.estadoSalvar](this.departamento)
        .subscribe(
          () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),

          (error: any) => {

            console.error(error);
            this.toastr.error("Erro ao salvar alterações.", "Erro!"); })
        .add(() => this.spinner.hide());
    };
  }
}


