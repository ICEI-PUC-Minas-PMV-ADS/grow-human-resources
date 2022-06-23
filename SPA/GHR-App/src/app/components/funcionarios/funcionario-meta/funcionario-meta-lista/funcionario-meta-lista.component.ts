import { Meta } from './../../../../models/metas/Meta';
import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-funcionario-meta-lista',
  templateUrl: './funcionario-meta-lista.component.html',
  styleUrls: ['./funcionario-meta-lista.component.scss']
})
export class FuncionarioMetaListaComponent implements OnInit {

  public alteracaoTermoBusca: Subject<string> = new Subject<string>();

  public funcionarioIdParam = +this.activatedRouter.snapshot.paramMap.get('id');

  public funcionario = {} as Funcionario;

  public funcionarioMetas: FuncionarioMeta[] = [];
  public paginacao = {} as Paginacao;
  public metas: Meta[] = [];

  public TotalMetasNaoCumpridas = 0;
  public TotalMetasCumpridas = 0;

  public imagemURL = environment.apiURL + 'recursos/fotos/'

  public iniciar = true;


  constructor(
    private activatedRouter: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private funcionarioService: FuncionarioService,
    private funcionarioMetaService: FuncionarioMetaService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.spinner.show();
    this.consultarFuncionario();
    this.consultarMetasPorFuncionario();
  }

  public filtrarMetas(event: any): void {

    if (this.alteracaoTermoBusca.observers.length === 0) {
      this.spinner.show();

      this.alteracaoTermoBusca
        .pipe(debounceTime(1500))
        .subscribe(
          filtrarPor => {
          this.funcionarioMetaService
            .recuperarMetasPorFuncionarioId(this.funcionarioIdParam, this.paginacao.paginaAtual, this.paginacao.itensPorPagina, filtrarPor)
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

    if (this.funcionarioIdParam !== null) {
      this.spinner.show();
      this.funcionarioService
        .recuperarFuncionarioPorId(this.funcionarioIdParam)
        .subscribe(
          (funcionario: Funcionario) => {
            this.funcionario = funcionario;
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de metas por funcionario", "Erro!");
            console.error(error);
          })
        .add(() => this.spinner.hide());
    };
  }

  public consultarMetasPorFuncionario(): void {

    if (this.funcionarioIdParam !== null) {
      this.spinner.show();

      this.funcionarioMetaService
        .recuperarMetasPorFuncionarioId(
          this.funcionarioIdParam,
          this.paginacao.paginaAtual,
          this.paginacao.itensPorPagina)
        .subscribe(
          (funcionarioMetas: ResultadoPaginacao<FuncionarioMeta[]>) => {
            this.funcionarioMetas = funcionarioMetas.resultado;

            this.paginacao = funcionarioMetas.paginacao;
            this.TotalMetasNaoCumpridas = this.funcionarioMetas.filter(
              (metasNaoCumpridas) => metasNaoCumpridas.metaCumprida == false
              ).length;
            this.TotalMetasCumpridas = this.funcionarioMetas.filter(
              (metasNaoCumpridas) => metasNaoCumpridas.metaCumprida == true
              ).length;
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

    this.consultarMetasPorFuncionario();
  }
}
