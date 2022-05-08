import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'

import { Funcionario } from '../models/Funcionario';

@Injectable(
  //{ providedIn: 'root' }
)
export class FuncionarioService {

  public baseURL = 'https://localhost:5001/api/funcionarios'

  constructor(private http: HttpClient) { }

  public getFuncionarios(): Observable<Funcionario[]> {
    return this.http
      .get<Funcionario[]>(this.baseURL)
      .pipe(take(1));
  }

  public getFuncionariosByNomeCompleto(nome: string): Observable<Funcionario[]> {
    return this.http
      .get<Funcionario[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public getFuncionarioById(id: number): Observable<Funcionario> {
    return this.http
      .get<Funcionario>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(funcionario: Funcionario): Observable<Funcionario> {
    return this.http
      .post<Funcionario>(this.baseURL, funcionario)
      .pipe(take(1));
  }

  public put(funcionario: Funcionario): Observable<Funcionario> {
    return this.http
      .put<Funcionario>(`${this.baseURL}/${funcionario.id}`, funcionario)
      .pipe(take(1));
  }

  public deleteFuncionario(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}

