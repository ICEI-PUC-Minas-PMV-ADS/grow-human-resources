import { Departamento } from "../departamentos/Departamento";

export interface Cargo {
  id: number;
  nomeCargo: string;
  funcao: string;
  recursosHumanos: boolean;
  departamentoId: number;
  departamentos: Departamento;
}
