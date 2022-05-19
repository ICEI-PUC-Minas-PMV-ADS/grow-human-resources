import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/internal/Observable';
import { take } from 'rxjs/operators';

import { Cargo } from 'src/app/models/cargos/Cargo';

import { environment } from 'src/environments/environment';

@Injectable()
export class CargoService {

  public baseURL = environment.apiURL + 'api/cargos'

  constructor(private http: HttpClient) { }

  public recuperarCargos(): Observable<Cargo[]> {
    return this.http
      .get<Cargo[]>(this.baseURL)
      .pipe(take(1));
  }

  public recuperarCargosporNomeCargo(nome: string): Observable<Cargo[]> {
    return this.http
      .get<Cargo[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public recuperarCargosPorDescricaoMeta(descricao: string): Observable<Cargo[]> {
    return this.http
      .get<Cargo[]>(`${this.baseURL}/${descricao}/descricao`)
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
