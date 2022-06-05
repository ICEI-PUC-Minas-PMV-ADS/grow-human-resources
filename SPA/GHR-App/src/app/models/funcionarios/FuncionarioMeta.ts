import { Meta } from "../metas/Meta";
import { Funcionario } from "./Funcionario";


export class FuncionarioMeta {
  id: number;
  metaId: number;
  meta: Meta;
  funcionarioId: number;
  funcionario: Funcionario;
  metaCumprida: Boolean;
  inicioAcordado?: string;
  fimAcordado?: string;
  inicioRealizadb: string;
  fimRealizado: string;
  supervisor: string;
}
