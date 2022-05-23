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
      (val) => {
        if (val instanceof NavigationEnd) {
          this.contaService.contaAtual$.subscribe(
            (value) => {
              this.contaLogada = value !== null;
              this.contaAtiva = { ...value } ;
              this.visaoRH = this.contaAtiva.visao.includes('RH');
              console.log(this.contaLogada, this.contaAtiva, this.contaAtiva.visao);
              console.log('Menu', this.contaAtiva.visao, this.visaoRH);
            }
            )
          }
        }
        )
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
