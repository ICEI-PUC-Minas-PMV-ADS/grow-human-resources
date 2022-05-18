import { UserUpdate } from './../models/identity/UserUpdate';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/identity/User';

@Injectable()
export class AccountService {

  private currentUserSource = new ReplaySubject<User>(1);

  public currentUser$ = this.currentUserSource.asObservable();

  public baseURL = environment.apiURL + 'api/account/'

  constructor(private http: HttpClient) { }

  public cadastrarConta(model: any): Observable<void> {
console.log(model)
    return this.http.post<User>(this.baseURL + 'cadastrarconta', model)
               .pipe(take(1),
                     map((response: User) => {
                        const user = response;

                        if(user) {
                          this.setCurrentUser(user);
                        }
                     })
            );
  }

  public setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  public logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
  }

  public login(model: any): Observable<void> {

    return this.http.post<User>(this.baseURL + 'login', model)
               .pipe(take(1),
                     map((response: User) => {
                        const user = response;
                        if(user) {
                          this.setCurrentUser(user);
                        }
                     })
               );
  }
  public getUser(): Observable<UserUpdate> {
    return this.http.get<UserUpdate>(this.baseURL + 'getUser').pipe(take(1));
  }

  public updateUser(model: UserUpdate): Observable<void> {
    return this.http.put<UserUpdate>(this.baseURL + 'AlterarConta', model).pipe(take(1),
      map((user: UserUpdate) => {
        this.setCurrentUser(user);
    }));
  }
}
