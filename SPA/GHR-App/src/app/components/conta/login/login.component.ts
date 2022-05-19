import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Conta } from 'src/app/models/contas/Conta';

import { ContaLogin } from 'src/app/models/contas/ContaLogin';

import { ContaService } from 'src/app/services/contas/Conta.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public model = {} as ContaLogin;

  constructor(
    private contaService: ContaService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
  }

  public login(): void {
    this.spinner.show();

    this.contaService.login(this.model).subscribe(
      () => {
        this.router.navigateByUrl('/home');
      },
      (error: any) => {
        if (error.status = 401)
        this.toastr.error('Conta ou senha inválidos.', 'Erro!');
        else {
          console.error(error);
          this.toastr.error('Falha ao realizar login.', 'Erro!');
        }
      }
      ).add(() => this.spinner.hide())

      //window.location.reload();
  }
}
