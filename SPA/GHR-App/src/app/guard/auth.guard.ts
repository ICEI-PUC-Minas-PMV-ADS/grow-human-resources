import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import {  CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private toastr: ToastrService
  ) {
  }
  canActivate(): boolean {
    
    if (localStorage.getItem('user') !== null)
      return true;
    
    this.toastr.info('Conta não autenticada!')
    this.router.navigate(['/user/login']);
    return false;
  }
  
}
