import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { User } from 'src/app/models/identity/User';
import { AccountService } from 'src/app/services/Account.service';

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
  public userLogado = {} as User;

  constructor(
    private accountService: AccountService,
    private router: Router
  ) {
        router.events.subscribe(
      (val) => {
        if (val instanceof NavigationEnd) {
          this.accountService.currentUser$.subscribe(
            (value) => {
              this.contaLogada = value !== null;
              this.userLogado = { ...value } ;
              console.log("UserLoged", value?.visao);
        }
          )
          console.log(this.contaLogada, this.userLogado, this.userLogado.visao );
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
