import { Component, OnInit, TemplateRef } from '@angular/core';
import {  FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { Meta } from 'src/app/models/Meta';

import { MetaService } from 'src/app/services/Meta.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';

@Component({
  selector: 'app-funcionario-meta',
  templateUrl: './funcionario-meta.component.html',
  styleUrls: ['./funcionario-meta.component.scss']
})
export class FuncionarioMetaComponent implements OnInit {
  public modalRef?: BsModalRef;
  public form!: FormGroup;

  public funcionario = {} as Funcionario;
  public metas: Meta[] = [];
  public meta = {} as Meta;
  public funcionarioMeta = {} as FuncionarioMeta;
  public funcionarioMetas = [] as FuncionarioMeta[];
  public estadoSalvar: string = "post";

  get f(): any
  {
    return this.form.controls;
  }

  get fMeta(): any
  {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private funcionarioService: FuncionarioService,
    private metaService: MetaService,
    private funcionarioMetaService: FuncionarioMetaService,
    private modalService: BsModalService) {
  }

  ngOnInit(): void
  {
    this.spinner.show();
    this.validarFormulario();
    this.consultarFuncionario();
    this.carregarComboMetas();
    this.consultarMetasPorFuncionario();
  }

  public validarFormulario(): void {
    this.form = this.fb.group(       {
      id: [''],
      nomeCompleto: [''],
      email: [''],
      telefone: [''],
      dataAdmissao: [''],
      dataDemissao: [''],
      deparamento: [''],
      nomeMeta: ['', Validators.required],
      inicioPlanejado: [null],
      metaCumprida: [false, Validators.required],
      fimPlanejado: [null],
      inicioRealizado: [null],
      fimRealizado: [null],
      supervisorId: [null]
    });
  }

  public consultarFuncionario(): void {

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');

    if (funcionarioIdParam !== null) {
      this.spinner.show();
      this.funcionarioService.getFuncionarioById(+funcionarioIdParam).subscribe(
        (funcionario: Funcionario) => {
          this.funcionario = funcionario;
          this.form.patchValue(this.funcionario);
        },
        (error: any) => {
          this.toastr.error("Não foi possível carregar a página de funcionário", "Erro!");
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    };

  }

  public carregarComboMetas(): void {

    this.spinner.show();

    this.metaService.getMetas().subscribe(
      (metas: Meta[]) => {
        this.metas = metas;
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Falha ao recuperar metas', "Erro!");
      }).add(() => this.spinner.hide());
  }

  public validarCampo(campoForm: FormControl): any {

    return ValidadorFormularios.verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {

    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public consultarMeta(): void {

    this.spinner.show();

    this.metaService.getMetaById(this.form.get('id').value).subscribe(
      (metaRetorno: Meta) => {
        this.meta = metaRetorno;
        this.form.patchValue(this.meta);
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Falha ao recuperar metas', "Erro!");
      }).add(() => this.spinner.hide());

  }

  public consultarMetasPorFuncionario(): void {

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');

    if (funcionarioIdParam !== null)
    {
      this.spinner.show();

      this.funcionarioMetaService.getMetasByFuncionarioId(+funcionarioIdParam).subscribe(
        (funcionarioMetas: FuncionarioMeta[]) => {
          this.funcionarioMetas = funcionarioMetas;
          console.log(this.funcionarioMetas)
        },
        (error: any) =>
        {
          this.toastr.error("Não foi possível carregar a página de metas por funcionário", "Erro!");
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public associarFuncionarioMeta(): void {
    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');
    const metaIdParam = this.form.get('id').value

    if (funcionarioIdParam !== null && metaIdParam !== null) {
      if (this.form.valid) {
        this.recuperarFuncionario(+funcionarioIdParam);

        if (this.funcionario !== null) {
          this.recuperarMeta(metaIdParam);

          if (this.meta !== null) {
            this.recuperarFuncionarioMeta();
            this.gravarFuncionarioMeta();
          }
        }
      }
    };
  }

  public recuperarFuncionario(id: number): void {

    this.spinner.show();

    this.funcionarioService.getFuncionarioById(id).subscribe(
      (funcioanrioRetorno: Funcionario) => {
        this.funcionario = { ...funcioanrioRetorno } ;
      },
      (error: any) => {
        console.error(error);
        this.toastr.error("Falha ao recuperar funcionários para associação a metas", "Erro!")
    }).add(() => this.spinner.hide());

    if (this.funcionario === null) {
      this.toastr.error("Funcionário não encontrado para associaçao com metas", "Erro!")
    };
  }

  public recuperarMeta(id: number): void {

    this.spinner.show();

    this.metaService.getMetaById(id).subscribe(
      (metaRetorno: Meta) => {
        this.meta = { ...metaRetorno };
      },
      (error: any) => {
        console.error(error);
        this.toastr.error("Falha ao recuperar Meta oara associação a funcionário.", "Erro!")
    }).add(() => this.spinner.hide);

    if (this.meta === null) {
      this.toastr.error("Meta não encontrada para associaçao a Funcionário", "Erro!")
    };
  }

  public recuperarFuncionarioMeta(): void {

    this.spinner.show();

    this.funcionarioMetaService.getMeta(this.funcionario.id, this.meta.id).subscribe(
      (funcionarioMetaRetorno: FuncionarioMeta) => {
        this.funcionarioMeta = funcionarioMetaRetorno;
        this.estadoSalvar = "put";
console.log(this.funcionarioMeta.metaId, "inout")
      },
      (error: any) => {
       console.error(error);
        this.toastr.error("Falha ao recuperar FuncionarioMeta", "Error")
      }
    ).add(() => this.spinner.hide());
        console.log(this.funcionarioMeta.metaId, "out")
  }

  public gravarFuncionarioMeta(): void {
    console.log("gravacao", this.funcionario, this.meta, this.estadoSalvar)
    this.spinner.show();

    this.funcionarioMeta.metaId = this.meta.id;
    this.funcionarioMeta.funcionarioId = this.funcionario.id;
    this.funcionarioMeta.metaCumprida = this.meta.metaCumprida;
    this.funcionarioMeta.inicioAcordado = this.meta.inicioPlanejado;
    this.funcionarioMeta.fimAcordado = this.meta.fimPlanejado;
    this.funcionarioMeta.inicioRealizado = this.meta.inicioRealizado;
    this.funcionarioMeta.fimRealizado = this.meta.fimRealizado;
    this.funcionarioMeta.supervisorId = 0;

    this.funcionarioMetaService['post'](this.funcionarioMeta).subscribe(
      (funcionarioMetaRetorno: FuncionarioMeta) => {
        this.toastr.success("Associação realizada com sucesso!", "Sucesso!");
        location.reload();
      },
      (error: any) => {
        console.error(error);
        this.toastr.info("Meta já associada ao funcionário", "Info!");
      }).add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, funcionarioId: number, metaId: number): void {
    event.stopPropagation();
    this.funcionarioMeta.funcionarioId = funcionarioId
    this.funcionarioMeta.metaId = metaId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmar(): void {
    this.modalRef?.hide();
    this.spinner.show();
    console.log("confirm", this.funcionarioMeta.funcionarioId, this.funcionarioMeta.metaId);
    this.funcionarioMetaService.deleteFuncionarioMeta(this.funcionarioMeta.funcionarioId, this.funcionarioMeta.metaId).subscribe(
      (retornoDelete: any) => {
        if (retornoDelete.message === "Excluído") {
          this.toastr.success("Funcionário excluído da base!", "Excluído!")
          this.spinner.hide();
          location.reload();
        };
      },
      (error: any) => {
        this.toastr.error(`Falha ao excluir funcionário/meta`, 'Erro!');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  recusar(): void {
    this.modalRef?.hide();
  }
}
