import { Component, OnInit, TemplateRef } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { ContaVisao } from 'src/app/models/contas/ContaVisao';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { Paginacao, ResultadoPaginacao } from '../../../models/suporte/paginacao/paginacao';

import { ContaService } from 'src/app/services/contas/Conta.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

import { environment } from './../../../../environments/environment';
import { DadosPessoaisService } from 'src/app/services/funcionarios/dadosPessoais.service';
import { EnderecoService } from 'src/app/services/funcionarios/endereco.service';
import { Endereco } from 'src/app/models/funcionarios/Endereco';
import { DadoPessoal } from 'src/app/models/funcionarios/DadoPessoal';



@Component({
  selector: 'app-funcionario-lista',
  templateUrl: './funcionario-lista.component.html',
  styleUrls: ['./funcionario-lista.component.scss']
})
export class FuncionarioListaComponent implements OnInit {
  public modalRef?: BsModalRef;

  public contaAtiva = {} as ContaAtiva;
  public visaoRH = false;

  public funcionarios: Funcionario[] = [];
  public paginacao = {} as Paginacao;

  public funcionarioId = 0;
  public enderecoId = 0;
  public dadosPessoaisId = 0;
  public contaId = 0;

  public larguraImg = 100;
  public margemImg = 2;
  public exibirImg = true;

  alteracaoTermoBusca: Subject<string> = new Subject<string>();

  public filtrarFuncionarios(event: any): void {
    if (this.alteracaoTermoBusca.observers.length === 0) {
      this.spinner.show();

      this.alteracaoTermoBusca
        .pipe(debounceTime(1500))
        .subscribe(
          filtrarPor => {
          this.funcionarioService
            .recuperarFuncionarios(this.paginacao.paginaAtual, this.paginacao.itensPorPagina, filtrarPor)
            .subscribe(
              (funcionariosRetorno: ResultadoPaginacao<Funcionario[]>) => {
                this.funcionarios = funcionariosRetorno.resultado;
                this.paginacao = funcionariosRetorno.paginacao;},
              (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))
            .add(() => this.spinner.hide());
          }
        )
    }
    this.alteracaoTermoBusca.next(event.value);
  }

  constructor(
    private funcionarioService: FuncionarioService,
    private enderecoService: EnderecoService,
    private dadoPessoalService: DadosPessoaisService,
    public contaService: ContaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {
    router.events.subscribe(
      (verificaContaAtiva) => {
        if (verificaContaAtiva instanceof NavigationEnd) {
          this.contaService.contaAtual$.subscribe(
            (value) => {
              this.contaAtiva = { ...value } ;
              this.visaoRH = this.contaAtiva.visao?.includes('RH');})
        }})
  }

  public ngOnInit(): void {

    this.carregarFuncionarios();

  }

  public alternarImagem(): void {

    this.exibirImg = !this.exibirImg;

  }

  public exibirImagem(imagemURL: string): string {
    return (imagemURL !== '' && imagemURL !== null)
      ? `${environment.apiURL}recursos/fotos/${imagemURL}`
      : 'assets/img/semImagem.jfif';
  }

  public carregarFuncionarios(): void {

    if (this.visaoRH) {

      this.recuperarListaFuncionarios();

    } else {

      this.recuperarFuncionarioLogado();
    }
  }

  public recuperarListaFuncionarios(): void {

    this.spinner.show();

    this.funcionarioService
      .recuperarFuncionarios(this.paginacao.paginaAtual
        , this.paginacao.itensPorPagina)
      .subscribe(
        (funcionarios: ResultadoPaginacao<Funcionario[]>) => {

          this.funcionarios = funcionarios.resultado;
          this.paginacao = funcionarios.paginacao;},

        (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))

      .add(() => this.spinner.hide());
  }

  public recuperarFuncionarioLogado() {

    this.spinner.show();

    this.contaService
      .recuperarContaPorUserName(this.contaAtiva.userName)
      .subscribe(
        (contaPesquisada: ContaVisao) => {

          this.contaId = contaPesquisada.id;
          this.recuperarFuncionarioPorContaId();},

        (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))

      .add(() => this.spinner.hide());
  }

  public recuperarFuncionarioPorContaId() {

    this.paginacao.totalItens = 1;

    this.funcionarioService
      .recuperarFuncionarioPorContaId(this.contaId)
      .subscribe(
        (funcionarioRetorno: Funcionario) => {

          if (funcionarioRetorno !== null) {

            this.funcionarios[0] = funcionarioRetorno;
          }},

        (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))

      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, funcionarioId: number): void {

    event.stopPropagation();

    this.funcionarioId = funcionarioId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public paginaAlterada(event): void {

    this.paginacao.paginaAtual = event.page;

    this.carregarFuncionarios();
  }

  public confirmar(): void {

    this.modalRef?.hide();
    this.spinner.show();

    this.funcionarioService
      .recuperarFuncionarioPorId(this.funcionarioId)
      .subscribe(
        (funcionario: Funcionario) => {

          this.enderecoId = funcionario.enderecoId;
          this.dadosPessoaisId = funcionario.dadosPessoaisId;

          this.funcionarioService
            .excluirFuncionario(this.funcionarioId)
            .subscribe(
              (retornoDelete: any) => {

                if (retornoDelete.message === "Excluído") {
                  this.toastr.success("Funcionário excluído da base!", "Excluído!")
                  this.spinner.hide();
                  this.excluirEndereco(this.enderecoId);
                  this.excluirDadosPessoais(this.dadosPessoaisId);
                  this.carregarFuncionarios();
                }},

              (error: any) => {

                this.toastr.error(`Falha ao excluir funcionário ${this.funcionarioId}`, 'Erro!');
                console.error(error);})

            .add(() => this.spinner.hide());},

        (error: any) => {

          this.toastr.error(`Falha ao recuperar funcionário ${this.funcionarioId}`, 'Erro!');
          console.error(error);})

      .add(() => this.spinner.hide());
  }

  public excluirEndereco(id: number): void {
    this.spinner.show();

    this.enderecoService
      .excluirEndereco(id)
      .subscribe(
        (endereco: Endereco) => {

          this.toastr.success("Endereco excluído da base!", "Excluído!");},

        (error: any) => {

          this.toastr.error(`Falha ao excluir Endereco ${this.enderecoId}`, 'Erro!');
          console.error(error);})

      .add(() => this.spinner.hide());
  }

  public excluirDadosPessoais(id: number): void {

    this.spinner.show();

    this.dadoPessoalService
      .excluirDadoPessoal(id)
      .subscribe(

        (dadoPessoal: DadoPessoal) => {

          this.toastr.success("Dados pessoais excluído da base!", "Excluído!");},

        (error: any) => {

          this.toastr.error(`Falha ao excluir Dados Pessoais ${this.enderecoId}`, 'Erro!');
          console.error(error);})

      .add(() => this.spinner.hide());
  }

  recusar(): void {
    this.modalRef?.hide();
  }

  detalheFuncionario(id: number): void {
    this.router.navigate([`funcionarios/detalhe/${id}`]);
  }

  funcionarioMeta(id: number): void {
    this.router.navigate([`funcionarios/meta/${+id}`]);
  }

}
