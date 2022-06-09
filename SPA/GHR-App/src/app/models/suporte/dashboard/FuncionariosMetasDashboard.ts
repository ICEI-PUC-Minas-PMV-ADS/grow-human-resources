export class FuncionariosMetasDashboard {
  funcionarioId: number;
  funcionarioAtivo: boolean;
  funcionarioCargo: string;
  funcionarioDepartamentoId: number;
  funcionarioDepartamento: string;
  funcionarioSigla: string;
  funcionarioNome: string;
  funcionarioUser: string;
  nomeDepartamento: string;
  metaCumprida: boolean;
  metaId: number;
  metaAprovada: boolean;
  nomeMeta: string;

  constructor(
    funcionarioId: number,
    funcionarioAtivo: boolean,
    funcionarioCargo: string,
    funcionarioDepartamentoId: number,
    funcionarioDepartamento: string,
    funcionarioSigla: string,
    funcionarioNome: string,
    funcionarioUser: string,
    metaCumprida: boolean,
    metaId: number,
    metaAprovada: boolean,
    nomeMeta: string,
  ) {
    this.funcionarioId = funcionarioId
    this.funcionarioAtivo = funcionarioAtivo
    this.funcionarioCargo = funcionarioCargo
    this.funcionarioDepartamentoId = funcionarioDepartamentoId
    this.funcionarioDepartamento = funcionarioDepartamento
    this.funcionarioSigla = funcionarioSigla
    this.funcionarioNome = funcionarioNome
    this.funcionarioUser = funcionarioUser
    this.metaCumprida = metaCumprida
    this.metaId = metaId
    this.metaAprovada = metaAprovada
    this.nomeMeta = nomeMeta
  }
}
