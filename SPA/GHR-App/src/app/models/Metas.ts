import { Funcionario } from "./Funcionario";

export interface Metas {
  id: number;
  supervisorId: number;
  nomeMeta: string;
  descricao: string;
  metaCumprida: boolean;
  metaAprovada: boolean;
  inicioPlanejado?: Date;
  fimPlanejado?: Date;
  inicioRealizado?: Date;
  fimRealizado?: Date;
  funcionarios: Funcionario[];
}
