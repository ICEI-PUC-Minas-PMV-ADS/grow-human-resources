import { Funcionario } from "./Funcionario";

export interface Meta {
  id: number;
  supervisorId: number;
  nomeMeta: string;
  descricao: string;
  metaCumprida: boolean;
  metaAprovada: boolean;
  inicioPlanejado?: string;
  fimPlanejado?: string;
  inicioRealizado?: string;
  fimRealizado?: string;
  funcionarios: Funcionario[];
}
