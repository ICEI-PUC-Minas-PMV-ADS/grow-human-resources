import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Empresa } from 'src/app/models/Empresas/Empresa';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {

  public baseURL = environment.apiURL + 'api/empresas'

  constructor(private http: HttpClient) { }

  public recuperarEmpresas(): Observable<Empresa[]> {
    return this.http
      .get<Empresa[]>(`${this.baseURL}`)
      .pipe(take(1));
  }
}
