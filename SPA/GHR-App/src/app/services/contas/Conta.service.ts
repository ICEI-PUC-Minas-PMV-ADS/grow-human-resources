import { ContaVisao } from './../../models/contas/ContaVisao';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { Conta } from 'src/app/models/contas/Conta';

@Injectable()
export class ContaService {

  private origemContaAtual = new ReplaySubject<ContaAtiva>(1);

  public contaAtual$ = this.origemContaAtual.asObservable();

  public baseURL = environment.apiURL + 'api/contas/'

  constructor(private http: HttpClient) { }

  public cadastrarConta(model: any): Observable<void> {
    return this.http
      .post<ContaAtiva>(this.baseURL + 'cadastrarconta', model)
      .pipe(take(1),
        map((contaRetorno: ContaAtiva) => {
            const conta = contaRetorno;
            if(conta) {
              this.definirContaAtual(conta);
            }
        })
      );
  }

  public definirContaAtual(conta: ContaAtiva): void {
    localStorage.setItem('conta', JSON.stringify(conta));
    this.origemContaAtual.next(conta);
  }

  public logout(): void {
    localStorage.removeItem('conta');
    this.origemContaAtual.next(null);
    this.origemContaAtual.complete();
  }

  public login(model: any): Observable<void> {
    return this.http
      .post<ContaAtiva>(this.baseURL + 'login', model)
      .pipe(take(1),
        map((contaRetorno: ContaAtiva) => {
          const conta = contaRetorno;
          if(conta) {
            this.definirContaAtual(conta);
          }
        })
      );
  }

  public recuperarContaAtiva(): Observable<Conta> {
    return this.http
      .get<Conta>(this.baseURL + 'recuperarContaAtiva').pipe(take(1));
  }

  public recuperarContaPorUserName(userName: string): Observable<Conta> {
    return this.http
      .get<Conta>(this.baseURL + `${userName}/userName`).pipe(take(1));
  }

  public recuperarContaPorId(id: number): Observable<Conta> {
    return this.http
      .get<Conta>(this.baseURL + `${id}`).pipe(take(1));
  }

  public alterarConta(model: Conta): Observable<void> {
    return this.http.put<Conta>(this.baseURL + 'alterarContaToken', model).pipe(take(1),
      map((user: Conta) => {
        this.definirContaAtual(user);
      }));
  }

  public atualizarConta(model: ContaVisao): Observable<any> {
    return this.http
      .put<Conta>(this.baseURL + 'alterarContaVisao', model)
      .pipe(take(1));
  }

  public salvarImagem(file: File): Observable<ContaVisao> {
    const salvarArquivo = file[0] as File;
    const formData = new FormData();

    formData.append('file', salvarArquivo);

    return this.http
      .post<ContaVisao>(`${this.baseURL}upload-imagem`, formData)
      .pipe(take(1));
  }
}
