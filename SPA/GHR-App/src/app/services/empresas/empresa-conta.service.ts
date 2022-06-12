import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { EmpresaConta } from 'src/app/models/Empresas/EmpresaConta';
import { environment } from 'src/environments/environment';

@Injectable()
export class EmpresaContaService {

  public baseURL = environment.apiURL + 'api/empresasContas'

  constructor(private http: HttpClient) { }

  public recuperarEmpresaContaPorEmpresaIdUserNameAsync(empresaId: number, userName: string): Observable<EmpresaConta> {
    return this.http
      .get<EmpresaConta>(`${this.baseURL}/${empresaId}/${userName}`)
      .pipe(take(1));
  }

  public cadastrarEmpresaConta(empresaConta: EmpresaConta): Observable<EmpresaConta> {
    console.log("service", empresaConta)
    return this.http
      .post<EmpresaConta>(this.baseURL, empresaConta)
      .pipe(take(1));
  }
}
