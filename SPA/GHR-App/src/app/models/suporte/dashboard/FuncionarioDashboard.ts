export class FuncionarioDashboard {
  funcionarioId: number;
  funcionarioAtivo: boolean;
  funcionarioCargo: string;
  funcionarioDepartamentoId: number;
  funcionarioDepartamento: string;
  funcionarioSigla: string;
  funcionarioNome: string;
  funcionarioUser: string;
  
  constructor(
    funcionarioId: number,
    funcionarioAtivo: boolean,
    funcionarioCargo: string,
    funcionarioDepartamentoId: number,
    funcionarioDepartamento: string,
    funcionarioSigla: string,
    funcionarioNome: string,
    funcionarioUser: string
  ) {
    this.funcionarioId = funcionarioId;
    this.funcionarioAtivo = funcionarioAtivo;
    this.funcionarioCargo = funcionarioCargo;
    this.funcionarioDepartamentoId = funcionarioDepartamentoId
    this.funcionarioDepartamento = funcionarioDepartamento;
    this.funcionarioSigla = funcionarioSigla;
    this.funcionarioNome = funcionarioNome;
    this.funcionarioUser = funcionarioUser;
  }
}
