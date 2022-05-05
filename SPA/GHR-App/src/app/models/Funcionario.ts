import { Metas } from "./Metas";

export interface Funcionario {
  id: number;
  nomeCompleto: string;
  email: string;
  telefone: string;
  salario: number;
  cargoId: number;
  dataAdmissao?: Date;
  dataDemissao?: Date;
  imagemURL: string;
  funcionarioAtivo: Boolean;
  departamentoId: number;
  supervisorId: number;
  loginId: number;
  metas: Metas[];
}
