import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Meta } from 'src/app/models/Meta';
import { MetaService } from 'src/app/services/Meta.service';

@Component({
  selector: 'app-meta-lista',
  templateUrl: './meta-lista.component.html',
  styleUrls: ['./meta-lista.component.scss']
})
export class MetaListaComponent implements OnInit {
  modalRef?: BsModalRef;


  public metas: Meta[] = [];
  public metasFiltrados: Meta[] = [];
  public metaId = 0;

  private filtroListado: string = '';

  public get filtroLista(): string {
    return this.filtroListado;
  }

  public set filtroLista(filtro: string) {
    this.filtroListado = filtro;
    this.metasFiltrados = this.filtroLista
      ? this.filtrarMetas(this.filtroLista)
      : this.metas;
  }

  public filtrarMetas(filtrarPor: string): Meta[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.metas.filter(
      (meta: Meta) => meta.nomeMeta.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        meta.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(
    private metaService: MetaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.carregarMetas();
    this.spinner.show();
  }

  public carregarMetas(): void {
    this.spinner.show();
    this.metaService.getMetas().subscribe({
      next: (metasRetorno: Meta[]) => {
        this.metas = metasRetorno;
        this.metasFiltrados = this.metas;
      },
      error: (error: any) => this.toastr.error('Falha ao carregar as metas', 'Erro!'),
    }).add(() => this.spinner.hide());
  }
  openModal(event: any, template: TemplateRef<any>, metaId: number): void {
    event.stopPropagation();
    this.metaId= metaId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.metaService.deleteMeta(this.metaId).subscribe(
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
