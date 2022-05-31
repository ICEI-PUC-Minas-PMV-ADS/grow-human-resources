import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';
import { ContaService } from 'src/app/services/contas/Conta.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { Meta } from 'src/app/models/metas/Meta';
import { NavegacaoEntreForms } from 'src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';
import { MetaService } from 'src/app/services/metas/Meta.service';
import { Conta } from 'src/app/models/contas/Conta';

@Component({
  selector: 'app-funcionario-meta-associar',
  templateUrl: './funcionario-meta-associar.component.html',
  styleUrls: ['./funcionario-meta-associar.component.scss']
})
export class FuncionarioMetaAssociarComponent implements OnInit {

  public form: FormGroup;

  public conta = {} as Conta
  public contaAtiva = {} as ContaAtiva;
  public funcionario = {} as Funcionario;
  public funcionarioMeta = {} as FuncionarioMeta;
  public metas: Meta[] = [];
  public meta = {} as Meta;

  public visaoRH = false;

  public prosseguir: boolean ;

  public get mostrarAbas(): boolean {
    return (this.visaoRH);
  }

  public estadoSalvar: string = "post";

  get ctrMeta(): any
  {
    return this.form.controls;
  }

  constructor(
    private activatedRouter: ActivatedRoute,
    private contaService: ContaService,
    private funcionarioMetaService: FuncionarioMetaService,
    private funcionarioService: FuncionarioService,
    private fb: FormBuilder,
    private metaService: MetaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router)
    {
      router.events.subscribe(
        (verificaContaAtiva) => {
          if (verificaContaAtiva instanceof NavigationEnd) {
            this.contaService
              .contaAtual$
              .subscribe(
                (value) => {
                  this.contaAtiva = { ...value };
                  this.visaoRH = this.contaAtiva.visao?.includes('RH');})
      }})
  }

  ngOnInit(): void
  {
    this.spinner.show();
    this.validarFormulario();
    this.consultarFuncionario();
    this.carregarComboMetas();
  }

  public validarFormulario(): void {
    this.form = this.fb.group(       {
      id: [''],
      nomeMeta: ['', Validators.required],
      metaCumprida: [false, Validators.required],
      metaAprovada: [false, Validators.required],
      inicioPlanejado: [null],
      fimPlanejado: [null],
      inicioRealizado: [null],
      fimRealizado: [null],
      supervisor: [null]
    });
  }

  public consultarFuncionario(): void {

    this.spinner.show();

    this.contaService
      .recuperarContaPorUserName(this.contaAtiva.userName)
      .subscribe(
        (conta: Conta) => {

          this.conta = conta;

          console.log("Conta", this.conta);
          if (this.conta !== null) {
            this.spinner.show();

            this.funcionarioService
              .recuperarFuncionarioPorContaId(this.conta.id)
              .subscribe(
                (funcionario: Funcionario) => {
                  console.log("func1", funcionario)
                  this.funcionario = funcionario;
                  this.form.get("supervisor").setValue(this.funcionario.contas.nomeCompleto);
                },
                (error: any) => {
                  this.toastr.error("Não foi possível carregar o funcionario logado", "Erro!");
                  console.error(error);
                })
              .add(() => this.spinner.hide());
          };
        },

        (error: any) => this.toastr.error("Falha ao recuperar a Conta Ativa.", "Erro!"))

        .add(() => this.spinner.hide());


  }

  public carregarComboMetas(): void {

    this.spinner.show();

    this.metaService
      .recuperarMetasAtivas()
      .subscribe(
        (metas: Meta[]) => { this.metas = metas; },

        (error: any) => {

          console.error(error);
          this.toastr.error('Falha ao recuperar metas', "Erro!");})

      .add(() => this.spinner.hide());
  }

  public validarCampo(campoForm: FormControl): any {

    return ValidadorFormularios.verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {

    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public consultarMeta(): void {

    this.spinner.show();

    this.metaService
      .recuperarMetaPorId(this.form.get('id').value)
      .subscribe(
        (metaRetorno: Meta) => {
          this.meta = metaRetorno;
          this.form.patchValue(this.meta);},
        (error: any) => {
          console.error(error);
          this.toastr.error('Falha ao recuperar metas', "Erro!");})
      .add(() => this.spinner.hide());
  }

  public associarFuncionarioMeta(): void {
    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');
    const metaIdParam = this.form.get('id').value

    if (funcionarioIdParam !== null && metaIdParam !== null) {
      if (this.form.valid) {
        if (this.funcionario !== null) {
          if (this.meta !== null) {
            this.gravarFuncionarioMeta(+funcionarioIdParam, metaIdParam);
          }
        }
      }
    };
  }


  public gravarFuncionarioMeta(funcionarioId: number, metaId: number): void {
    console.log("gravacao", this.funcionario, this.meta, this.estadoSalvar)
    this.spinner.show();

    this.funcionarioMeta.metaId = metaId;
    this.funcionarioMeta.funcionarioId = funcionarioId;
    this.funcionarioMeta.metaCumprida = this.meta.metaCumprida;
    this.funcionarioMeta.inicioAcordado = this.meta.inicioPlanejado;
    this.funcionarioMeta.fimAcordado = this.meta.fimPlanejado;
    this.funcionarioMeta.inicioRealizado = this.meta.inicioRealizado;
    this.funcionarioMeta.fimRealizado = this.meta.fimRealizado;
    this.funcionarioMeta.supervisorId = 0;

    this.funcionarioMetaService
      .criarFuncionarioMeta(this.funcionarioMeta)
      .subscribe(
        (funcionarioMetaRetorno: FuncionarioMeta) => {
          this.toastr.success("Associação realizada com sucesso!", "Sucesso!");
          location.reload();
        },
        (error: any) => {
          console.info(error);
          this.toastr.info("Meta já associada ao funcionário", "Info!");
        }).add(() => this.spinner.hide());
  }

}
