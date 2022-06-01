import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { Cargo } from 'src/app/models/cargos/Cargo';
import { Departamento } from 'src/app/models/departamentos/Departamento';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { NavegacaoEntreForms } from 'src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms';
import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';

import { CargoService } from 'src/app/services/cargos/Cargo.service';
import { DepartamentoService } from 'src/app/services/departamentos/departamento.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

@Component({
  selector: 'app-funcionario-empresa',
  templateUrl: './funcionario-empresa.component.html',
  styleUrls: ['./funcionario-empresa.component.scss']
})
export class FuncionarioEmpresaComponent implements OnInit {

  @Input() formsParametros: NavegacaoEntreForms;

  public form: FormGroup;

  public visaoRH = false;

  public funcionario = {} as Funcionario;
  public cargos: Cargo[] = [];
  public departamentos: Departamento[] = [];

  public locale = 'pt-br';


  public protegerCampoFuncionario = false;

  public funcionarioId: number;

  get ctrF(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY h:mm a',
      containerClass: 'theme-default',
      standalone: true
    };
  }

  constructor(
    private cargoService: CargoService,
    private departamentoService: DepartamentoService,
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService,
    private localeService: BsLocaleService,
    private router: Router,
    private routerActivated: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
    ) {  }

  ngOnInit(): void {

    this.visaoRH = (this.formsParametros.visaoRhLogado) ? true : false
    this.protegerCampoFuncionario = (this.visaoRH) ? false : true;

    this.validarFormularios();
    this.carregarDepartametos();
    this.carregarFuncionario();
  }

  public validarFormularios(): void {

   this.form = this.fb.group({
      id: [],
      cargoId: ['', Validators.required],
      salario: ['', Validators.required],
      dataAdmissao: ['', Validators.required],
      dataDemissao: [null],
      departamentoId: ['', Validators.required],
      supervisor: [],
      gerente: [],
      gerenteOperacionaId: [0],
      diretor: [],
      contaId: [0],
      enderecoId: [],
      dadosPessoaisId: [], });

  }
  public carregarDepartametos(): void {

    this.spinner.show();

    this.departamentoService
      .recuperarDepartamentos()
      .subscribe(
        (departamentoRetorno: ResultadoPaginacao<Departamento[]>) => {
          this.departamentos = departamentoRetorno.resultado;},

        (error: any) => {
          this.toastr.error("Não foi possível carregar os departamentos", "Erro!");
          console.error(error);})

      .add(() => this.spinner.hide());
    }

  public carregarFuncionario(): void {

    this.funcionarioId = +this.routerActivated.snapshot.paramMap.get('id');

    if (this.funcionarioId !== null && this.funcionarioId !== 0) {
      this.spinner.show();

      this.funcionarioService
        .recuperarFuncionarioPorId(this.funcionarioId)
        .subscribe(

          (funcionario: Funcionario) => {
            this.funcionario = funcionario;
            this.form.patchValue(funcionario.departamentos)
            this.form.patchValue(funcionario)
            this.recuperarCargosPorDepartamentoId(funcionario.departamentoId);
          },

          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
            console.error(error);    })

        .add(() => this.spinner.hide())
      };
    }

  public salvarDadosEmpresa(): void {

    this.funcionarioId = +this.routerActivated.snapshot.paramMap.get('id');


    this.spinner.show();

    if (this.form.valid) {
      this.funcionario = { ...this.form.value };

      this.funcionarioService
        .salvarFuncionario(this.funcionario)
        .subscribe(
          (funcionario: Funcionario) => {
            this.funcionario = funcionario;
            this.form.patchValue(this.funcionario);
            this.toastr.success("Dados da Empresa atualizado!", "Sucesso!")
          },

          (error: any) => {
            console.error(error);
            this.toastr.error("Falha ao atualizar Dados da Empresa.", "Erro!");})

        .add(() => this.spinner.hide())
     }
   }

  public validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios
      .verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios
      .retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public recuperarValorForm(funcionario: Funcionario): void {
    this.funcionario = funcionario;
  }

  public onClickDeptoId(event: any): void {
    this.recuperarCargosPorDepartamentoId(+event.value);
  }

  public recuperarCargosPorDepartamentoId(departamentoId: number): void {
    this.spinner.show

    this.cargoService
      .recuperarCargosPorDepartamentoId(departamentoId)
      .subscribe(
        (cargos: Cargo[]) => {
          this.cargos = cargos;
        },
        (error: any) => {
          this.toastr.error("Falha ao recuperar cargos por departamento.", "Erro!");
          console.error(error);
        })

      .add(() => this.spinner.hide());
    }
}
