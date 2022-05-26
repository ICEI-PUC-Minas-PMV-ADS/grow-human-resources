import { Funcionario } from "../funcionarios/Funcionario";

export class Meta {
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
