import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { ContaService } from 'src/app/services/contas/Conta.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  isCollapsed = true;
  contaLogada = false;
  contaAtiva = {} as ContaAtiva;
  visaoRH = false;

  constructor(
    public contaService: ContaService,
    private router: Router,
  ) {
    router.events.subscribe(
      (verificarConta) => {
        if (verificarConta instanceof NavigationEnd) {
          this.contaService.contaAtual$.subscribe(
            (contaAtiva) => {
              this.contaLogada = contaAtiva !== null;
              this.contaAtiva = { ...contaAtiva } ;
              this.visaoRH = this.contaAtiva.visao?.includes('RH') ;})
        }
      })
    }

  ngOnInit() {

  }

  showMenu(): boolean {
    return this.router.url !== '/conta/login'
  }

  logout(): void {
    this.contaService.logout();
    this.router.navigateByUrl('/conta/login');
  }
}
