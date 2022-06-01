import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';

import { environment } from 'src/environments/environment';


@Injectable()
export class FuncionarioMetaService {
  public baseURL = environment.apiURL + 'api/funcionariosMetas'

  constructor(private http: HttpClient) { }

  public recuperarFuncionariosMetas(pagina?: number, itensPorPagina?: number, termo?: string): Observable<ResultadoPaginacao<FuncionarioMeta[]>> {

    const resultadoPaginacao: ResultadoPaginacao<FuncionarioMeta[]> = new ResultadoPaginacao<FuncionarioMeta[]>();

    let params = new HttpParams;

    if (pagina != null && itensPorPagina != null) {
      params = params.append('numeroDaPagina', pagina.toString());
      params = params.append('tamanhoDaPagina', itensPorPagina.toString());
    };

    if (termo != null && termo != '')
      params = params.append('termo', termo);

    return this.http
      .get<FuncionarioMeta[]>(this.baseURL, {observe: 'response', params})
      .pipe(
        take(1),
        map((response) => {          resultadoPaginacao.resultado = response.body;
          if (response.headers.has('Paginacao')) {
            resultadoPaginacao.paginacao = JSON.parse(response.headers.get('Paginacao'));
          }
          return resultadoPaginacao;
        }));
  }

  public recuperarMetasPorFuncionarioId(funcionarioId: number, pagina?: number, itensPorPagina?: number, termo?: string): Observable<ResultadoPaginacao<FuncionarioMeta[]>> {

    const resultadoPaginacao: ResultadoPaginacao<FuncionarioMeta[]> = new ResultadoPaginacao<FuncionarioMeta[]>();

    let params = new HttpParams;

    if (pagina != null && itensPorPagina != null) {
      params = params.append('numeroDaPagina', pagina.toString());
      params = params.append('tamanhoDaPagina', itensPorPagina.toString());
    };

    if (termo != null && termo != '')
      params = params.append('termo', termo);

    return this.http
      .get<FuncionarioMeta[]>(`${this.baseURL}/${funcionarioId}`, {observe: 'response', params})
      .pipe(
        take(1),
        map((response) => {          resultadoPaginacao.resultado = response.body;
          if (response.headers.has('Paginacao')) {
            resultadoPaginacao.paginacao = JSON.parse(response.headers.get('Paginacao'));
          }
          return resultadoPaginacao;
        }));
  }

  public recuperarFuncionarioIdMetaId(funcionarioId: number, metaId:number): Observable<FuncionarioMeta> {
    return this.http
      .get<FuncionarioMeta>(`${this.baseURL}/${funcionarioId}/${metaId}/meta`)
      .pipe(take(1));
  }

  public criarFuncionarioMeta(funcionarioMeta: FuncionarioMeta): Observable<FuncionarioMeta> {

    return this.http
      .post<FuncionarioMeta>(this.baseURL, funcionarioMeta)
      .pipe(take(1));
  }

  public salvarFuncionarioMeta(funcionarioMeta: FuncionarioMeta): Observable<FuncionarioMeta> {
    return this.http
      .put<FuncionarioMeta>(this.baseURL, funcionarioMeta)
      .pipe(take(1));
  }

  public excluirFuncionarioMeta(funcionarioId: number, metaId:number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${funcionarioId}/${metaId}`)
      .pipe(take(1));
  }

}
