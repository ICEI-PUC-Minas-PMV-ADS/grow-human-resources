import { FuncionariosMetasDashboard } from './../../../models/suporte/dashboard/FuncionariosMetasDashboard';
import { ListaDashboard } from '../../../models/suporte/dashboard/ListaDashboard';
import { FuncionarioMetaService } from './../../../services/funcionarios/funcionarioMeta.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { FuncionarioService } from './../../../services/funcionarios/funcionario.service';
import { Component, OnInit } from '@angular/core';

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';

import { ChartData, ChartEvent, ChartType} from 'chart.js';
import { FuncionarioDashboard } from '../../../models/suporte/dashboard/FuncionarioDashboard';
import { MetaDashboard } from '../../../models/suporte/dashboard/MetaDashboard';


@Component({
  selector: 'app-dashboard-cabTotais',
  templateUrl: './dashboard-cabTotais.component.html',
  styleUrls: ['./dashboard-cabTotais.component.scss']
})
export class DashboardCabTotaisComponent implements OnInit {
  public paginaAtualParam: 1;
  public linhasParam: 1000;

  public cores: string[] = [];

  public funcionariosDashboard: FuncionarioDashboard[] = [];
  public metaDashboard: MetaDashboard[] = [];


  public funcionarios: Funcionario[] = [];
  public funcionariosMetas: FuncionarioMeta[] = [];

  public metasCumpridasTop5: ListaDashboard[] = [];
  public metasNaoCumpridasTop5: ListaDashboard[] = [];

  public metasCumpridasDepto: ListaDashboard[] = [];
  public metasNaoCumpridasDepto: ListaDashboard[] = [];

  public metasCumpridasDeptoA: ListaDashboard[] = [];
  public metasNaoCumpridasDeptoA: ListaDashboard[] = [];

  // Doughnut
  public doughnutChartLabels: string[] = [];
  public doughnutChartValues: number[] = [];
  public doughnutChartData: ChartData<'doughnut'> = {
    labels: this.doughnutChartLabels,
    datasets: [{ data: this.doughnutChartValues }]
  };
  public doughnutChartType: ChartType = 'doughnut';
  public doughnutChartOptions: any = {
    responsive: true,
    cutoutPercentage: 90,
    maintainAspectRatio: true,
    segmentShowStroke: true,
    elements: {
      arc: {
        borderWidth: 0
      }
    },
    legend: {
      display: false,
    },
    options: {
      plugins: {
        datalabels: {
          formatter: (value, ctx) => {
            return ctx.chartData.dataset.data[ctx.labelsIndex]
          }
        }
      }
    }
  };


  public cabDashboardTotFunc = 0;
  public cabDashboardTotFuncAtivo = 0;
  public cabDashboardPerFuncAtivo = 0;

  public cabDashboardTotMeta = 0;
  public cabDashboardTotMetaCumprida = 0;
  public cabDashboardPerMetaCumprida = 0;
  public cabDashboardTotMetaPendente = 0;
  public cabDashboardPerMetaPendente = 0;

  public cabDashboardWhidthMetas = '';

  constructor(
    private funcionarioMetaService: FuncionarioMetaService,
    private funcionarioService: FuncionarioService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,

  ) { }

  ngOnInit() {

    this.carregarFuncionarios();
    this.carregarMetas();
    console.log('Funcionarios', this.funcionariosDashboard)

  }

  public carregarFuncionarios(): void {

    this.spinner.show();

    this.funcionarioService
      .recuperarFuncionarios(this.paginaAtualParam, this.linhasParam)
      .subscribe(
        (funcionarios: ResultadoPaginacao<Funcionario[]>) => {
          this.funcionarios = funcionarios.resultado;
          this.funcionarios.forEach((func, i) => {
            this.funcionariosDashboard[i] = new FuncionarioDashboard(
              func.id,
              func.ativo,
              func.cargos.nomeCargo,
              func.departamentoId,
              func.departamentos.nomeDepartamento,
              func.departamentos.siglaDepartamento,
              func.contas.nomeCompleto,
              func.contas.userName);
          })
          this.montarCabDashboardFunc();
          this.montarDoughnut();

        },
        (error: any) => {
          this.toastr.error('Falha ao carregar os funcionários', 'Erro!');
          console.error(error);
        })
      .add(() => this.spinner.hide());
  }

  public carregarMetas(): void {
    this.funcionarioMetaService
      .recuperarFuncionariosMetas()
      .subscribe(
        (funcionariosMetas: FuncionarioMeta[]) => {
          this.funcionariosMetas = funcionariosMetas;

          this.funcionariosMetas.forEach((meta, i) => {
            this.metaDashboard[i] = new MetaDashboard(
              meta.funcionarioId,
              meta.funcionarios.ativo,
              meta.metaCumprida,
              meta.metaId,
              meta.metas.metaAprovada,
              meta.metas.nomeMeta
            );
          });
          console.log("tupla Meta", this.metaDashboard)
          this.montarCabDashboardMeta();
          this.topFiveMetas();
          this.metasPorDepartamento();
          this.metasPorDepartamentoAgregado();
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Falha ao carregar as metas dos funcionarios', 'Erro!');
        })
      .add(() => this.spinner.hide());

  }

