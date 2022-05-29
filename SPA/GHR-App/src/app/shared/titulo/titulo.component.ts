import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { ContaService } from 'src/app/services/contas/Conta.service';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

  public contaLogada = false;
  public contaAtiva = {} as ContaAtiva;

  @Input() titulo: string | undefined;
  @Input() subtitulo = "GROW";
  @Input() iconClass = 'fa fa-user';
  @Input() botaoListar = false;
  @Input() visao = '';


  constructor(
    private contaService: ContaService,
    private router: Router
  ) {
        router.events.subscribe(
      (verificarConta) => {
        if (verificarConta instanceof NavigationEnd) {
          this.contaService.contaAtual$.subscribe(
            (contaAtvia) => {
              this.contaLogada = contaAtvia !== null;
              this.contaAtiva = { ...contaAtvia } ;})
        }
      })
   }

  ngOnInit(): void {
    this.visao = this.contaAtiva.visao;
  }
  listar(): void {
    this.router.navigate([`${this.titulo.toLocaleLowerCase()}/lista`]);

  }

}
