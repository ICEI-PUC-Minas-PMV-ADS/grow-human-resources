import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute} from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { DadoPessoal } from 'src/app/models/funcionarios/DadoPessoal';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { NavegacaoEntreForms } from 'src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms';

import { DadosPessoaisService } from 'src/app/services/funcionarios/dadosPessoais.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

@Component({
  selector: 'app-funcionario-dados-pessoais',
  templateUrl: './funcionario-dados-pessoais.component.html',
  styleUrls: ['./funcionario-dados-pessoais.component.scss']
})
export class FuncionarioDadosPessoaisComponent implements OnInit {

  @Input() formsParametros: NavegacaoEntreForms;

  public form: FormGroup;

  public visaoRH = false;

  public funcionario = {} as Funcionario;
  public dadosPessoais = {} as DadoPessoal;

  public locale = 'pt-br';

  public protegerCampoDadosPessoais = false;
  public modoEditar = false;

  public funcionarioId: number;

  get ctrDP(): any {
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
    private dadosPessoaisService: DadosPessoaisService,
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService,
    private localeService: BsLocaleService,
    private router: Router,
    private routerActivated: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {

    this.visaoRH = (this.formsParametros.visaoRhLogado) ? true : false
    this.protegerCampoDadosPessoais = (this.visaoRH) ? false : true;

    this.validarFormularios();
    this.carregarDadosPessoais();
  }

  public validarFormularios(): void {

    this.form = this.fb.group({
      cpf: ['', Validators.required],
      tituloEleitor: ['', Validators.required],
      identidade: ['', Validators.required],
      dataExpedicaoIdentidade: ['', Validators.required],
      orgaoExpedicaoIdentidade: ['', Validators.required],
      ufIdentidade: ['', Validators.required],
      estadoCivil: ['', Validators.required],
      carteiraTrabalho: ['', Validators.required],
      dataExpedicaoCarteiraTrabalho: ['', Validators.required],
      id: ['']
    });
  }

  public carregarDadosPessoais(): void {

    this.funcionarioId  = +this.routerActivated.snapshot.paramMap.get('id');

    if (this.funcionarioId !== null && this.funcionarioId !== 0) {
      this.spinner.show();

      this.modoEditar = true;

      this.funcionarioService
        .recuperarFuncionarioPorId(this.funcionarioId)
        .subscribe(
          (funcionario: Funcionario) => {

            this.dadosPessoais = funcionario.dadosPessoais;
            this.form.patchValue(this.dadosPessoais);},

          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
            console.error(error);})

        .add(() => this.spinner.hide())
    };
  }

  public salvarDadosPessoais(): void {

    this.spinner.show();

    if (this.form.valid) {

      this.dadosPessoais = this.form.value;

      this.dadosPessoaisService
        .salvarDadoPessoal(this.dadosPessoais)
        .subscribe(
          (dadosPessoais: DadoPessoal) => {
            this.dadosPessoais = dadosPessoais
            this.toastr.success("Dados Pessoais atualizados!", "Sucesso!")
          },

          (error: any) => {
            console.error(error);
            this.toastr.error("Falha ao atualizar Dados Pessoais.", "Erro!");})

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
}
