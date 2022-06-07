import { Meta } from "../metas/Meta";
import { Funcionario } from "./Funcionario";


export class FuncionarioMeta {
  id: number;
  metaId: number;
  metas: Meta;
  funcionarioId: number;
  funcionarios: Funcionario;
  metaCumprida: Boolean;
  inicioAcordado?: string;
  fimAcordado?: string;
  inicioRealizado: string;
  fimRealizado: string;
  supervisor: string;
}
