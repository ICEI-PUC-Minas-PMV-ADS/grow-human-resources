import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { Meta } from 'src/app/models/metas/Meta';
import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';

@Component({
  selector: 'app-funcionario-detalhe-meta',
  templateUrl: './funcionario-detalhe-meta.component.html',
  styleUrls: ['./funcionario-detalhe-meta.component.scss']
})
export class FuncionarioDetalheMetaComponent implements OnInit {

  public form: FormGroup;

  public funcionarioMeta = {} as FuncionarioMeta;

  public metaIniciada = false;

  get ctrMeta(): any {
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
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private funcionarioMetaService: FuncionarioMetaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.validarFormulario();
    this.carregarFuncionarioMeta();
  }

  public validarFormulario(): void {
    this.form = this.fb.group(       {
      metaId: [],
      funcionarioId:[],
      nomeMeta: ['', Validators.required],
      metaCumprida: [false, Validators.required],
      metaAprovada: [false, Validators.required],
      inicioAcordado: [null],
      fimAcordado: [null],
      inicioRealizado: [null],
      fimRealizado: [null],
      id: [],
    });
  }

  public carregarFuncionarioMeta(): void {

    this.spinner.show();

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('funcionarioId');
    const metaIdParam = this.activatedRouter.snapshot.paramMap.get('metaId');
    console.log("parametro", funcionarioIdParam, metaIdParam )

    this.funcionarioMetaService
      .recuperarFuncionarioIdMetaId(+funcionarioIdParam, +metaIdParam)
      .subscribe(
        (funcionarioMeta: FuncionarioMeta) => {
          this.funcionarioMeta = funcionarioMeta;
          this.form.patchValue(this.funcionarioMeta.meta);
          this.form.patchValue(this.funcionarioMeta);
          console.log("metas", this.funcionarioMeta)
          if (this.funcionarioMeta.inicioRealizado !== null &&
              this.funcionarioMeta.inicioRealizado !== "")
            this.metaIniciada = true;
        },

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

  public iniciarMeta(): void {
    this.spinner.show();

    if (this.form.get("inicioRealizado").value === null ||
      this.form.get("inicioRealizado").value === "") {
      this.spinner.hide();
      this.toastr.info("Informa uma data para início de sua meta!", "Informação!");
    } else {

      this.funcionarioMeta.inicioRealizado = this.form.get("inicioRealizado").value;
      console.log("deleted", this.funcionarioMeta)

      this.funcionarioMetaService
        .salvarFuncionarioMeta(this.funcionarioMeta)
        .subscribe(
          () => {
            this.metaIniciada = true;
            console.log("criado")
          },
          (error: any) => {
            this.toastr.error("Houve falha na atualização da meta.", "Erro:");
            console.error(error);
          }
        )
        .add(() => this.spinner.hide());

    }
  }

  public finalizarMeta(): void {

  }
}
