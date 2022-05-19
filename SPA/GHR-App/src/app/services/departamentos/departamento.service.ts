import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'

import { Departamento } from 'src/app/models/departamentos/Departamento';

import { environment } from 'src/environments/environment';

@Injectable()
export class DepartamentoService {

  public baseURL = environment.apiURL + 'api/departamentos'

  constructor(private http: HttpClient) { }

  public recuperarDepartamentos(): Observable<Departamento[]> {
    return this.http
      .get<Departamento[]>(this.baseURL)
      .pipe(take(1));
  }

  public recuperarDepartamentosByNomeCompleto(nome: string): Observable<Departamento[]> {
    return this.http
      .get<Departamento[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public recuperarDepartamentoById(id: number): Observable<Departamento> {
    return this.http
      .get<Departamento>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public cadastrarDepatamento(departamento: Departamento): Observable<Departamento> {
    return this.http
      .post<Departamento>(this.baseURL, departamento)
      .pipe(take(1));
  }

  public alterarDepartamento(departamento: Departamento): Observable<Departamento> {
    return this.http
      .put<Departamento>(`${this.baseURL}/${departamento.id}`, departamento)
      .pipe(take(1));
  }

  public excluirFuncionario(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}

