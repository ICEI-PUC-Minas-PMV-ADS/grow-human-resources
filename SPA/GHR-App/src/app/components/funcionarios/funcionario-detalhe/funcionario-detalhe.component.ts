import { identifierModuleUrl } from "@angular/compiler";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router";
import { BsLocaleService } from "ngx-bootstrap/datepicker";

import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from "ngx-toastr";

import { ValidadorFormularios } from "src/app/helpers/ValidadorFormularios";

import { Cargo } from "src/app/models/cargos/Cargo";
import { ContaAtiva } from "src/app/models/contas/ContaAtiva";
import { ContaVisao } from "src/app/models/contas/ContaVisao";
import { Departamento } from "src/app/models/departamentos/Departamento";
import { DadoPessoal } from "src/app/models/funcionarios/DadoPessoal";
import { Endereco } from "src/app/models/funcionarios/Endereco";
import { Funcionario } from "src/app/models/funcionarios/Funcionario";
import { ResultadoPaginacao } from "src/app/models/paginacao/paginacao";


import { CargoService } from "src/app/services/cargos/Cargo.service";
import { ContaService } from "src/app/services/contas/Conta.service";
import { DepartamentoService } from "src/app/services/departamentos/departamento.service";
import { DadosPessoaisService } from "src/app/services/funcionarios/dadosPessoais.service";
import { EnderecoService } from "src/app/services/funcionarios/endereco.service";
import { FuncionarioService } from "src/app/services/funcionarios/funcionario.service";


@Component({
  selector: 'app-funcionario-detalhe',
  templateUrl: './funcionario-detalhe.component.html',
  styleUrls: ['./funcionario-detalhe.component.scss']
})
export class FuncionarioDetalheComponent implements OnInit {

  contaLogada = false;
  contaAtiva = {} as ContaAtiva;
  visaoRH = false;

  public funcionario = {} as Funcionario;
  public contaPesquisa = {} as ContaVisao;
  public dadosPessoais = {} as DadoPessoal;
  public enderecos = {} as Endereco;
  public cargos: Cargo[] = [];
  public departamentos: Departamento[] = [];

  public locale = 'pt-br';

  public formConta: FormGroup;
  public formFuncionario: FormGroup;

  public ativarConsultaConta = "d-none"; /* colocar */
  public ativarFuncionario = "d-none"; /* colocar d-none*/

  public protegerCampoConta = false;
  public protegerCampoFuncionario = false;
  public modoEditar = false;

  public funcionarioId: number;


  get ctrConta(): any {
    return this.formConta.controls;
  }

