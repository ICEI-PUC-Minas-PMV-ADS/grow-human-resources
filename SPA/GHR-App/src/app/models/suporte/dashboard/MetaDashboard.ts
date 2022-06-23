export class MetaDashboard {
  funcionarioId: number;
  funcionarioAtivo: boolean;
  metaCumprida: boolean;
  metaId: number;
  metaAprovada: boolean;
  nomeMeta: string;

  constructor(
    funcionarioId: number,
    funcionarioAtivo: boolean,
    metaCumprida: boolean,
    metaId: number,
    metaAprovada: boolean,
    nomeMeta: string,
  ) {
    this.funcionarioId = funcionarioId;
    this.funcionarioAtivo = funcionarioAtivo;
    this.metaCumprida = metaCumprida;
    this.metaId = metaId
    this.metaAprovada = metaAprovada;
    this.nomeMeta = nomeMeta;
  }
}
