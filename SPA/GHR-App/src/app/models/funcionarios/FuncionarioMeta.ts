import { Meta } from "../metas/Meta";
import { Funcionario } from "./Funcionario";


export class FuncionarioMeta {
  metaId: number;
  meta: Meta;
  funcionarioId: number;
  funcionario: Funcionario;
  metaCumprida: boolean;
  inicioAcordado?: string;
  fimAcordado?: string;
  inicioRealizado: string;
  fimRealizado: string;
  supervisorId: number;
}
