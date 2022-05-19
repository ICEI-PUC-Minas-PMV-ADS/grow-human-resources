import {
  Component,
  OnInit,
  TemplateRef
} from '@angular/core';
import { Router } from '@angular/router';

import {
  BsModalRef,
  BsModalService
} from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Cargo } from 'src/app/models/cargos/Cargo';

import { CargoService } from 'src/app/services/cargos/Cargo.service';


@Component({
  selector: 'app-cargo-lista',
  templateUrl: './cargo-lista.component.html',
  styleUrls: ['./cargo-lista.component.scss']
})

export class CargoListaComponent implements OnInit {

  public modalRef?: BsModalRef;

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
      (cargo: Cargo) =>
        cargo.nomeCargo.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        cargo.funcao.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        cargo.departamentos.nomeDepartamento.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(
    private cargoService: CargoService,
    private modalService: BsModalService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
  ) { }

  public ngOnInit(): void {

    this.spinner.show();
    this.consultarCargos();
  }

  public consultarCargos(): void {

    this.spinner.show();

    this.cargoService.recuperarCargos().subscribe(
      (cargosRetorno: Cargo[]) => {
        this.cargos = cargosRetorno;
        this.cargosFiltrados = this.cargos;},
      (error: any) => {
        this.toastr.error('Falha ao carregar os cargos', 'Erro!');
        console.log(error);
      }).add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, cargoId: number): void {

    event.stopPropagation();

    this.cargoId = cargoId

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmar(): void {

    this.modalRef?.hide();

    this.spinner.show();

    this.cargoService.excluirCargo(this.cargoId).subscribe(
      (retornoDelete: any) => {
        if (retornoDelete.message === "Excluído") {
          this.toastr.success("Cargo excluído da base!", "Excluído!")
          this.spinner.hide();
          this.consultarCargos();
        }},
      (error: any) => {
        this.toastr.error(`Falha ao excluir departamento ${this.cargoId}`, 'Erro!');
        console.error(error);
      }).add(() => this.spinner.hide());
  }

  recusar(): void {

    this.modalRef?.hide();
  }

  detalheCargo(id: number): void {

    this.router.navigate([`cargos/detalhe/${id}`]);
  }
}
