import { FuncionarioMetaService } from './../../../services/funcionarios/funcionarioMeta.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { FuncionarioService } from './../../../services/funcionarios/funcionario.service';
import { Component, OnInit } from '@angular/core';

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';

import { ChartData, ChartEvent, ChartType } from 'chart.js';

@Component({
  selector: 'app-dashboard-cabTotais',
  templateUrl: './dashboard-cabTotais.component.html',
  styleUrls: ['./dashboard-cabTotais.component.scss']
})
export class DashboardCabTotaisComponent implements OnInit {

  public funcionarios: Funcionario[] = [];
  public funcionariosMetas: FuncionarioMeta[] = [];

  public totalDeFuncionarios = 0;
  public totalDeFuncionariosAtivos = 0;
  public percentFuncionariosAtivos = 0;

  public totalDeMetasDistribuidas = 0;
  public totalDeMetasCumpridas = 0;
  public percentMetasCumpridas = 0;
  public totalDeMetasPendentes = 0;
  public percentMetasPendentes = 0;

  public width = '';



  constructor(
    private funcionarioMetaService: FuncionarioMetaService,
    private funcionarioService: FuncionarioService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,

  ) { }

  ngOnInit() {
    this.carregarFuncionarios();
    this.carregarMetas();
  }

  public carregarFuncionarios(): void {

    this.spinner.show();

    this.funcionarioService
      .recuperarFuncionarios(1, 1000)
      .subscribe(
        (funcionarios: ResultadoPaginacao<Funcionario[]>) => {
          this.funcionarios = funcionarios.resultado;
          this.totalDeFuncionarios = this.funcionarios.length;
          console.log("Func",this.funcionarios )

          this.funcionarios = this.funcionarios.filter(
            (funcionario: Funcionario) =>
              funcionario.ativo == true);
              this.totalDeFuncionariosAtivos = this.funcionarios.length;
              this.percentFuncionariosAtivos = this.totalDeFuncionariosAtivos
                                             / this.totalDeFuncionarios * 100;
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
          this.totalDeMetasDistribuidas = this.funcionariosMetas.length;
          this.funcionariosMetas = this.funcionariosMetas.filter(
            (funcionariosMetas: FuncionarioMeta) => funcionariosMetas.metaCumprida == true);
          this.totalDeMetasCumpridas = this.funcionariosMetas.length;
          this.percentMetasCumpridas = this.totalDeMetasCumpridas
            / this.totalDeMetasDistribuidas * 100;
          this.width = this.percentMetasCumpridas + '%';
          this.totalDeMetasPendentes = this.totalDeMetasDistribuidas
            - this.totalDeMetasCumpridas;
          this.percentMetasPendentes = this.totalDeMetasPendentes
            / this.totalDeMetasDistribuidas * 100;

        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Falha ao carregar as metas dos funcionarios', 'Erro!');
        })
      .add(() => this.spinner.hide());
  }


  // Doughnut
  public doughnutChartLabels: string[] = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales', "D1", "D2"];

  public doughnutChartData: ChartData<'doughnut'> = {
    labels: this.doughnutChartLabels,
    datasets: [
      { data: [350, 450, 100, 100, 150] },

    ]
  };

  public doughnutChartType: ChartType = 'doughnut';

  // events
  public chartClicked({ event, active }: { event: ChartEvent, active: {}[] }): void {
   console.log(event, active);
  }

  public chartHovered({ event, active }: { event: ChartEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public doughnutChartOptions: any = {
    responsive: true,
    cutoutPercentage: 70,
    maintainAspectRatio: true,
    segmentShowStroke: false,
    elements: {
      arc: {
          borderWidth: 0
      }
    },
    legend: {
      display: false,
    }
  };



}
