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

  public recuperarFuncionarios(): Observable<Funcionario[]> {
    return this.http
      .get<Funcionario[]>(this.baseURL)
      .pipe(take(1));
  }

  public recuperarFuncionariosPorNomeCompleto(nome: string): Observable<Funcionario[]> {
    return this.http
      .get<Funcionario[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public recuperarFuncionarioPorId(id: number): Observable<Funcionario> {
    return this.http
      .get<Funcionario>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
  public recuperarFuncionarioPorContaId(contaId: number): Observable<Funcionario> {
    return this.http
      .get<Funcionario>(`${this.baseURL}/${contaId}/contaId`)
      .pipe(take(1));
  }

  public criarFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.http
      .post<Funcionario>(this.baseURL, funcionario)
      .pipe(take(1));
  }

  public salvarFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.http
      .put<Funcionario>(`${this.baseURL}/${funcionario.id}`, funcionario)
      .pipe(take(1));
  }

  public excluirFuncionario(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public postUpload(funcionarioId: number, file: File): Observable<Funcionario> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload);
    console.log('formdata', formData, fileToUpload )

    return this.http
      .post<Funcionario>(`${this.baseURL}/salvar-imagem/${funcionarioId}`, formData)
      .pipe(take(1));
  }
}

