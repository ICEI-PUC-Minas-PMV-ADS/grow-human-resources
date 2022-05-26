import { DateTimeFormatPipe } from './../../helpers/DateTimeFormat.pipe';
export class DadoPessoal {
  id: number;
  cpf: string;
  tituloEleitor: string;
  identidade: string;
  dataExpedicaoIdentidade: Date;
  orgaoExpedicaoIdentidade: Date
  UfIdentidade: string;
  estadoCivil: string;
  carteiraTrabalho: string;
  dataExpedicaoCarteiraTrabalho: string;
}
