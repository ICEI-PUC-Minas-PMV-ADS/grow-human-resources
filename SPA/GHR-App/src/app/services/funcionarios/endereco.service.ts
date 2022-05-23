import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Endereco } from 'src/app/models/funcionarios/Endereco';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {

  public baseURL = environment.apiURL +  'api/funcionariosEnderecos'

  constructor(private http: HttpClient) { }


  public recuperarEnderecosPorId(id: number): Observable<Endereco> {
    return this.http
      .get<Endereco>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
  public criarEndereco(endereco: Endereco): Observable<Endereco> {
    return this.http
      .post<Endereco>(this.baseURL, endereco)
      .pipe(take(1));
  }

  public salvarEndereco(endereco: Endereco): Observable<Endereco> {
    return this.http
      .put<Endereco>(`${this.baseURL}/${endereco.id}`, endereco)
      .pipe(take(1));
  }

  public excluirEndereco(id: number): Observable<any> {
    return this.http
      .delete<string>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }


}
