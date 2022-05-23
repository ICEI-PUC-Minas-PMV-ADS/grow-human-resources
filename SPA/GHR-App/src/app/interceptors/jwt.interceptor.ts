import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { ContaAtiva } from '../models/contas/ContaAtiva';

import { ContaService } from '../services/contas/Conta.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private contaService: ContaService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let contaAtual: ContaAtiva;

    this.contaService.contaAtual$.pipe(take(1)).subscribe(user => {
      contaAtual = user

      if (contaAtual) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${contaAtual.token}`
          }
        });
      }
    });
    return next.handle(request);
  }
}
