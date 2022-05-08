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
  public funcionarioId = 0;

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
    this.carregarFuncionarios();
    this.spinner.show();
  }

  public alternarImagem(): void {
    this.exibirImg = !this.exibirImg;
  }
  public carregarFuncionarios(): void {
    this.spinner.show();
    this.funcionarioService.getFuncionarios().subscribe({
      next: (funcionariosRetorno: Funcionario[]) => {
        this.funcionarios = funcionariosRetorno;
        this.funcionariosFiltrados = this.funcionarios;
      },
      error: (error: any) => this.toastr.error('Falha ao carregar os funcionários', 'Erro!'),
    }).add(() => this.spinner.hide());
  }
  openModal(event: any, template: TemplateRef<any>, funcionarioId: number): void {
    event.stopPropagation();
    this.funcionarioId = funcionarioId
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.funcionarioService.deleteFuncionario(this.funcionarioId).subscribe(
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
}
