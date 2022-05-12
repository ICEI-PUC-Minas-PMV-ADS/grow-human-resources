import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'

import { Meta } from '../models/Meta';

@Injectable(
  //{ providedIn: 'root' }
)
export class MetaService {

  public baseURL = 'https://localhost:5001/api/metas'

  constructor(private http: HttpClient) { }

  public getMetas(): Observable<Meta[]> {
    return this.http
      .get<Meta[]>(this.baseURL)
      .pipe(take(1));
  }

  public getMetasByNomeMeta(nome: string): Observable<Meta[]> {
    return this.http
      .get<Meta[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  public getMetasByDescricaoMeta(descricao: string): Observable<Meta[]> {
    return this.http
      .get<Meta[]>(`${this.baseURL}/${descricao}/descricao`)
      .pipe(take(1));
  }

  public getMetasByMetaCumprida(metaCumprida: boolean): Observable<Meta[]> {
    return this.http
      .get<Meta[]>(`${this.baseURL}/${metaCumprida}/metaCumrpida`)
      .pipe(take(1));
  }

  public getMetasByMetaAprovada(metaAprovada: boolean): Observable<Meta[]> {
    return this.http
      .get<Meta[]>(`${this.baseURL}/${metaAprovada}/metaAprovada`)
      .pipe(take(1));
   }

  public getMetaById(id: number): Observable<Meta> {
    return this.http
      .get<Meta>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(meta: Meta): Observable<Meta> {
    return this.http
      .post<Meta>(this.baseURL, meta)
      .pipe(take(1));
  }

  public put(meta: Meta): Observable<Meta> {
    return this.http
      .put<Meta>(`${this.baseURL}/${meta.id}`, meta)
      .pipe(take(1));
  }

  public deleteMeta(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}

