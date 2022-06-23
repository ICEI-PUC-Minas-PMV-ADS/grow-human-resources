import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'
import { DadoPessoal } from 'src/app/models/funcionarios/DadoPessoal';
import { environment } from 'src/environments/environment';



@Injectable()
export class DadosPessoaisService {

  public baseURL = environment.apiURL +  'api/funcionariosdadospessoais'

  constructor(private http: HttpClient) { }


  public recuperarDadosPessoaisPorId(id: number): Observable<DadoPessoal> {
    return this.http
      .get<DadoPessoal>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
  public criarDadoPessoal(dadoPessoal: DadoPessoal): Observable<DadoPessoal> {
    return this.http
      .post<DadoPessoal>(this.baseURL, dadoPessoal)
      .pipe(take(1));
  }

  public salvarDadoPessoal(dadoPessoal: DadoPessoal): Observable<DadoPessoal> {
    return this.http
      .put<DadoPessoal>(`${this.baseURL}/${dadoPessoal.id}`, dadoPessoal)
      .pipe(take(1));
  }

  public excluirDadoPessoal(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}

