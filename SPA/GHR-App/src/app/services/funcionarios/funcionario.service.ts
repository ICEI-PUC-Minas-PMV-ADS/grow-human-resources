import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { environment } from 'src/environments/environment';



@Injectable()
export class FuncionarioService {

  public baseURL = environment.apiURL +  'api/funcionarios'

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

  public postUpload(funcionarioId: number, file: File): Observable<Funcionario> {

    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload, 'foto.jpg');
    console.log(formData, fileToUpload,   )

    return this.http
      .post<Funcionario>(`${this.baseURL}/upload-image/${funcionarioId}`, formData)
      .pipe(take(1));
  }
}

