export class Paginacao {
  paginaAtual: number;
  itensPorPagina: number;
  totalItens: number;
  totalDePaginaS: number;
}

export class ResultadoPaginacao<T> {
  resultado: T;
  paginacao: Paginacao;
}

