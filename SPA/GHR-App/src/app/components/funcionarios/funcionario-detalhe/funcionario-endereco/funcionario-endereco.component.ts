import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { Endereco } from 'src/app/models/funcionarios/Endereco';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { NavegacaoEntreForms } from 'src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms';

import { EnderecoService } from 'src/app/services/funcionarios/endereco.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

@Component({
  selector: 'app-funcionario-endereco',
  templateUrl: './funcionario-endereco.component.html',
  styleUrls: ['./funcionario-endereco.component.scss']
})
export class FuncionarioEnderecoComponent implements OnInit {

  @Input() formsParametros: NavegacaoEntreForms ;

  public form: FormGroup;

  public visaoRH = false;

  public funcionario = {} as Funcionario;
  public endereco = {} as Endereco;

  public locale = 'pt-br';

  public protegerCampoEndereco = false;

  public funcionarioId: number;


  get ctrEmp(): any {
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
    private enderecoService: EnderecoService,
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService,
    private localeService: BsLocaleService,
    private router: Router,
    private routerActivated: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
    ) {    }

  ngOnInit(): void {

    this.visaoRH = (this.formsParametros.visaoRhLogado) ? true : false
    this.protegerCampoEndereco = (this.visaoRH) ? false : true;

    this.validarFormularios();
    this.carregarEndereco();
  }

  public validarFormularios(): void {

    this.form = this.fb.group({
      cep: ['', Validators.required],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: ['', Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      uf: ['', Validators.required],
      pais: ['Brasil', Validators.required],
      caixaPostal: ['', Validators.required],
      complementoEndereco: ['', Validators.required],
      id: ['']});
  }

  public carregarEndereco(): void {

    this.funcionarioId  = +this.routerActivated.snapshot.paramMap.get('id');

    if (this.funcionarioId !== null && this.funcionarioId !== 0) {
      this.spinner.show();

      this.funcionarioService
      .recuperarFuncionarioPorId(this.funcionarioId)
      .subscribe(
        (funcionario: Funcionario) => {
          this.form.patchValue(funcionario.enderecos)},

        (error: any) => {
            this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
            console.error(error);})

        .add(() => this.spinner.hide())
      };
    }

  public salvarEndereco(): void {

    this.spinner.show();

    if (this.form.valid) {

      this.endereco = { ...this.form.value };

      this.enderecoService
        .salvarEndereco(this.endereco)
        .subscribe(
          (enderecoRetrono: Endereco) => {
            this.endereco = enderecoRetrono;
            this.toastr.success("Endereço atualizado.", "Sucesos!");},

          (error: any) => {
            console.error(error);
            this.toastr.error("Falha ao atualizar Endereço.", "Erro!");})

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
