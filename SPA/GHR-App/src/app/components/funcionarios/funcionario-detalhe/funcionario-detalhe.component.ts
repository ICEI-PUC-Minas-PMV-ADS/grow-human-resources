import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { BsLocaleService } from "ngx-bootstrap/datepicker";

import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from "ngx-toastr";

import { ValidadorFormularios } from "src/app/helpers/ValidadorFormularios";

import { Cargo } from "src/app/models/cargos/Cargo";
import { ContaVisao } from "src/app/models/contas/ContaVisao";
import { Departamento } from "src/app/models/departamentos/Departamento";
import { DadoPessoal } from "src/app/models/funcionarios/DadoPessoal";
import { Endereco } from "src/app/models/funcionarios/Endereco";
import { Funcionario } from "src/app/models/funcionarios/Funcionario";

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

  public locale = 'pt-br';

  public formConta: FormGroup;
  public formFuncionario: FormGroup;

  public ativarConsultaConta = "d-none" /* colocar */
  public ativarFuncionario = "d-none" /* colocar d-none*/

  public protegerCampoConta = false;
  public modoEditar = false;

  public contaPesquisa = {} as ContaVisao;
  public funcionario = {} as Funcionario;
  public dadosPessoais = {} as DadoPessoal;
  public enderecos = {} as Endereco;
  public cargos: Cargo[] = [];
  public departamentos: Departamento[] = [];

  public funcionarioId: number;
  public userId: number;

  get ctrConta(): any {
    return this.formConta.controls;
  }

  get ctrF(): any {
    return this.formFuncionario.controls;
  }

  get bsConfig(): any {
    return {isAnimated: true, adaptivePosition: true, dateInputFormat: 'DD/MM/YYYY h:mm a', containerClass: 'theme-default'};
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
  ) { }

  ngOnInit(): void {

    this.validarFormularios();
    this.consultarCargos();
    this.consultarDepartametos();
    this.carregarFuncionario();
    console.log("Carga func2", this.funcionario)

    if (this.modoEditar) {
      this.carregarConta();
    }
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
      CPF: ['', Validators.required],
      tituloEleitor: ['', Validators.required],
      identidade: ['', Validators.required],
      dataExpedicaoIdentidade: ['', Validators.required],
      orgaoExpedicaoIdentidade: ['', Validators.required],
      UfIdentidade: ['', Validators.required],
      estadoCivil: ['', Validators.required],
      carteiraTrabalho: ['', Validators.required],
      dataExpedicaoCarteiraTrabalho: ['', Validators.required],
      CEP: ['', Validators.required],
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
      userId: [0],
    });
  }

  public limparFormulario(): void {
    this.formFuncionario.reset();
  }

  public validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios.verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public consultarDepartametos(): void {
    this.spinner.show();

    this.departamentoService.recuperarDepartamentos().subscribe(
      (departamentoRetorno: Departamento[]) => {
        this.departamentos = departamentoRetorno;
      },
      (error: any) => {
        this.toastr.error("Não foi possível carregar os departamentos", "Erro!");
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  public consultarCargos(): void  {
    this.spinner.show();

    this.cargoService.recuperarCargos().subscribe(
      (cargoRetorno: Cargo[]) => {
        this.cargos = cargoRetorno;
      },
      (error: any) =>
      {
        this.toastr.error("Não foi possível carregar os cargos", "Erro!");
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  public consultarConta(): void {

    this.spinner.show();

    if (!this.formConta.get('contaPesquisa').value) {
      this.toastr.info("Conta para pesquisa não iformada.", "Informação!");
      this.spinner.hide();

    } else {

      this.contaService.recuperarContaPorUserName(this.formConta.get('contaPesquisa').value).subscribe(
        (contaPesquisada: ContaVisao) => {
          console.log("conta Pesquisada:", contaPesquisada);

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
        () => {
          this.toastr.success("Alterações salvas com sucesso!", "Salvo!")
          this.ativarFuncionario = 'd-block';
          this.protegerCampoConta = true;
        },
        (error: any) => {
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
          console.error(error);
        }).add(() => this.spinner.hide());
    };
  }

  public salvarFuncionario(): void {
    console.log("mode", this.modoEditar);

    if (!this.modoEditar) {

      this.criarDadosPessoais();
    }
  }

  public criarDadosPessoais(): void {

    this.spinner.show();

    if (this.formFuncionario.valid) {

      this.dadosPessoais.CPF = this.formFuncionario.get("CPF").value
      this.dadosPessoais.tituloEleitor = this.formFuncionario.get("tituloEleitor").value
      this.dadosPessoais.identidade = this.formFuncionario.get("identidade").value
      this.dadosPessoais.dataExpedicaoIdentidade = this.formFuncionario.get("dataExpedicaoIdentidade").value
      this.dadosPessoais.orgaoExpedicaoIdentidade = this.formFuncionario.get("orgaoExpedicaoIdentidade").value
      this.dadosPessoais.UfIdentidade = this.formFuncionario.get("UfIdentidade").value
      this.dadosPessoais.estadoCivil = this.formFuncionario.get("estadoCivil").value
      this.dadosPessoais.carteiraTrabalho = this.formFuncionario.get("carteiraTrabalho").value
      this.dadosPessoais.dataExpedicaoCarteiraTrabalho = this.formFuncionario.get("dataExpedicaoCarteiraTrabalho").value

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

      this.enderecos.CEP = this.formFuncionario.get("CEP").value
      this.enderecos.bairro = this.formFuncionario.get("bairro").value
      this.enderecos.caixaPostal = this.formFuncionario.get("caixaPostal").value
      this.enderecos.cidade = this.formFuncionario.get("cidade").value
      this.enderecos.complemento = this.formFuncionario.get("complemento").value
      this.enderecos.complementoEndereco = this.formFuncionario.get("complementoEndereco").value
      this.enderecos.numero = this.formFuncionario.get("numero").value
      this.enderecos.pais = this.formFuncionario.get("pais").value
      this.enderecos.logradouro = this.formFuncionario.get("logradouro").value
      this.enderecos.uf = this.formFuncionario.get("uf").value

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

      this.funcionario.salario = this.formFuncionario.get("salario").value;
      this.funcionario.dataAdmissao = this.formFuncionario.get("dataAdmissao").value;
      this.funcionario.dataDemissao = this.formFuncionario.get("dataDemissao").value;
      this.funcionario.supervisorId = Number.parseInt(this.formFuncionario.get("supervisorId").value);
      this.funcionario.funcionarioAtivo = true;
      this.funcionario.gerenteAdministrativoId = Number.parseInt(this.formFuncionario.get("gerenteAdministrativoId").value);
      this.funcionario.gerenteOperacionaId = Number.parseInt(this.formFuncionario.get("gerenteOperacionaId").value);
      this.funcionario.diretorId = Number.parseInt(this.formFuncionario.get("diretorId").value);
      this.funcionario.cargoId = Number.parseInt(this.formFuncionario.get("cargoId").value);
      this.funcionario.departamentoId = Number.parseInt(this.formFuncionario.get("departamentoId").value);
      this.funcionario.userId = this.contaPesquisa.id;
      this.funcionario.enderecoId = this.enderecos.id;
      this.funcionario.dadosPessoaisId = this.dadosPessoais.id;
       console.log("fun", this.funcionario);

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
            this.formFuncionario.patchValue(this.funcionario);
            console.log("func1", this.funcionario);
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de funcionário.", "Errro!");
            console.error(error);
          }
        ).add(() => this.spinner.hide());
    };

    console.log("modo editar:", this.userId);
  }

    public carregarConta(): void {

    this.spinner.show();

    console.log("Carregar", this.funcionario.userId);

    this.contaService
      .recuperarContaPorId(this.funcionario.userId).subscribe(
      (contaCarregada: ContaVisao) => {
        console.log("conta carregada:", contaCarregada);
        this.contaPesquisa = contaCarregada ;
        this.formConta.patchValue(this.contaPesquisa);
        this.toastr.success("Conta do funiconario Carregada", "Sucesso!");
      },
      (error) => {
        console.error(error);
        this.toastr.error("Falha na carga da conta do funcionário", "Erro!");
      }
    ).add(() => this.spinner.hide());
  }
}


