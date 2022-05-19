import { Meta } from "@angular/platform-browser";
import { Funcionario } from "./Funcionario";


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
