import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, AbstractControlOptions, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { UserLogin } from 'src/app/models/identity/UserLogin';
import { AccountService } from 'src/app/services/Account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model = {} as UserLogin;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
  }

  public login(): void {
    this.accountService.login(this.model).subscribe(
      () => { this.router.navigateByUrl('/funcionarios'); },
      (error: any) => {
        if (error.status = 401)
          this.toastr.error('Usuário ou senha inválidos.', 'Erro!');
        else {
          console.error(error);
          this.toastr.error('Falha ao realizar login.', 'Erro!');
        }
      }
    )
  }
}
