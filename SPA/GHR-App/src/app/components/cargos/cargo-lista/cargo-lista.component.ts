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
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { Cargo } from 'src/app/models/cargos/Cargo';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';

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
  public paginacao = {} as Paginacao;
  public cargoId = 0;

  public alteracaoTermoBusca: Subject<string> = new Subject<string>();

  public filtrarCargos(event: any): void {

    if (this.alteracaoTermoBusca.observers.length === 0) {

      this.spinner.show();

      this.alteracaoTermoBusca
        .pipe(debounceTime(1500))
        .subscribe(
          filtrarPor => {
            this.cargoService
              .recuperarCargos(this.paginacao.paginaAtual, this.paginacao.itensPorPagina, filtrarPor)
              .subscribe(
                (cargosRetorno: ResultadoPaginacao<Cargo[]>) => {

                  this.cargos = cargosRetorno.resultado;
                  this.paginacao = cargosRetorno.paginacao;},

                (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'))

              .add(() => this.spinner.hide()); })
    }

    this.alteracaoTermoBusca.next(event.value);
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
    this.carregarCargos();

  }

  public carregarCargos(): void {

    this.spinner.show();

    this.cargoService
      .recuperarCargos(
        this.paginacao.paginaAtual,
        this.paginacao.itensPorPagina)
      .subscribe(
        (cargosRetorno: ResultadoPaginacao<Cargo[]>) => {

          this.cargos = cargosRetorno.resultado;
          this.paginacao = cargosRetorno.paginacao;},
        (error: any) => {

          this.toastr.error('Falha ao carregar os cargos', 'Erro!');
          console.log(error);})

      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, cargoId: number): void {

    event.stopPropagation();

    this.cargoId = cargoId

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public paginaAlterada(event): void {

    this.paginacao.paginaAtual = event.page;

    this.carregarCargos();
  }

  confirmar(): void {

    this.modalRef?.hide();

    this.spinner.show();

    this.cargoService
      .excluirCargo(this.cargoId)
      .subscribe(
        (retornoDelete: any) => {
          if (retornoDelete.message === "Excluído") {

            this.toastr.success("Cargo excluído da base!", "Excluído!")
            this.spinner.hide();
            this.carregarCargos();
          }},
        (error: any) => {

          this.toastr.error(`Falha ao excluir departamento ${this.cargoId}`, 'Erro!');
          console.error(error); })
      .add(() => this.spinner.hide());

  }

  recusar(): void {

    this.modalRef?.hide();
  }

  detalheCargo(id: number): void {

    this.router.navigate([`cargos/detalhe/${id}`]);
  }
}
