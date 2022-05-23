import { Cargo } from "../cargos/Cargo";
import { Conta } from "../contas/Conta";
import { Departamento } from "../departamentos/Departamento";
import { Meta } from "../Meta";
import { DadoPessoal } from "./DadoPessoal";
import { Endereco } from "./Endereco";

export class Funcionario {
  id: number;
  salario: number;
  dataAdmissao?: string;
  dataDemissao?: string;
  supervisorId: number;
  funcionarioAtivo: Boolean;
  gerenteAdministrativoId: number;
  gerenteOperacionaId: number;
  diretorId: number;
  cargoId: number;
  cargos: Cargo;
  departamentoId: number;
  departamentos: Departamento;
  userId: number;
  contas: Conta;
  enderecoId: number;
  enderecos: Endereco;
  dadosPessoaisId: number;
  dadosPessoais: DadoPessoal;
  imagemURL: string;
  funcionarioMetas: Meta[];
}
