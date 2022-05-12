import { Funcionario } from './Funcionario';
import { Meta } from "./Meta";

export interface FuncionarioMeta {
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
