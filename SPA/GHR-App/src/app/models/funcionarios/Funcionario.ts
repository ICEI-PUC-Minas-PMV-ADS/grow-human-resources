import { Cargo } from "../cargos/Cargo";
import { Conta } from "../contas/Conta";
import { Departamento } from "../departamentos/Departamento";
import { Meta } from "../metas/Meta";
import { DadoPessoal } from "./DadoPessoal";
import { Endereco } from "./Endereco";

export class Funcionario {
  id: number;
  salario: number;
  dataAdmissao?: string;
  dataDemissao?: string;
  funcionarioAtivo: Boolean;
  cargoId: number;
  cargos: Cargo;
  departamentoId: number;
  departamentos: Departamento;
  contaId: number;
  contas: Conta;
  enderecoId: number;
  enderecos: Endereco;
  dadosPessoaisId: number;
  dadosPessoais: DadoPessoal;
  funcionarioMetas: Meta[];
}
