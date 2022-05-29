import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators'

import { Meta } from 'src/app/models/metas/Meta';

import { environment } from 'src/environments/environment';

import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';

@Injectable()
export class MetaService {

  public baseURL = environment.apiURL + 'api/metas'

  constructor(private http: HttpClient) { }

  public recuperarMetas(pagina?: number, itensPorPagina?: number, termo?: string): Observable<ResultadoPaginacao<Meta[]>> {

    const resultadoPaginacao: ResultadoPaginacao<Meta[]> = new ResultadoPaginacao<Meta[]>();

    let params = new HttpParams;

    if (pagina != null && itensPorPagina != null) {
      params = params.append('numeroDaPagina', pagina.toString());
      params = params.append('tamanhoDaPagina', itensPorPagina.toString());
    };

    console.log("termo", termo)
    if (termo != null && termo != '')
      params = params.append('termo', termo);

    return this.http
      .get<Meta[]>(this.baseURL, {observe: 'response', params})
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

  public recuperarMetaPorId(id: number): Observable<Meta> {
    return this.http
      .get<Meta>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public criarMeta(meta: Meta): Observable<Meta> {
    return this.http
      .post<Meta>(this.baseURL, meta)
      .pipe(take(1));
  }

  public SalvarMeta(meta: Meta): Observable<Meta> {
    return this.http
      .put<Meta>(`${this.baseURL}/${meta.id}`, meta)
      .pipe(take(1));
  }

  public excluirMeta(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}

