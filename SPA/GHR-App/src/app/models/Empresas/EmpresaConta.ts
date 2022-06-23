import { Conta } from "../contas/Conta";
import { Empresa } from "./Empresa";

export class EmpresaConta {
  id: number;
  userName: string;
  contas: Conta;
  empresaId: number;
  empresas: Empresa;
}
