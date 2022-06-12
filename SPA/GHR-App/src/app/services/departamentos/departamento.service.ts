import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators'

import { Departamento } from 'src/app/models/departamentos/Departamento';

import { environment } from 'src/environments/environment';

import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
@Injectable()
export class DepartamentoService {

  public baseURL = environment.apiURL + 'api/departamentos'

  constructor(private http: HttpClient) { }

  public recuperarDepartamentos(pagina?: number, itensPorPagina?: number, termo?: string): Observable<ResultadoPaginacao<Departamento[]>> {

    const resultadoPaginacao: ResultadoPaginacao<Departamento[]> = new ResultadoPaginacao<Departamento[]>();

    let params = new HttpParams;

    if (pagina != null && itensPorPagina != null) {
      params = params.append('numeroDaPagina', pagina.toString());
      params = params.append('tamanhoDaPagina', itensPorPagina.toString());
    };

    if (termo != null && termo != '')
      params = params.append('termo', termo);

    return this.http
      .get<Departamento[]>(this.baseURL, {observe: 'response', params})
      .pipe(
        take(1),
        map((response) => {          resultadoPaginacao.resultado = response.body;
          if (response.headers.has('Paginacao')) {
            resultadoPaginacao.paginacao = JSON.parse(response.headers.get('Paginacao'));
          }
          return resultadoPaginacao;
        }));
  }

  public recuperarDepartamentoById(id: number): Observable<Departamento> {
    return this.http
      .get<Departamento>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public cadastrarDepartamento(departamento: Departamento): Observable<Departamento> {
    return this.http
      .post<Departamento>(this.baseURL, departamento)
      .pipe(take(1));
  }

  public alterarDepartamento(departamento: Departamento): Observable<Departamento> {
    return this.http
      .put<Departamento>(`${this.baseURL}/${departamento.id}`, departamento)
      .pipe(take(1));
  }

  public excluirDepartamento(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}

