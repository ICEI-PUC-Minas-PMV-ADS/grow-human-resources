import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Funcionario } from 'src/app/models/Funcionario';
import { FuncionarioService } from 'src/app/services/funcionario.service';

@Component({
  selector: 'app-funcionario-lista',
  templateUrl: './funcionario-lista.component.html',
  styleUrls: ['./funcionario-lista.component.scss']
})
export class FuncionarioListaComponent implements OnInit {
  modalRef?: BsModalRef;

  public funcionarios: Funcionario[] = [];
  public funcionariosFiltrados: Funcionario[] = [];

  public larguraImg = 100;
  public margemImg = 2;
  public exibirImg = true;

  private filtroListado: string = '';

  public get filtroLista(): string {
    return this.filtroListado;
  }

  public set filtroLista(filtro: string) {
    this.filtroListado = filtro;
    this.funcionariosFiltrados = this.filtroLista
      ? this.filtrarFuncionarios(this.filtroLista)
      : this.funcionarios;
  }

  public filtrarFuncionarios(filtrarPor: string): Funcionario[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.funcionarios.filter(
      (funcionario: Funcionario) => funcionario.nomeCompleto.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        funcionario.email.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        funcionario.telefone.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(
    private funcionarioService: FuncionarioService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.getFuncionarios();
    this.spinner.show();
  }

  public alternarImagem(): void {
    this.exibirImg = !this.exibirImg;
  }
  public getFuncionarios(): void {
    this.funcionarioService.getFuncionarios().subscribe({
      next: (funcionariosRetorno: Funcionario[]) => {
        this.funcionarios = funcionariosRetorno;
        this.funcionariosFiltrados = this.funcionarios;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Falha ao carregar os funcionários', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }
  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.toastr.success('Exclusão de funcionario realizada', 'Sucesso!');
  }

  recusar(): void {
    this.modalRef?.hide();
  }
  detalheFuncionario(id: number): void {
    this.router.navigate([`funcionarios/detalhe/${id}`]);
  }
}
