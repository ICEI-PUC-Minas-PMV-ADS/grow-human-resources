import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { Conta } from 'src/app/models/contas/Conta';
import { ContaAlterar } from 'src/app/models/contas/ContaAlterar';

@Injectable()
export class ContaService {

  private origemContaAtual = new ReplaySubject<Conta>(1);

  public contaAtual$ = this.origemContaAtual.asObservable();

  public baseURL = environment.apiURL + 'api/contas/'

  constructor(private http: HttpClient) { }

  public cadastrarConta(model: any): Observable<void> {
    return this.http
      .post<Conta>(this.baseURL + 'cadastrarconta', model)
      .pipe(take(1),
        map((contaRetorno: Conta) => {
            const conta = contaRetorno;

            if(conta) {
              this.definirContaAtual(conta);
            }
        })
      );
  }

  public definirContaAtual(conta: Conta): void {
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
      .post<Conta>(this.baseURL + 'login', model)
      .pipe(take(1),
        map((contaRetorno: Conta) => {
          const conta = contaRetorno;
          if(conta) {
            this.definirContaAtual(conta);
          }
          console.log('login', conta)
        })
      );
  }

  public recuperarConta(): Observable<ContaAlterar> {
    return this.http
      .get<ContaAlterar>(this.baseURL + 'recuperarConta').pipe(take(1));
  }

  public alterarConta(model: ContaAlterar): Observable<void> {
    return this.http.put<ContaAlterar>(this.baseURL + 'alterarConta', model).pipe(take(1),
      map((user: ContaAlterar) => {
        this.definirContaAtual(user);
    }));
  }
}
