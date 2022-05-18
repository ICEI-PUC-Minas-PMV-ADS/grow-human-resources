import { AccountService } from 'src/app/services/Account.service';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { User } from 'src/app/models/identity/User';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  isCollapsed = true;
  contaLogada = false;
  userLogado = {} as User;

  constructor(
    public accountService: AccountService,
    private router: Router,
  ) { 
    router.events.subscribe(
      (val) => {
        if (val instanceof NavigationEnd) {
          this.accountService.currentUser$.subscribe(
            (value) => {
              this.contaLogada = value !== null;
              this.userLogado = { ...value } ;
              console.log(value.visao);
        }
          )
          console.log(this.contaLogada, this.userLogado, this.userLogado.visao );
        }
      }
    )
  }

  ngOnInit() {
    console.log(this.accountService.currentUser$);
  }
  
  showMenu(): boolean {
    return this.router.url !== '/user/login'
  }
  
  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
  }
}
