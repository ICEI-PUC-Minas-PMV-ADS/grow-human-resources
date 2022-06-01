import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Meta } from 'src/app/models/metas/Meta';
import { MetaService } from 'src/app/services/metas/Meta.service';

@Component({
  selector: 'app-meta-Detalhe',
  templateUrl: './meta-Detalhe.component.html',
  styleUrls: ['./meta-Detalhe.component.scss']
})
export class MetaDetalheComponent implements OnInit {

  public form: FormGroup;

  public locale = 'pt-br';

  public meta = {} as Meta;

  public estadoSalvar: string = "post";

  get f(): any
  {
    return this.form.controls;
  }

  get bsConfig(): any
  {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY h:mm a',
      containerClass: 'theme-success'
    };
  }

  constructor(
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private metaService: MetaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService)  {   }

  ngOnInit(): void
  {
    this.carregarMeta();
    this.validation();
  }
  public validation(): void
  {
    this.form = this.fb.group(
      {
        nomeMeta: ['', [  Validators.required,
                          Validators.minLength(4),
                          Validators.maxLength(100)]],
        descricao: ['', [ Validators.required,
                          Validators.minLength(30),
                          Validators.maxLength(1500)]],
        metaCumprida: ['', [ Validators.required]],
        metaAprovada: ['', [ Validators.required]],
        inicioPlanejado: [null],
        fimPlanejado: [null],
        inicioRealizado: [null],
        fimRealizado: [null],
        supervisor: [],
      });
  }

  public limparFormulario(): void
  {
    this.form.reset();
  }

  public validarCampo(campoForm: FormControl): any
  {
    return ValidadorFormularios.verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any
  {
    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public carregarMeta(): void
  {
    const metaIdParam = this.router.snapshot.paramMap.get('id');

    if (metaIdParam !== null)
    {
      this.spinner.show();

      this.estadoSalvar = "put";

      this.metaService
        .recuperarMetaPorId(+metaIdParam)
        .subscribe(
          (meta: Meta) =>
          {
            this.meta = { ...meta };
            this.form.patchValue(this.meta);
          },
          (error: any) =>
          {
            this.toastr.error("Não foi possível carregar a página de departamento", "Erro!");
            console.error(error);
          })

        .add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {
      if (this.estadoSalvar === 'post') {
        this.criarMeta();
      } else {
        this.salvarMeta();
      }
    }
  }

  public criarMeta(): void {
    this.meta = { ...this.form.value }

    this.metaService
      .criarMeta(this.meta)
      .subscribe(
        () => this.toastr.success("Meta Criada com sucesso!", "Salvo!"),
        (error: any) => {
          console.error(error);
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
        })

      .add(() => this.spinner.hide());
  }

  public salvarMeta(): void {
    this.meta = { id: this.meta.id, ...this.form.value };

    this.metaService
      .SalvarMeta(this.meta)
      .subscribe(
        () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),

        (error: any) => {
          console.error(error);
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
        })

      .add(() => this.spinner.hide());
    }
}
