export class ListaDashboard {
  qtde: number;
  nome: string;
  qtdeTotal: number;
  percent: number;
  styleColor: String;
  stylePercent: String;

  constructor(qtde: number, nome: string, qtdeTotal: number, style: string) {
    this.qtde = qtde;
    this.nome = nome;
    this.qtdeTotal = qtdeTotal;
    this.percent = +((qtde / qtdeTotal * 100).toFixed(2));
    this.styleColor = style;
    this.stylePercent = this.percent + '%';
   }
}
