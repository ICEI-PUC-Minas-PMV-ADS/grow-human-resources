import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cargo } from 'src/app/models/Cargo';
import { Departamento } from 'src/app/models/Departamento';
import { Funcionario } from 'src/app/models/Funcionario';
import { CargoService } from 'src/app/services/Cargo.service';
import { DepartamentoService } from 'src/app/services/departamento.service';

@Component({
  selector: 'app-cargo-lista',
  templateUrl: './cargo-lista.component.html',
  styleUrls: ['./cargo-lista.component.scss']
})
export class CargoListaComponent implements OnInit {
  modalRef?: BsModalRef;


  public cargos: Cargo[] = [];
  public cargosFiltrados: Cargo[] = [];
  public cargoId = 0;

  private filtroListado: string = '';

  public get filtroLista(): string {
    return this.filtroListado;
  }

  public set filtroLista(filtro: string) {
    this.filtroListado = filtro;
    this.cargosFiltrados = this.filtroLista
      ? this.filtrarCargos(this.filtroLista)
      : this.cargos;
  }

  public filtrarCargos(filtrarPor: string): Cargo[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.cargos.filter(
      (cargo: Cargo) => cargo.nomeCargo.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(
    private cargoService: CargoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.carregarCargos();
    this.spinner.show();
  }

  public carregarCargos(): void {
    this.spinner.show();
    this.cargoService.getCargos().subscribe({
      next: (cargosRetorno: Cargo[]) => {
        this.cargos = cargosRetorno;
        this.cargosFiltrados = this.cargos;
      },
      error: (error: any) => this.toastr.error('Falha ao carregar os cargos', 'Erro!'),
    }).add(() => this.spinner.hide());
  }
  openModal(event: any, template: TemplateRef<any>, cargoId: number): void {
    event.stopPropagation();
    this.cargoId = cargoId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.cargoService.deleteCargo(this.cargoId).subscribe(
      (retornoDelete: any) => {
        if (retornoDelete.message === "Excluído") {
          this.toastr.success("Cargo excluído da base!", "Excluído!")
          this.spinner.hide();
          this.carregarCargos();
        }
      },
      (error: any) => {
        this.toastr.error(`Falha ao excluir departamento ${this.cargoId}`, 'Erro!');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  recusar(): void {
    this.modalRef?.hide();
  }

  detalheCargo(id: number): void {
    this.router.navigate([`cargos/detalhe/${id}`]);
  }
}
