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

  @Input() titulo: string | undefined;
  @Input() subtitulo = "GROW";
  @Input() iconClass = 'fa fa-user';
  @Input() botaoListar = false;

  public contaLogada = false;
  public contaAtiva = {} as ContaAtiva;

  constructor(
    private contaService: ContaService,
    private router: Router
  ) {
        router.events.subscribe(
      (val) => {
        if (val instanceof NavigationEnd) {
          this.contaService.contaAtual$.subscribe(
            (value) => {
              this.contaLogada = value !== null;
              this.contaAtiva = { ...value } ;
              console.log("UserLoged", value?.visao);
        }
          )
          console.log(this.contaLogada, this.contaAtiva, this.contaAtiva.visao );
        }
      }
    )
   }

  ngOnInit(): void {
  }
  listar(): void {
    this.router.navigate([`${this.titulo.toLocaleLowerCase()}/lista`]);

  }

}
