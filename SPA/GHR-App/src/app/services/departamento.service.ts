import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'

import { Departamento } from '../models/Departamento';

@Injectable(
  //{ providedIn: 'root' }
)
export class DepartamentoService {

  public baseURL = environment.apiURL + 'api/departamentos'

  constructor(private http: HttpClient) { }

  public getDepartamentos(): Observable<Departamento[]> {
    return this.http
      .get<Departamento[]>(this.baseURL)
      .pipe(take(1));
  }

  public getDepartamentosByNomeCompleto(nome: string): Observable<Departamento[]> {
    return this.http
      .get<Departamento[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public getDepartamentoById(id: number): Observable<Departamento> {
    return this.http
      .get<Departamento>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(departamento: Departamento): Observable<Departamento> {
    return this.http
      .post<Departamento>(this.baseURL, departamento)
      .pipe(take(1));
  }

  public put(departamento: Departamento): Observable<Departamento> {
    return this.http
      .put<Departamento>(`${this.baseURL}/${departamento.id}`, departamento)
      .pipe(take(1));
  }

  public deleteFuncionario(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}