  get ctrF(): any {
    return this.formFuncionario.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY h:mm a',
      containerClass: 'theme-default',
      standalone: true
    };
  }

  constructor(
    private cargoService: CargoService,
    private contaService: ContaService,
    private dadosPessoaisService: DadosPessoaisService,
    private departamentoService: DepartamentoService,
    private enderecoService: EnderecoService,
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService,
    private localeService: BsLocaleService,
    private router: Router,
    private routerActivated: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
    ) {
      router.events.subscribe(
        (val) => {
          if (val instanceof NavigationEnd) {
            this.contaService.contaAtual$.subscribe(
              (value) => {
                this.contaLogada = value !== null;
                this.contaAtiva = { ...value } ;
                this.visaoRH = this.contaAtiva.visao?.includes('RH');
                console.log(this.contaLogada, this.contaAtiva, this.contaAtiva.visao);
                console.log('Menu', this.contaAtiva.visao, this.visaoRH);
              }
              )
            }
          }
          )
        }

  ngOnInit(): void {
    console.log("visao", this.visaoRH);

    this.protegerCampoConta = (this.visaoRH) ? false : true;
    this.protegerCampoFuncionario = (this.visaoRH) ? false : true;
    this.ativarConsultaConta = (this.visaoRH) ? "d-none" : "d-block"

    this.validarFormularios();
    this.carregarCargos();
    this.carregarDepartametos();
    this.carregarFuncionario();
  }

  public validarFormularios(): void {

    this.formConta = this.fb.group({
      contaPesquisa: [''],
      userName: [''],
      nomeCompleto: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50),
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      phoneNumber: ['', Validators.required],
      visao: ['Funcionário', Validators.required],
    });

    this.formFuncionario = this.fb.group({
      cpf: ['', Validators.required],
      tituloEleitor: ['', Validators.required],
      identidade: ['', Validators.required],
      dataExpedicaoIdentidade: ['', Validators.required],
      orgaoExpedicaoIdentidade: ['', Validators.required],
      ufIdentidade: ['', Validators.required],
      estadoCivil: ['', Validators.required],
      carteiraTrabalho: ['', Validators.required],
      dataExpedicaoCarteiraTrabalho: ['', Validators.required],
      cep: ['', Validators.required],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: ['', Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      uf: ['', Validators.required],
      pais: ['Brasil', Validators.required],
      caixaPostal: ['', Validators.required],
      complementoEndereco: ['', Validators.required],
      cargoId: ['', Validators.required],
      salario: ['', Validators.required],
      dataAdmissao: ['', Validators.required],
      dataDemissao: [null],
      departamentoId: ['', Validators.required],
      supervisorId: [0],
      gerenteAdministrativoId: [0],
      gerenteOperacionaId: [0],
      diretorId: [0],
      contaId: [0],
    });
  }

  public carregarCargos(): void  {
    this.spinner.show();

    this.cargoService
      .recuperarCargos()
      .subscribe(
        (cargoRetorno: ResultadoPaginacao<Cargo[]>) => {
          this.cargos = cargoRetorno.resultado;},
        (error: any) => {
          this.toastr.error("Não foi possível carregar os cargos", "Erro!");
          console.error(error);})
      .add(() => this.spinner.hide());
    }

  public carregarDepartametos(): void {
    this.spinner.show();

    this.departamentoService
      .recuperarDepartamentos()
      .subscribe(
        (departamentoRetorno: ResultadoPaginacao<Departamento[]>) => {
          this.departamentos = departamentoRetorno.resultado;},
        (error: any) => {
          this.toastr.error("Não foi possível carregar os departamentos", "Erro!");
          console.error(error);})
      .add(() => this.spinner.hide());
    }

  public carregarFuncionario(): void {

    this.funcionarioId  = +this.routerActivated.snapshot.paramMap.get('id');

    if (this.funcionarioId !== null && this.funcionarioId !== 0) {
      this.spinner.show();

      this.modoEditar = true;

      this.funcionarioService
        .recuperarFuncionarioPorId(this.funcionarioId)
        .subscribe(
          (funcionario: Funcionario) => {
            this.funcionario = { ...funcionario };
            this.formConta.patchValue(this.funcionario.contas);
            this.formFuncionario.patchValue(this.funcionario);
            this.formFuncionario.patchValue(this.funcionario.enderecos);
            this.formFuncionario.patchValue(this.funcionario.dadosPessoais);
            this.contaPesquisa = { ...funcionario.contas }
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
            console.error(error);
          }
          )
          .add(() => this.spinner.hide())
      };
    }

  public consultarConta(): void {

    this.spinner.show();

    if (!this.formConta.get('contaPesquisa').value) {
      this.toastr.info("Conta para pesquisa não iformada.", "Informação!");
      this.spinner.hide();

    } else {

      this.contaService.recuperarContaPorUserName(this.formConta.get('contaPesquisa').value).subscribe(
        (contaPesquisada: ContaVisao) => {

          this.ativarConsultaConta = "d-none";

          if (contaPesquisada == null) {
            this.toastr.info("Conta não encontrada.", "Informação!");
          } else {
            this.ativarConsultaConta = 'd-block'
            this.contaPesquisa = contaPesquisada ;
            this.formConta.patchValue(this.contaPesquisa);
          };
        },
        (error) => {
          console.error(error);
          this.toastr.error("Falha na carga de usuário", "Erro!");
        }
      ).add(() => this.spinner.hide());
    }
  }

  public salvarConta(): void {

    this.spinner.show();

    if (this.formConta.valid) {

      this.contaPesquisa = { id: this.contaPesquisa.id, ...this.formConta.value };

      this.contaService.atualizarConta(this.contaPesquisa).subscribe(
        (contaRetorno: ContaVisao) => {
          this.contaPesquisa = contaRetorno;
          this.ativarFuncionario = 'd-block';
          this.protegerCampoConta = true;
          this.consultarFuncionarioDaConta();
        },
        (error: any) => {
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
          console.error(error);
        }).add(() => this.spinner.hide());
    };
  }

  public consultarFuncionarioDaConta(): void {

    this.funcionarioService
    .recuperarFuncionarioPorContaId(this.contaPesquisa.id)
    .subscribe(
      (funcionario: Funcionario) => {
        this.funcionario = { ...funcionario };
        this.formFuncionario.patchValue(this.funcionario);
        this.formFuncionario.patchValue(this.funcionario.enderecos);
        this.formFuncionario.patchValue(this.funcionario.dadosPessoais);
      },
      (error: any) => {
        this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
        console.error(error);
      })
    .add(() => this.spinner.hide());

    console.log("modo editar:", this.modoEditar, this.funcionario.id);
  }

  public salvarFuncionario(): void {
    console.log("mode", this.modoEditar);

    if (!this.modoEditar) {

      this.criarDadosPessoais();
    } else {
      this.alterarDadosPessoais();
    }
  }

  public criarDadosPessoais(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {

      this.dadosPessoais = { ...this.formFuncionario.value };
      console.log("from form DP", this.dadosPessoais)

      this.dadosPessoaisService
      .criarDadoPessoal(this.dadosPessoais)
      .subscribe(
        (dadosPessoaisRetorno: DadoPessoal) => {
          this.dadosPessoais = { ... dadosPessoaisRetorno}
          console.log("DP", this.dadosPessoais);
          this.criarEndereco();
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar Dados Pessoais.", "Erro!");
        })
      .add(() => this.spinner.hide())
    }
  }

  public criarEndereco(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {

      this.enderecos = { ...this.formFuncionario.value };
      console.log("from form END", this.enderecos)

      this.enderecoService
      .criarEndereco(this.enderecos)
      .subscribe(
        (enderecoRetrono: Endereco) => {
          this.enderecos = { ...enderecoRetrono }
          console.log("End", this.enderecos);
          this.criarFuncionario();
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar Endereço.", "Erro!");
        })
      .add(() => this.spinner.hide())
    }
  }

  public criarFuncionario(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {

      this.funcionario = {
        dadosPessoaisId: this.dadosPessoais.id,
        enderecoId: this.enderecos.id,
        funcionarioAtivo: true,
        ...this.formFuncionario.value
      };
      this.funcionario.contaId = this.contaPesquisa.id;
      console.log("from form Func", this.funcionario, this.contaPesquisa)


      this.funcionarioService
        .criarFuncionario(this.funcionario)
        .subscribe(
          (funcionarioRetorno: Funcionario) => {
            this.funcionario = { ...funcionarioRetorno };
            this.toastr.success("Funcionario cadastrado!", "Sucesso!")
            this.router.navigate([`funcionarios/detalhe/${this.funcionario.id}`]);
          },
          (error: any) => {
            console.error(error);
            this.toastr.error("Falha ao atualizar Funcionario.", "Erro!");
          })
        .add(() => this.spinner.hide())
     }
   }

