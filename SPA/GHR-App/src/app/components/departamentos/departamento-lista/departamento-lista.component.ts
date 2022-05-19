import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Departamento } from 'src/app/models/departamentos/Departamento';

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
  public departamentoId = 0;

  private filtroListado: string = '';

  public get filtroLista(): string {
    return this.filtroListado;
  }

  public set filtroLista(filtro: string) {
    this.filtroListado = filtro;
    this.departamentosFiltrados = this.filtroLista
      ? this.filtrarDepartamentos(this.filtroLista)
      : this.departamentos;
  }

  public filtrarDepartamentos(filtrarPor: string): Departamento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.departamentos.filter(
      (departamento: Departamento) =>
        departamento.nomeDepartamento
          .toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        departamento.siglaDepartamento
          .toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
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
    this.departamentoService.recuperarDepartamentos().subscribe({
      next: (departamentosRetorno: Departamento[]) => {
        this.departamentos = departamentosRetorno;
        this.departamentosFiltrados = this.departamentos;
      },
      error: (error: any) => this.toastr.error('Falha ao carregar os departamentos', 'Erro!'),
    }).add(() => this.spinner.hide());
  }
  openModal(event: any, template: TemplateRef<any>, departamentoId: number): void {
    event.stopPropagation();
    this.departamentoId= departamentoId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.departamentoService.excluirFuncionario(this.departamentoId).subscribe(
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

  recusar(): void {
    this.modalRef?.hide();
  }

  detalheDepartamento(id: number): void {
    this.router.navigate([`departamentos/detalhe/${id}`]);
  }
}
