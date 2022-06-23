import { Component, OnInit } from "@angular/core";

import { NavegacaoEntreForms } from "src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms";

@Component({
  selector: 'app-funcionario-detalhe',
  templateUrl: './funcionario-detalhe.component.html',
  styleUrls: ['./funcionario-detalhe.component.scss']
})
export class FuncionarioDetalheComponent implements OnInit {

  public formsParametros = {} as NavegacaoEntreForms;

  public prosseguir: boolean ;

  public get mostrarAbas(): boolean {
    return (this.formsParametros.contaOk || !this.formsParametros.visaoRhLogado)
    ? true : false;
  }

  constructor(
    ) {  }

  ngOnInit(): void {
  }

  public recuperarValorForm(formsParametros: NavegacaoEntreForms): void {
    this.formsParametros = formsParametros;
  }
}



