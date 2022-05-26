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
import { Paginacao, ResultadoPaginacao } from './../../../models/paginacao/paginacao';

import { ContaService } from 'src/app/services/contas/Conta.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

import { environment } from './../../../../environments/environment';



@Component({
  selector: 'app-funcionario-lista',
  templateUrl: './funcionario-lista.component.html',
  styleUrls: ['./funcionario-lista.component.scss']
})
export class FuncionarioListaComponent implements OnInit {
  public modalRef?: BsModalRef;

  public contaLogada = false;
  public contaAtiva = {} as ContaAtiva;
  public visaoRH = false;

  public funcionarios: Funcionario[] = [];
  public paginacao = {} as Paginacao;

  public funcionarioId = 0;
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
    public contaService: ContaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {
    router.events.subscribe(
      (val) => {
        if (val instanceof NavigationEnd) {
          this.contaService.contaAtual$.subscribe(
            (value) => {
              this.contaLogada = value !== null;
              this.contaAtiva = { ...value } ;
              this.visaoRH = this.contaAtiva.visao?.includes('RH');
              console.log(this.contaLogada, this.contaAtiva, this.contaAtiva.visao);
              console.log('Menu', this.contaAtiva.visao, this.visaoRH);
            }
          )
        }
      }
    )
  }

  public ngOnInit(): void {
    this.paginacao = { paginaAtual: 1, itensPorPagina: 3, totalItens: 4 } as Paginacao;
    this.carregarFuncionarios();
  }

  public alternarImagem(): void {
    this.exibirImg = !this.exibirImg;
  }

  public exibirImagem(imagemURL: string): string {
    return (imagemURL !== 'semfoto.jpg')
      ? `${environment.apiURL}resources/images/${imagemURL}`
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
      .recuperarFuncionarios(this.paginacao.paginaAtual,
        this.paginacao.itensPorPagina)
      .subscribe(
        (funcionariosRetorno: ResultadoPaginacao<Funcionario[]>) => {
          this.funcionarios = funcionariosRetorno.resultado;
          console.log("func", this.funcionarios)
          this.paginacao = funcionariosRetorno.paginacao;},
        (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))
      .add(() => this.spinner.hide());
  }

  public recuperarFuncionarioLogado() {

    this.spinner.show();

    this.contaService
      .recuperarContaPorUserName(this.contaAtiva.userName)
      .subscribe(
        (contaPesquisada: ContaVisao) => {
          console.log(contaPesquisada)
          this.contaId = contaPesquisada.id;
          console.log(this.contaId);
          this.recuperarFuncionarioPorContaId();},
        (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))
      .add(() => this.spinner.hide());
    console.log("contaId", this.contaId)
  }

  public recuperarFuncionarioPorContaId() {
    this.paginacao.totalItens = 1;
    this.funcionarioService
      .recuperarFuncionarioPorContaId(this.contaId)
      .subscribe(
        (funcionarioRetorno: Funcionario) => {
          this.funcionarios[0] = funcionarioRetorno;
          console.log("func1", funcionarioRetorno, this.visaoRH, this.contaId);},
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
    console.log("page", event.page);
    this.carregarFuncionarios();
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.funcionarioService.excluirFuncionario(this.funcionarioId).subscribe(
      (retornoDelete: any) => {
        if (retornoDelete.message === "Excluído") {
          this.toastr.success("Funcionário excluído da base!", "Excluído!")
          this.spinner.hide();
          this.carregarFuncionarios();
        }
      },
      (error: any) => {
        this.toastr.error(`Falha ao excluir funcionário ${this.funcionarioId}`, 'Erro!');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
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
