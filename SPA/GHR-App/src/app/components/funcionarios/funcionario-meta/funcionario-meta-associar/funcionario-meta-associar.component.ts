import { Component, OnInit} from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Conta } from 'src/app/models/contas/Conta';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';

import { Meta } from 'src/app/models/metas/Meta';
import { ContaService } from 'src/app/services/contas/Conta.service';

import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';

import { MetaService } from 'src/app/services/metas/Meta.service';

@Component({
  selector: 'app-funcionario-meta-associar',
  templateUrl: './funcionario-meta-associar.component.html',
  styleUrls: ['./funcionario-meta-associar.component.scss']
})
export class FuncionarioMetaAssociarComponent implements OnInit {

  public form: FormGroup;

  public funcionarioMeta = {} as FuncionarioMeta;
  public metas: Meta[] = [];
  public meta = {} as Meta;
  public contaAtiva = {} as Conta;

  public visaoRH = false;

  get ctrMeta(): any
  {
    return this.form.controls;
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
    private activatedRouter: ActivatedRoute,
    private contaService: ContaService,
    private funcionarioMetaService: FuncionarioMetaService,
    private fb: FormBuilder,
    private metaService: MetaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router)
    {  }

  ngOnInit(): void
  {
    this.spinner.show();
    this.validarFormulario();
    this.carregarContaAtiva();
    this.carregarComboMetas();
  }

  public validarFormulario(): void {
    this.form = this.fb.group(       {
      nomeMeta: ['', Validators.required],
      metaCumprida: [false, Validators.required],
      metaAprovada: [false, Validators.required],
      inicioPlanejado: [null],
      fimPlanejado: [null],
      inicioRealizado: [null],
      fimRealizado: [null],
      supervisor: [null],
      funiconarioId: [],
      id: [],
    });
  }

  public carregarContaAtiva(): void {
    this.spinner.show();

    this.contaService.recuperarContaAtiva()
      .subscribe(
        (contaAtiva: Conta) => {
          this.contaAtiva = contaAtiva;
          this.visaoRH = contaAtiva.visao.includes("RH");
        },
        (error: any) => {
          this.toastr.error("Falha ao carregar conta ativa.", "Erro!");
          console.error(error);
        }
      )
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

    return ValidadorFormularios
      .verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {

    return ValidadorFormularios
      .retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public consultarMeta(): void {

    this.spinner.show();
    const metaId = this.form.get('id').value

    if (metaId === null) {
      this.spinner.hide();
      this.toastr.info("Selecione uma meta para prosseguir", "Informação!")
    } else {
      this.metaService
        .recuperarMetaPorId(this.form.get('id').value)
        .subscribe(
          (metaRetorno: Meta) => {
            this.meta = metaRetorno;
            this.form.patchValue(this.meta);
            this.funcionarioMetaService

          },
          (error: any) => {
            console.error(error);
            this.toastr.error('Falha ao recuperar metas', "Erro!");})
        .add(() => this.spinner.hide());
    }
  }

  public associarFuncionarioMeta(): void {
    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');
    const metaIdParam = this.form.get('id').value;

    if (funcionarioIdParam !== null && metaIdParam !== null) {
      if (this.form.valid) {
        this.consultarFuncionarioMeta(+funcionarioIdParam, +metaIdParam);
      }
    };
  }

  public consultarFuncionarioMeta(funcionarioId: number, metaId: number): void {
    this.spinner.show();

    this.funcionarioMetaService
      .recuperarFuncionarioIdMetaId(funcionarioId, metaId)
      .subscribe(
        (funcionarioMeta: FuncionarioMeta) => {
          if (funcionarioMeta !== null) {
              this.salvarFuncionarioMeta(funcionarioMeta.funcionarioId, funcionarioMeta.metaId);
          } else {
              this.criarFuncionarioMeta(funcionarioId, metaId);
          }
        },
        (error: any) => {
          this.toastr.error("Falha ao recuperar metas por Funcionario.", "Erro!");
          console.error(error);
        })
      .add(() => this.spinner.hide());
  }

  public criarFuncionarioMeta(funcionarioId: number, metaId: number): void {
    this.spinner.show();


    this.funcionarioMeta.funcionarioId = funcionarioId;
    this.funcionarioMeta.metaId = metaId;
    this.funcionarioMeta.metaCumprida = this.form.get('metaCumprida').value
    this.funcionarioMeta.inicioAcordado = this.meta.inicioPlanejado;
    this.funcionarioMeta.fimAcordado = this.meta.fimPlanejado;

    this.funcionarioMetaService
      .criarFuncionarioMeta(this.funcionarioMeta)
      .subscribe(
        () => {
          this.toastr.success("Associação realizada com sucesso!", "Sucesso!");
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao cadastrar metas para um funcionario", "Erro!");
        })
      .add(() => this.spinner.hide());
  }

  public salvarFuncionarioMeta(funcionarioId: number, metaId: number): void {
    this.spinner.show();

    this.funcionarioMeta.funcionarioId = funcionarioId;
    this.funcionarioMeta.metaId = metaId;
    this.funcionarioMeta.metaCumprida = this.form.get('metaCumprida').value
    this.funcionarioMeta.inicioRealizado = this.form.get('inicioPlanejado').value
    this.funcionarioMeta.fimRealizado = this.form.get('fimPlanejado').value
    console.log('func', this.funcionarioMeta)
    this.funcionarioMetaService
      .salvarFuncionarioMeta(this.funcionarioMeta)
      .subscribe(
        () => {
          this.toastr.success("Funiconario/Meta atualizado!", "Sucesso!");
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Falha ao atualizar meta de um funcionário", "Erro!");
        })
      .add(() => this.spinner.hide());
  }



}
