import { Departamento } from "../departamentos/Departamento";

export class Cargo {
  id: number;
  nomeCargo: string;
  funcao: string;
  recursosHumanos: boolean;
  departamentoId: number;
  departamentos: Departamento;
}
