import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/internal/Observable';
import { map, take } from 'rxjs/operators';

import { Cargo } from 'src/app/models/cargos/Cargo';
import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';

import { environment } from 'src/environments/environment';

@Injectable()
export class CargoService {

  public baseURL = environment.apiURL + 'api/cargos'

  constructor(private http: HttpClient) { }

  public recuperarCargos(pagina?: number, itensPorPagina?: number, termo?: string): Observable<ResultadoPaginacao<Cargo[]>> {

    const resultadoPaginacao: ResultadoPaginacao<Cargo[]> = new ResultadoPaginacao<Cargo[]>();

    let params = new HttpParams;

    if (pagina != null && itensPorPagina != null) {
      params = params.append('numeroDaPagina', pagina.toString());
      params = params.append('tamanhoDaPagina', itensPorPagina.toString());
    };

    console.log("termo", termo)
    if (termo != null && termo != '')
      params = params.append('termo', termo);

    return this.http
      .get<Cargo[]>(this.baseURL, {observe: 'response', params})
      .pipe(
        take(1),
        map((response) => {          resultadoPaginacao.resultado = response.body;
          if (response.headers.has('Paginacao')) {
            resultadoPaginacao.paginacao = JSON.parse(response.headers.get('Paginacao'));
          }
          return resultadoPaginacao;
        }));
  }

  public recuperarCargosporNomeCargo(nome: string): Observable<Cargo[]> {
    return this.http
      .get<Cargo[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public recuperarCargoPorId(id: number): Observable<Cargo> {
    return this.http
      .get<Cargo>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(meta: Cargo): Observable<Cargo> {
    return this.http
      .post<Cargo>(this.baseURL, meta)
      .pipe(take(1));
  }

  public put(meta: Cargo): Observable<Cargo> {
    return this.http
      .put<Cargo>(`${this.baseURL}/${meta.id}`, meta)
      .pipe(take(1));
  }

  public excluirCargo(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
