import { DateTimeFormatPipe } from './../../../../helpers/DateTimeFormat.pipe';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { Meta } from 'src/app/models/metas/Meta';
import { NavegacaoEntreForms } from 'src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';
import { MetaService } from 'src/app/services/metas/Meta.service';
import { environment } from 'src/environments/environment';
import { Constants } from 'src/app/util/constants';

@Component({
  selector: 'app-funcionario-meta-lista',
  templateUrl: './funcionario-meta-lista.component.html',
  styleUrls: ['./funcionario-meta-lista.component.scss']
})
export class FuncionarioMetaListaComponent implements OnInit {

  public alteracaoTermoBusca: Subject<string> = new Subject<string>();

  public form!: FormGroup;

  public funcionario = {} as Funcionario;
  public metas: Meta[] = [];
  public meta = {} as Meta;
  public funcionarioMeta = {} as FuncionarioMeta;
  public funcionarioMetas = [] as FuncionarioMeta[];
  public paginacao = {} as Paginacao;

  public TotalMetasNaoCumpridas = 0;
  public TotalMetasCumpridas = 0;

  public imagemURL = environment.apiURL + 'recursos/fotos/'

  public iniciar = true;


  constructor(
    private activatedRouter: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private funcionarioService: FuncionarioService,
    private metaService: MetaService,
    private funcionarioMetaService: FuncionarioMetaService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.spinner.show();
    this.consultarFuncionario();
    this.consultarMetasPorFuncionario();
  }

  public filtrarMetas(event: any): void {
    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');

    if (this.alteracaoTermoBusca.observers.length === 0) {
      this.spinner.show();

      this.alteracaoTermoBusca
        .pipe(debounceTime(1500))
        .subscribe(
          filtrarPor => {
          this.funcionarioMetaService
            .recuperarMetasPorFuncionarioId(+funcionarioIdParam, this.paginacao.paginaAtual, this.paginacao.itensPorPagina, filtrarPor)
            .subscribe(
              (MetasRetorno: ResultadoPaginacao<FuncionarioMeta[]>) => {
                this.funcionarioMetas = MetasRetorno.resultado;
                this.paginacao = MetasRetorno.paginacao;},
              (error: any) => this.toastr.error('Falha ao carregar as metas', 'Erro!'))
            .add(() => this.spinner.hide());
          }
        )
    }
    this.alteracaoTermoBusca.next(event.value);
    }

  public consultarFuncionario(): void {

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');
    console.log("func id", funcionarioIdParam);
    if (funcionarioIdParam !== null) {
      this.spinner.show();
      this.funcionarioService
        .recuperarFuncionarioPorId(+funcionarioIdParam)
        .subscribe(
          (funcionario: Funcionario) => {
            console.log("func", funcionario)
            this.funcionario = funcionario;
            this.imagemURL = (this.funcionario.contas.imagemURL !== '' && this.funcionario.contas.imagemURL !== null)
              ? environment.apiURL + 'recursos/fotos/' + this.funcionario.contas.imagemURL
              : "../../../../assets/img/semImagem.jfif";
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de metas por funcionario", "Erro!");
            console.error(error);
          })
        .add(() => this.spinner.hide());
    };

  }

  public consultarMetasPorFuncionario(): void {

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');

    if (funcionarioIdParam !== null) {
      this.spinner.show();

      this.funcionarioMetaService
        .recuperarMetasPorFuncionarioId(
          +funcionarioIdParam,
          this.paginacao.paginaAtual,
          this.paginacao.itensPorPagina)
        .subscribe(
          (funcionarioMetasRetorno: ResultadoPaginacao<FuncionarioMeta[]>) => {
            this.funcionarioMetas = funcionarioMetasRetorno.resultado;
            this.paginacao = funcionarioMetasRetorno.paginacao;
            this.TotalMetasNaoCumpridas = this.funcionarioMetas.filter(
              (metasNaoCumpridas) => metasNaoCumpridas.metaCumprida == false
            ).length;
            this.TotalMetasCumpridas = this.funcionarioMetas.filter(
              (metasNaoCumpridas) => metasNaoCumpridas.metaCumprida == true
            ).length;
            console.log("metas", funcionarioMetasRetorno)
            console.log("cont", this.funcionarioMetas.filter(
              (metasNaoCumpridas) => metasNaoCumpridas.metaCumprida == false
            ).length);
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de metas por funcionário", "Erro!");
            console.error(error);
          })
        .add(() => this.spinner.hide());
    }
  }

  public paginaAlterada(event): void {
    this.paginacao.paginaAtual = event.page;
    console.log("page", event.page);
    this.consultarMetasPorFuncionario();
  }

  public finalizarMeta(metaId: number): void {

  }

  public iniciarMeta(metaId: number): void {

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');

    if (funcionarioIdParam !== null) {
      this.spinner.show();

      console.log('metaId', metaId);

      this.funcionarioMetaService
        .recuperarFuncionarioIdMetaId(+funcionarioIdParam, metaId)
        .subscribe(
          (funcionarioMeta: FuncionarioMeta) => {
            this.funcionarioMeta = funcionarioMeta;
            this.funcionarioMeta.inicioRealizadb = new Date(Date.now()).toString();
            console.log(this.funcionarioMeta)
            this.funcionarioMetaService
              .salvarFuncionarioMeta(this.funcionarioMeta)
              .subscribe(
                (funcionarioMeta: FuncionarioMeta) => {
                  this.iniciar = false;
                  this.toastr.success("Meta atualizada!", "sucesso!");
                })
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de metas por funcionário", "Erro!");
            console.error(error);
          })
        .add(() => this.spinner.hide());
    }
  }
  public funcionarioDetalheMeta(funcionarioId: number, metaId: number): void {
    this.router.navigate([`funcionarios/detalhe/meta/${funcionarioId}/${metaId}`]);
  }
}
