import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-funcionarios',
  templateUrl: './funcionarios.component.html',
  styleUrls: ['./funcionarios.component.scss']
})
export class FuncionariosComponent implements OnInit {

  public funcionarios: any = [];
  public funcionariosFiltrados: any = [];

  larguraImg: number = 100;
  margemImg: number = 2;
  exibirImg: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(filtro: string) {
    this._filtroLista = filtro;
    this.funcionariosFiltrados = this.filtroLista
      ? this.filtrarFuncionarios(this.filtroLista)
      : this.funcionarios;
  }

  filtrarFuncionarios(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.funcionarios.filter(
      (funcionario: any) => funcionario.nomeCompleto.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        funcionario.email.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        funcionario.telefone.toLocaleLowerCase().indexOf(filtrarPor) !== -1 
    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getFuncionarios();
  }
  alternarImagem() {
    this.exibirImg = !this.exibirImg;
  }
  public getFuncionarios(): void {
    this.http.get('https://localhost:5001/api/funcionarios').subscribe(
      response => {
        this.funcionarios = response;
        this.funcionariosFiltrados = this.funcionarios;
      },
      error => console.log(error)
    );
  }
}
