import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { FuncionarioMeta } from '../models/FuncionarioMeta';

@Injectable({
  providedIn: 'root'
})
export class FuncionarioMetaService {
  public baseURL = 'https://localhost:5001/api/funcionariosMetas'

  constructor(private http: HttpClient) { }

  public getMetasByFuncionarioId(funcionarioId: number): Observable<FuncionarioMeta[]> {
     console.log("console func.", `${this.baseURL}/${funcionarioId}`)
    return this.http
      .get<FuncionarioMeta[]>(`${this.baseURL}/${funcionarioId}`)
      .pipe(take(1));
  }

  public getMeta(funcionarioId: number, metaId: number): Observable<FuncionarioMeta> {
    return this.http
      .get<FuncionarioMeta>(`${this.baseURL}/${funcionarioId}/${metaId}/meta`)
      .pipe(take(1));
  }

  public post(funcionarioMeta: FuncionarioMeta): Observable<FuncionarioMeta> {

    return this.http
      .post<FuncionarioMeta>(this.baseURL, funcionarioMeta)
      .pipe(take(1));
  }

  public put(funcionarioMeta: FuncionarioMeta): Observable<FuncionarioMeta> {
    return this.http
      .put<FuncionarioMeta>(`${this.baseURL}/${funcionarioMeta.funcionarioId}/${funcionarioMeta.metaId}`, funcionarioMeta)
      .pipe(take(1));
  }

  public deleteFuncionarioMeta(funcionarioId: number, metaId:number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${funcionarioId}/${metaId}`)
      .pipe(take(1));
  }

}
