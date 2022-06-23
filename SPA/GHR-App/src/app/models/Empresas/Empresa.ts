import { EmpresaConta } from "./EmpresaConta";

export class Empresa {
  id: number;
  nomeEmpresa: string;
  nomeFantasia: string;
  ativa: boolean;
  dataCadastro: Date;
  desativacao?: Date;
  logotipo: string;
  empresasContas: EmpresaConta;
}
