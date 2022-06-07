import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators'

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';

import { environment } from 'src/environments/environment';

import { ResultadoPaginacao } from '../../models/suporte/paginacao/paginacao';



@Injectable()
export class FuncionarioService {

  public baseURL = environment.apiURL +  'api/funcionarios'

  constructor(private http: HttpClient) { }

  public recuperarFuncionarios(pagina?: number, itensPorPagina?: number, termo?: string): Observable<ResultadoPaginacao<Funcionario[]>> {

    const resultadoPaginacao: ResultadoPaginacao<Funcionario[]> = new ResultadoPaginacao<Funcionario[]>();

    let params = new HttpParams;

    if (pagina != null && itensPorPagina != null) {
      params = params.append('numeroDaPagina', pagina.toString());
      params = params.append('tamanhoDaPagina', itensPorPagina.toString());
    };

    if (termo != null && termo != '')
      params = params.append('termo', termo);

    return this.http
      .get<Funcionario[]>(this.baseURL, {observe: 'response', params})
      .pipe(
        take(1),
        map((response) => {
          resultadoPaginacao.resultado = response.body;

          if (response.headers.has('Paginacao')) {
            resultadoPaginacao.paginacao = JSON.parse(response.headers.get('Paginacao'));
          }

          return resultadoPaginacao;
        }));
  }

  public recuperarFuncionarioPorId(id: number): Observable<Funcionario> {
    return this.http
      .get<Funcionario>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
  public recuperarFuncionarioPorContaId(contaId: number): Observable<Funcionario> {
    return this.http
      .get<Funcionario>(`${this.baseURL}/${contaId}/contaId`)
      .pipe(take(1));
  }

  public criarFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.http
      .post<Funcionario>(this.baseURL, funcionario)
      .pipe(take(1));
  }

  public salvarFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.http
      .put<Funcionario>(`${this.baseURL}/${funcionario.id}`, funcionario)
      .pipe(take(1));
  }

  public excluirFuncionario(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public postUpload(funcionarioId: number, file: File): Observable<Funcionario> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload);

    return this.http
      .post<Funcionario>(`${this.baseURL}/salvar-imagem/${funcionarioId}`, formData)
      .pipe(take(1));
  }

  public recuperarFuncionarioPorDepartamentoId(departamentoId: number): Observable<Funcionario[]> {
    return this.http
      .get<Funcionario[]>(`${this.baseURL}/${departamentoId}/deptoId`)
      .pipe(take(1));
  }
}

