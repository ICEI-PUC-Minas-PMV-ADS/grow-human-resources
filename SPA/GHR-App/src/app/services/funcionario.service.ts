import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { Funcionario } from '../models/Funcionario';

@Injectable(
  //{ providedIn: 'root' }
)
export class FuncionarioService {

  public baseURL = 'https://localhost:5001/api/funcionarios'

  constructor(private http: HttpClient) { }

  public getFuncionarios(): Observable<Funcionario[]> {
    return this.http.get<Funcionario[]>(this.baseURL);
  }

  public getFuncionariosByNomeCompleto(nome: string): Observable<Funcionario[]> {
    return this.http.get<Funcionario[]>(`${this.baseURL}/${nome}/nome`);
  }

  public getFuncionarioById(id: number): Observable<Funcionario> {
    return this.http.get<Funcionario>(`${this.baseURL}/${id}`);
  }
}
