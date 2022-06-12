import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { Conta } from 'src/app/models/contas/Conta';
import { ContaVisao } from 'src/app/models/contas/ContaVisao';
import { DadoPessoal } from 'src/app/models/funcionarios/DadoPessoal';
import { Endereco } from 'src/app/models/funcionarios/Endereco';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';

import { ContaService } from 'src/app/services/contas/Conta.service';
import { DadosPessoaisService } from 'src/app/services/funcionarios/dadosPessoais.service';
import { EnderecoService } from 'src/app/services/funcionarios/endereco.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

@Component({
  selector: 'app-funcionario-conta',
  templateUrl: './funcionario-conta.component.html',
  styleUrls: ['./funcionario-conta.component.scss']
})
export class FuncionarioContaComponent implements OnInit {

  @Output() changeFormValue = new EventEmitter();

  public form: FormGroup;

  public contaVisao = {} as ContaVisao;
  public visaoRH = false;

  public funcionario = {} as Funcionario;
  public contaPesquisa = {} as Conta;
  public dadosPessoais = {} as DadoPessoal;
  public enderecos = {} as Endereco;

  public protegerCampoConta = false;
  public modoEditar = false;
  public contaOk = false;

  public funcionarioId: number;


  get ctrConta(): any {
    return this.form.controls;
  }

  constructor(
    private contaService: ContaService,
    private dadosPessoaisService: DadosPessoaisService,
    private enderecoService: EnderecoService,
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService,
    private router: Router,
    private routerActivated: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {

    this.validarFormularios();
    this.carregarContaAtiva();

    this.funcionarioId = +this.routerActivated.snapshot.paramMap.get('id');

    if (this.funcionarioId !== null && this.funcionarioId !== 0) {

      this.modoEditar = true;
      this.carregarFuncionario();

    }
  }

  public validarFormularios(): void {

    this.form = this.fb.group({
      contaPesquisa: [''],
      userName: [''],
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50),]],
      email: ['', [
        Validators.required,
        Validators.email]],
      phoneNumber: ['', Validators.required],
      visao: ['Funcionário', Validators.required],
      contaOk: [false],
      visaoRhLogado: [],
      id: [],
    });

    this.verificarFormulario();
  }

  public carregarContaAtiva(): void {
    this.spinner.show();

    this.contaService
      .recuperarContaAtiva()
      .subscribe(
        (conta: Conta) => {
          this.visaoRH = conta.visao.includes('RH');
          this.protegerCampoConta = (this.visaoRH) ? false : true;
          this.form.get("visaoRhLogado").setValue(this.visaoRH);
          this.form.get("contaOk").setValue(false)
        },
        (error: any) => {
          this.toastr.error("Falha ao subir a cotna ativa.", "Erro!");
          console.error(error);
        })
      .add(() => this.spinner.hide())
  }

  public carregarFuncionario(): void {
    this.spinner.show();

    this.funcionarioService
      .recuperarFuncionarioPorId(this.funcionarioId)
      .subscribe(
        (funcionario: Funcionario) => {
          this.funcionario = funcionario;
          this.contaPesquisa = funcionario.contas;
          this.form.patchValue(this.contaPesquisa);
        },
        (error: any) => {
          this.toastr.error("Erro ao consultar funcionário.", "Errro!");
          console.error(error);    })

      .add(() => this.spinner.hide());
  }

  public verificarFormulario(): void {
    this.form.valueChanges.subscribe(
      () => this.changeFormValue.emit({ ... this.form.value })
    )
  }

  public consultarConta(): void {

    this.spinner.show();

    this.form.get("contaOk").setValue(false);

    if (!this.form.get('contaPesquisa').value) {
      this.toastr.info("Conta para pesquisa não iformada.", "Informação!");
      this.spinner.hide();

    } else {
      this.contaService
        .recuperarContaPorUserName(this.form.get('contaPesquisa').value)
        .subscribe(
          (conta: Conta) => {

            if (conta == null) {
              this.toastr.info("Conta não encontrada.", "Informação!");

            } else {
              this.contaPesquisa = conta ;
              this.form.patchValue(this.contaPesquisa);
              this.consultarFuncionarioDaConta();

            };},

          (error) => {
            console.error(error);
            this.toastr.error("Falha na carga de usuário", "Erro!");})

        .add(() => this.spinner.hide());
    }
  }

  public consultarFuncionarioDaConta(): void {

    this.spinner.show();

    this.funcionarioService
      .recuperarFuncionarioPorContaId(this.contaPesquisa.id)
      .subscribe(
        (funcionario: Funcionario) => {

          if (funcionario == null) {
            this.salvarFuncionario()

          } else {
            this.modoEditar = true
            this.funcionario = funcionario
            this.toastr.success("Conta validada, dados do funcionario liberados!", "Sucesso!");
            this.router.navigate([`funcionarios/detalhe/${this.funcionario.id}`]);
            this.form.get("contaOk").setValue(true);
          }},

        (error: any) => {
          this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
          console.error(error);})

      .add(() => this.spinner.hide());
  }

  public salvarConta(): void {

    this.spinner.show();

    if (this.form.valid) {

      this.contaVisao = { id: this.contaPesquisa.id, ...this.form.value };
      console.log(this.contaVisao)
      this.contaService
        .atualizarConta(this.contaVisao)
        .subscribe(
          (contaVisao: ContaVisao) => {

            this.contaVisao = contaVisao;
            this.protegerCampoConta = true;
            this.form.get("contaOk").setValue(true);
            this.router.navigate([`funcionarios/detalhe/${this.funcionario.id}`]);
            this.toastr.success("Dados da conta atualizado!", "Sucesso!")
          },

          (error: any) => {
            this.toastr.error("Erro ao salvar alterações.", "Erro!");
            console.error(error);})

        .add(() => this.spinner.hide());
    };
  }

  public salvarFuncionario(): void {

    if (!this.modoEditar) {

      this.criarDadosPessoais();
    }
  }

  public criarDadosPessoais(): void {

    this.spinner.show();

    this.dadosPessoaisService
      .criarDadoPessoal(this.dadosPessoais)
      .subscribe(
        (dadosPessoais: DadoPessoal) => {
          this.dadosPessoais = dadosPessoais;
          this.criarEndereco();},

        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar Dados Pessoais.", "Erro!");})

      .add(() => this.spinner.hide())
  }

  public criarEndereco(): void {

    this.spinner.show();

    this.enderecoService
      .criarEndereco(this.enderecos)
      .subscribe(
        (endereco: Endereco) => {
          this.enderecos = endereco
          this.criarFuncionario();},

        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar Endereço.", "Erro!");})
      .add(() => this.spinner.hide())
  }

  public criarFuncionario(): void {

    this.funcionario = {
      dadosPessoaisId: this.dadosPessoais.id,
      enderecoId: this.enderecos.id,
      ...this.form.value};

    this.funcionario.contaId = this.contaPesquisa.id;
    this.funcionario.cargoId = 1;
    this.funcionario.departamentoId = 1;

    this.funcionarioService
      .criarFuncionario(this.funcionario)
      .subscribe(
        (funcionario: Funcionario) => {

          this.form.get("contaOk").setValue(true);
          this.funcionario = funcionario;
          this.router.navigate([`funcionarios/detalhe/${this.funcionario.id}`]);
          this.toastr.success("Dados da Conta atualizados", "Sucesso!")},

        (error: any) => {

          console.error(error);
          this.toastr.error("Falha ao atualizar Conta.", "Erro!");})

      .add(() => this.spinner.hide());
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