public alterarDadosPessoais(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {

      this.dadosPessoais = { id: this.funcionario.dadosPessoais.id, ...this.formFuncionario.value };
      console.log("from form DP", this.dadosPessoais)

      this.dadosPessoaisService
      .salvarDadoPessoal(this.dadosPessoais)
      .subscribe(
        (dadosPessoaisRetorno: DadoPessoal) => {
          this.dadosPessoais = { ... dadosPessoaisRetorno}
          this.alterarEndereco();
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar Dados Pessoais.", "Erro!");
        })
      .add(() => this.spinner.hide())
    }
  }

  public alterarEndereco(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {

      this.enderecos = { id: this.funcionario.enderecos.id, ...this.formFuncionario.value };
      console.log("from form END", this.enderecos)

      this.enderecoService
      .salvarEndereco(this.enderecos)
      .subscribe(
        (enderecoRetrono: Endereco) => {
          this.enderecos = { ...enderecoRetrono }
          this.alterarFuncionario();
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar Endereço.", "Erro!");
        })
      .add(() => this.spinner.hide())
    }
  }

  public alterarFuncionario(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {
      this.funcionario = {
        id: this.funcionario.id,
        dadosPessoaisId: this.funcionario.dadosPessoais.id,
        departamentoId: this.funcionario.departamentos.id,
        enderecoId: this.funcionario.enderecos.id,
        funcionarioAtivo: this.funcionario.funcionarioAtivo,
        ...this.formFuncionario.value
      };
      console.log("from form Func", this.funcionario)

      this.funcionarioService
        .salvarFuncionario(this.funcionario)
        .subscribe(
          (funcionarioRetorno: Funcionario) => {
            this.funcionario = { ...funcionarioRetorno };
            this.toastr.success("Funcionario cadastrado!", "Sucesso!")
          },
          (error: any) => {
            console.error(error);
            this.toastr.error("Falha ao atualizar Funcionario.", "Erro!");
          })
        .add(() => this.spinner.hide())
     }
   }




/***********************************************************/
  public limparFormulario(): void {
    this.formFuncionario.reset();
  }

  public validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios.verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }
}



