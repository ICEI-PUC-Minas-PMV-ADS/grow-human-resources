export interface Login {
  id: number;
  userName: string;
  senha: string;
  dataCadastro?: Date;
  funcionarioId?: number;
  sobreMim: string;
}
