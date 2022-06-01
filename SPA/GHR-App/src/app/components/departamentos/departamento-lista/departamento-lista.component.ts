import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { Departamento } from 'src/app/models/departamentos/Departamento';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';

import { DepartamentoService } from 'src/app/services/departamentos/departamento.service';
@Component({
  selector: 'app-departamento-lista',
  templateUrl: './departamento-lista.component.html',
  styleUrls: ['./departamento-lista.component.scss']
})
export class DepartamentoListaComponent implements OnInit {

  public modalRef?: BsModalRef;
  public departamentos: Departamento[] = [];
  public departamentosFiltrados: Departamento[] = [];
  public paginacao = {} as Paginacao;
  public departamentoId = 0;

  alteracaoTermoBusca: Subject<string> = new Subject<string>();

  public filtrarDepartamentos(event: any): void {
    if (this.alteracaoTermoBusca.observers.length === 0) {
      this.spinner.show();

      this.alteracaoTermoBusca
        .pipe(debounceTime(1500))
        .subscribe(
          filtrarPor => {
          this.departamentoService
            .recuperarDepartamentos(this.paginacao.paginaAtual, this.paginacao.itensPorPagina, filtrarPor)
            .subscribe(
              (departamentosRetorno: ResultadoPaginacao<Departamento[]>) => {
                this.departamentos = departamentosRetorno.resultado;
                this.paginacao = departamentosRetorno.paginacao;},
              (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))
            .add(() => this.spinner.hide());
          }
        )
    }
    this.alteracaoTermoBusca.next(event.value);
  }

  constructor(
    private departamentoService: DepartamentoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.carregarDepartamentos();
    this.spinner.show();
  }

  public carregarDepartamentos(): void {
    this.spinner.show();

    this.departamentoService
      .recuperarDepartamentos(this.paginacao.paginaAtual,
        this.paginacao.itensPorPagina)
      .subscribe(
        (departamentosRetorno: ResultadoPaginacao<Departamento[]>) => {
          this.departamentos = departamentosRetorno.resultado;
          this.paginacao = departamentosRetorno.paginacao;},
      (error: any) => this.toastr.error('Falha ao carregar os departamentos', 'Erro!'),)
    .add(() => this.spinner.hide());

  }

  public openModal(event: any, template: TemplateRef<any>, departamentoId: number): void {
    event.stopPropagation();
    this.departamentoId= departamentoId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public paginaAlterada(event): void {
    this.paginacao.paginaAtual = event.page;
    this.carregarDepartamentos();
  }


  public confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.departamentoService.excluirDepartamento(this.departamentoId).subscribe(
      (retornoDelete: any) => {
        if (retornoDelete.message === "Excluído") {
          this.toastr.success("Departamento excluído da base!", "Excluído!")
          this.spinner.hide();
          this.carregarDepartamentos();
        }
      },
      (error: any) => {
        this.toastr.error(`Falha ao excluir departamento ${this.departamentoId}`, 'Erro!');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  public recusar(): void {
    this.modalRef?.hide();
  }

  public detalheDepartamento(id: number): void {
    this.router.navigate([`departamentos/detalhe/${id}`]);
  }
}