  public montarCabDashboardFunc(): void {

    this.cabDashboardTotFunc = this.funcionariosDashboard.length;

    this.cabDashboardTotFuncAtivo = this.funcionariosDashboard.filter(
      (fd) => fd.funcionarioAtivo == true).length;

    this.cabDashboardPerFuncAtivo = +(this.cabDashboardTotFuncAtivo
      / this.cabDashboardTotFunc * 100).toFixed(2);
  }

  public montarCabDashboardMeta(): void {

    this.cabDashboardTotMeta = this.metaDashboard.length;

    this.cabDashboardTotMetaCumprida = this.metaDashboard
      .filter((fm) => fm.metaCumprida == true).length;

    this.cabDashboardPerMetaCumprida = +(this.cabDashboardTotMetaCumprida
      / this.cabDashboardTotMeta * 100).toFixed(2);

    this.cabDashboardWhidthMetas = this.cabDashboardPerMetaCumprida + '%';

    this.cabDashboardTotMetaPendente = +(this.cabDashboardTotMeta
      - this.cabDashboardTotMetaCumprida).toFixed(2);

    this.cabDashboardPerMetaPendente = +(this.cabDashboardTotMetaPendente
      / this.cabDashboardTotMeta * 100).toFixed(2);
  }

  // Doughnut
  // events
  public chartClicked({ event, active }: { event: ChartEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event: ChartEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public montarDoughnut(): void {
    var ListaDepartamentos: string[] = [];
    var ListaDepartamentosSemDuplicidade: string[] = [];

    var funcionariosPorDepartamento: FuncionarioDashboard[] = [];

    funcionariosPorDepartamento = this.funcionariosDashboard
      .sort(func => func.funcionarioDepartamentoId)

    funcionariosPorDepartamento.forEach((depto) => {
      ListaDepartamentos.push(depto.funcionarioDepartamento);
    });

    console.log("ListaDepartamentos", ListaDepartamentos)

    ListaDepartamentosSemDuplicidade = ListaDepartamentos
      .filter((ele, pos) => ListaDepartamentos.indexOf(ele) == pos);

    console.log("ListaDepartamentosSemDuplicidade", ListaDepartamentosSemDuplicidade)

    for (var dep of ListaDepartamentosSemDuplicidade) {
      this.doughnutChartLabels.push(dep)
    }

    for (var i in ListaDepartamentosSemDuplicidade) {
      this.doughnutChartValues.push(funcionariosPorDepartamento
        .filter((func) => func.funcionarioDepartamento == ListaDepartamentosSemDuplicidade[i]).length)
    }
    console.log("doughnuts", this.doughnutChartLabels, this.doughnutChartValues)

  }

  public topFiveMetas(): void {
    var listaMetas: string[] = [];
    var listametasSemDuplicidade: string[] = [];

    var listaMetasCumpridas: ListaDashboard[] = [];
    var listaMetasOrdenadas: ListaDashboard[] = [];
    var listaMetasNaoCumpridas: ListaDashboard[] = [];



    var meta4: ListaDashboard[] = [];

    this.metaDashboard.forEach((fm) => {
      listaMetas.push(fm.nomeMeta)
    })

    console.log("listaMetas", listaMetas);

    listametasSemDuplicidade = listaMetas
      .filter((ele, pos) => listaMetas.indexOf(ele) == pos);

    console.log("listaMetasSemDuplicidade", listametasSemDuplicidade);

    this.montaCor();

    for (var i in listametasSemDuplicidade) {
      var qtdMetas = this.metaDashboard
        .filter((fm) => fm.nomeMeta == listametasSemDuplicidade[i]).length;
      listaMetasCumpridas[i] = new ListaDashboard(this.metaDashboard
        .filter(
          (fm) => fm.nomeMeta == listametasSemDuplicidade[i]
            && fm.metaCumprida == true).length,
        listametasSemDuplicidade[i],
        qtdMetas,
        this.cores[i]);
    }
    console.log("listaMetasCumpridas", listaMetasCumpridas)

    listaMetasOrdenadas = listaMetasCumpridas.sort((a, b) => (a.qtde > b.qtde) ? -1 : 1);

    console.log("listaMetasOrdenadas", listaMetasOrdenadas)

    this.metasCumpridasTop5 = listaMetasOrdenadas.filter((funcionarioMeta, index) => index < 5)
    console.log("metasCumpridasTop5", this.metasCumpridasTop5)

    this.montaCor();


    for (var i in listametasSemDuplicidade) {
      var qtdMetas = this.metaDashboard.filter((fm) => fm.nomeMeta == listametasSemDuplicidade[i]).length;
      listaMetasNaoCumpridas[i] = new ListaDashboard(this.metaDashboard
        .filter(
          (fm) => fm.nomeMeta == listametasSemDuplicidade[i] && fm.metaCumprida == false).length,
        listametasSemDuplicidade[i],
        qtdMetas,
        this.cores[i]);
    }

    console.log("listaMetasNaoCumpridas", listaMetasNaoCumpridas)

    listaMetasOrdenadas = listaMetasNaoCumpridas.sort((a, b) => (a.qtde > b.qtde) ? -1 : 1);
    console.log("listaMetasOrdenadas", listaMetasOrdenadas)

    this.metasNaoCumpridasTop5 = listaMetasOrdenadas.filter((funcionarioMeta, index) => index < 5)

    console.log("metasNaoCumpridasTop5", this.metasNaoCumpridasTop5)

  }

  public metasPorDepartamento(): void {
    var funcionarioMetasDashboard: FuncionariosMetasDashboard[] = [];

    var listaDepartamentos: string[] = [];
    var listametasSemDuplicidade: string[] = [];

    var listaMetasCumpridas: ListaDashboard[] = [];
    var listaMetasOrdenadas: ListaDashboard[] = [];
    var listaMetasNaoCumpridas: ListaDashboard[] = [];

    console.log("$$$$$$$$$$$$$$$$$$$$$$$$$$$")

    var j = 0;

    for (var func of this.funcionariosDashboard) {
      var metas = this.metaDashboard.filter(fm => fm.funcionarioId == func.funcionarioId)
      for (var meta of metas) {
        funcionarioMetasDashboard[j] = new FuncionariosMetasDashboard(
          func.funcionarioId,
          func.funcionarioAtivo,
          func.funcionarioCargo,
          func.funcionarioDepartamentoId,
          func.funcionarioDepartamento,
          func.funcionarioSigla,
          func.funcionarioNome,
          func.funcionarioUser,
          meta.metaCumprida,
          meta.metaId,
          meta.metaAprovada,
          meta.nomeMeta
        )
        j++;
      }
    }
    console.log("---------", funcionarioMetasDashboard)

    funcionarioMetasDashboard.forEach((fm) => {
      listaDepartamentos.push(fm.funcionarioDepartamento)
    })

    console.log("listaDepartamentos", listaDepartamentos);

    listametasSemDuplicidade = listaDepartamentos
      .filter((ele, pos) => listaDepartamentos.indexOf(ele) == pos);

    console.log("listaMetasSemDuplicidade", listametasSemDuplicidade);

    this.montaCor();

    for (var i in listametasSemDuplicidade) {
      var qtdMetas = funcionarioMetasDashboard.filter(
        (fm) => fm.funcionarioDepartamento == listametasSemDuplicidade[i]).length;
      listaMetasCumpridas[i] = new ListaDashboard(funcionarioMetasDashboard
        .filter(
          (fm) => fm.funcionarioDepartamento == listametasSemDuplicidade[i] && fm.metaCumprida == true).length,
        listametasSemDuplicidade[i],
        qtdMetas,
        this.cores[i]);
    }
    console.log("listaMetasCumpridas", listaMetasCumpridas)

    listaMetasOrdenadas = listaMetasCumpridas.sort((a, b) => (a.qtde > b.qtde) ? -1 : 1);

    console.log("listaMetasOrdenadas", listaMetasOrdenadas)

    this.metasCumpridasDepto = listaMetasOrdenadas.filter((funcionarioMeta, index) => index < 5)
    console.log("metasCumpridasTop5", this.metasCumpridasDepto)

    this.montaCor();


    for (var i in listametasSemDuplicidade) {
      var qtdMetas = funcionarioMetasDashboard.filter(
        (fm) => fm.funcionarioDepartamento == listametasSemDuplicidade[i]).length;
      listaMetasNaoCumpridas[i] = new ListaDashboard(funcionarioMetasDashboard
        .filter(
          (fm) => fm.funcionarioDepartamento == listametasSemDuplicidade[i] && fm.metaCumprida == false).length,
        listametasSemDuplicidade[i],
        qtdMetas,
        this.cores[i]);
    }

    console.log("listaMetasNaoCumpridas", listaMetasNaoCumpridas)

    listaMetasOrdenadas = listaMetasNaoCumpridas.sort((a, b) => (a.qtde > b.qtde) ? -1 : 1);
    console.log("listaMetasOrdenadas", listaMetasOrdenadas)

    this.metasNaoCumpridasDepto = listaMetasOrdenadas.filter((funcionarioMeta, index) => index < 5)

    console.log("metasNaoCumpridasTop5", this.metasNaoCumpridasDepto)

  }

  public metasPorDepartamentoAgregado(): void {
    var funcionarioMetasDashboard: FuncionariosMetasDashboard[] = [];

    var listaDepartamentos: string[] = [];
    var listametasSemDuplicidade: string[] = [];

    var listaMetasCumpridas: ListaDashboard[] = [];
    var listaMetasOrdenadas: ListaDashboard[] = [];
    var listaMetasNaoCumpridas: ListaDashboard[] = [];

    console.log("$$$$$$$$$$$$$$$$$$$$$$$$$$$")

    var j = 0;

    for (var func of this.funcionariosDashboard) {
      var metas = this.metaDashboard.filter(fm => fm.funcionarioId == func.funcionarioId)
      for (var meta of metas) {
        funcionarioMetasDashboard[j] = new FuncionariosMetasDashboard(
          func.funcionarioId,
          func.funcionarioAtivo,
          func.funcionarioCargo,
          func.funcionarioDepartamentoId,
          func.funcionarioDepartamento,
          func.funcionarioSigla,
          func.funcionarioNome,
          func.funcionarioUser,
          meta.metaCumprida,
          meta.metaId,
          meta.metaAprovada,
          meta.nomeMeta
        )
        j++;
      }
    }
    console.log("---------", funcionarioMetasDashboard)

    funcionarioMetasDashboard.forEach((fm) => {
      listaDepartamentos.push(fm.funcionarioDepartamento)
    })

    console.log("listaDepartamentos", listaDepartamentos);

    listametasSemDuplicidade = listaDepartamentos
      .filter((ele, pos) => listaDepartamentos.indexOf(ele) == pos);

    console.log("listaMetasSemDuplicidade", listametasSemDuplicidade);

    var qtdMetas = funcionarioMetasDashboard.length;
    this.montaCor();

    for (var i in listametasSemDuplicidade) {
      listaMetasCumpridas[i] = new ListaDashboard(funcionarioMetasDashboard
        .filter(
          (fm) => fm.funcionarioDepartamento == listametasSemDuplicidade[i] && fm.metaCumprida == true).length,
        listametasSemDuplicidade[i],
        qtdMetas,
        this.cores[i]);
    }
    console.log("listaMetasCumpridas", listaMetasCumpridas)

    listaMetasOrdenadas = listaMetasCumpridas.sort((a, b) => (a.qtde > b.qtde) ? -1 : 1);

    console.log("listaMetasOrdenadas", listaMetasOrdenadas)

    this.metasCumpridasDeptoA = listaMetasOrdenadas.filter((funcionarioMeta, index) => index < 5)
    console.log("metasCumpridasTop5", this.metasCumpridasDepto)


    var qtdMetas = funcionarioMetasDashboard.length;
    this.montaCor();

    for (var i in listametasSemDuplicidade) {
      listaMetasNaoCumpridas[i] = new ListaDashboard(funcionarioMetasDashboard
        .filter(
          (fm) => fm.funcionarioDepartamento == listametasSemDuplicidade[i] && fm.metaCumprida == false).length,
        listametasSemDuplicidade[i],
        qtdMetas,
        this.cores[i]);
    }

    console.log("listaMetasNaoCumpridas", listaMetasNaoCumpridas)

    listaMetasOrdenadas = listaMetasNaoCumpridas.sort((a, b) => (a.qtde > b.qtde) ? -1 : 1);
    console.log("listaMetasOrdenadas", listaMetasOrdenadas)

    this.metasNaoCumpridasDeptoA = listaMetasOrdenadas.filter((funcionarioMeta, index) => index < 5)

    console.log("metasNaoCumpridasTop5", this.metasNaoCumpridasDepto)

  }

  public gerarCor() {
    var hexadecimais = '0123456789ABCDEF';

    var cor = '#';

    for (var i = 0; i < 6; i++) {
      cor += hexadecimais[Math.floor(Math.random() * 16)];
    }
    return cor;
  }

  public montaCor() {

    for (var i = 0; i < 12; i++) {
      this.cores[i] = this.gerarCor();
    }
    console.log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@Cores", this.cores)
  }

    public lineChartData: ChartDataSets[] = [
    { data: [61, 59, 80, 65, 45, 55, 40, 56, 76, 65, 77, 60], label: 'Apple' },
    { data: [57, 50, 75, 87, 43, 46, 37, 48, 67, 56, 70, 50], label: 'Mi' },
  ];

  public lineChartLabels: Label[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

  public lineChartOptions = {
    responsive: true,
  };

  public lineChartLegend = true;
  public lineChartType = 'line';
  public lineChartPlugins = [];
}
