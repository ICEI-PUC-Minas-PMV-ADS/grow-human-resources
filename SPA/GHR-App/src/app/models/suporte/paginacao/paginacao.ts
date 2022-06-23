export class Paginacao {
  paginaAtual: number;
  itensPorPagina: number;
  totalItens: number;
  totalDePaginas: number;
}

export class ResultadoPaginacao<T> {
  resultado: T;
  paginacao: Paginacao;
}

