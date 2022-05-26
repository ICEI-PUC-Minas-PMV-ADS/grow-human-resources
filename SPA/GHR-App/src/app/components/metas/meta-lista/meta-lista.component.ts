import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

import { Meta } from 'src/app/models/metas/Meta';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/paginacao/paginacao';

import { MetaService } from 'src/app/services/metas/Meta.service';

@Component({
  selector: 'app-meta-lista',
  templateUrl: './meta-lista.component.html',
  styleUrls: ['./meta-lista.component.scss']
})
export class MetaListaComponent implements OnInit {
  modalRef?: BsModalRef;


  public metas: Meta[] = [];
  public paginacao = {} as Paginacao;
  public metaId = 0;

  alteracaoTermoBusca: Subject<string> = new Subject<string>();

  public filtrarMetas(event: any): void {
    if (this.alteracaoTermoBusca.observers.length === 0) {
      this.spinner.show();

      this.alteracaoTermoBusca
        .pipe(debounceTime(1500))
        .subscribe(
          filtrarPor => {
          this.metaService
            .recuperarMetas(this.paginacao.paginaAtual, this.paginacao.itensPorPagina, filtrarPor)
            .subscribe(
              (MetasRetorno: ResultadoPaginacao<Meta[]>) => {
                this.metas = MetasRetorno.resultado;
                this.paginacao = MetasRetorno.paginacao;},
              (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))
            .add(() => this.spinner.hide());
          }
        )
    }
    this.alteracaoTermoBusca.next(event.value);
  }

  constructor(
    private metaService: MetaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.paginacao = { paginaAtual: 1, itensPorPagina: 3, totalItens: 4 } as Paginacao;
    this.carregarMetas();
    this.spinner.show();
  }

  public carregarMetas(): void {
    this.spinner.show();
    this.metaService
      .recuperarMetas(this.paginacao.paginaAtual,
        this.paginacao.itensPorPagina)
      .subscribe(
        (metasRetorno: ResultadoPaginacao<Meta[]>) => {
          this.metas = metasRetorno.resultado;
          console.log("func", this.metas)
          this.paginacao = metasRetorno.paginacao;},
        (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))
      .add(() => this.spinner.hide());
  }
  openModal(event: any, template: TemplateRef<any>, metaId: number): void {
    event.stopPropagation();
    this.metaId= metaId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public paginaAlterada(event): void {
    this.paginacao.paginaAtual = event.page;
    console.log("page", event.page);
    this.carregarMetas();
    }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.metaService.excluirMeta(this.metaId).subscribe(
      (retornoDelete: any) => {
        if (retornoDelete.message === "Excluído") {
          this.toastr.success("Meta excluída da base!", "Excluído!")
          this.spinner.hide();
          this.carregarMetas();
        }
      },
      (error: any) => {
        this.toastr.error(`Falha ao excluir metao ${this.metaId}`, 'Erro!');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  recusar(): void {
    this.modalRef?.hide();
  }

  detalheMeta(id: number): void {
    this.router.navigate([`metas/detalhe/${id}`]);
  }
}
