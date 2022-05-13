import { Departamento } from "./Departamento";
import { Meta } from "./Meta";

export interface Funcionario {
  id: number;
  nomeCompleto: string;
  email: string;
  telefone: string;
  salario: number;
  cargoId: number;
  dataAdmissao?: string;
  dataDemissao?: string;
  imagemURL: string;
  funcionarioAtivo: Boolean;
  departamentoId: number;
  departamento: Departamento;
  supervisorId: number;
  loginId: number;
  metas: Meta[];
}
