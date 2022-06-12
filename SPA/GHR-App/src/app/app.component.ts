import { Component } from '@angular/core';
import { ContaAtiva } from './models/contas/ContaAtiva';
import { ContaService } from './services/contas/Conta.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(
    private contaService: ContaService
  ) { }

  ngOnInit(): void{
    this.definirContaAtual()
  }

  public definirContaAtual(): void {
    let conta: ContaAtiva;

    if (localStorage.getItem('conta')) {
      conta = JSON.parse(localStorage.getItem('conta') ?? '{}');
    } else {
      conta = null
    }

    if (conta)
      this.contaService.definirContaAtual(conta);
  }
}
