import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { Departamento } from 'src/app/models/departamentos/Departamento';
import { DepartamentoService } from 'src/app/services/departamentos/departamento.service';
import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { Paginacao, ResultadoPaginacao } from 'src/app/models/suporte/paginacao/paginacao';
import { stringify } from 'querystring';

@Component({
  selector: 'app-departamento-detalhe',
  templateUrl: './departamento-detalhe.component.html',
  styleUrls: ['./departamento-detalhe.component.scss']
})
export class DepartamentoDetalheComponent implements OnInit {

  public locale = 'pt-br';

  public form: FormGroup;

  public departamento = {} as Departamento;
  public funcionarios: Funcionario[] = [];
  public funcionariosDiretor: Funcionario[] = [];
  public funcionariosGerente: Funcionario[] = [];
  public funcionariosSupervisor: Funcionario[] = [];
  public paginacao = {} as Paginacao;

  estadoSalvar: string = "cadastrarDepartamento";

  get ctrDep(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private departamentoService: DepartamentoService,
    private funcionarioService: FuncionarioService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }


  ngOnInit(): void {

    this.carregarDepartamento();
    this.carregarFuncionariosPorDepartamentoId();
    this.validation();

  }

  public validation(): void {

    this.form = this.fb.group({
      nomeDepartamento: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)]],
      siglaDepartamento: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(7)]],
      diretor: ['NaoInformado'],
      gerente: ['NaoInformado'],
      supervisor: ['NaoInformado'],
    });

  }

  public limparFormulario(): void {
    this.form.reset();
  }

  public validarCampo(campoForm: FormControl): any {
    return ValidadorFormularios
      .verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {
    return ValidadorFormularios
      .retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public carregarDepartamento(): void {
    const departamentoIdParam = this.router.snapshot.paramMap.get('id');

    if (departamentoIdParam !== null) {
      this.spinner.show();

      this.estadoSalvar = "alterarDepartamento";

      this.departamentoService
        .recuperarDepartamentoById(+departamentoIdParam)
        .subscribe(
          (departamento: Departamento) => {
            this.departamento = departamento;
            this.form.patchValue(this.departamento);
          },

          (error: any) => {
            this.toastr.error("Não foi possível carregar a página de departamento", "Erro!");
            console.error(error);
          })

        .add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void {

    this.spinner.show();

    if (this.form.valid) {
      this.departamento = (this.estadoSalvar === 'cadastrarDepartamento')
        ? { ...this.form.value }
        : { id: this.departamento.id, ...this.form.value };

      this.departamentoService[this.estadoSalvar](this.departamento)
        .subscribe(
          () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),

          (error: any) => {

            console.error(error);
            this.toastr.error("Erro ao salvar alterações.", "Erro!");
          })

        .add(() => this.spinner.hide());
    };
  }

  public carregarFuncionariosPorDepartamentoId(): void {
    this.spinner.show();
    const departamentoIdParam = +this.router.snapshot.paramMap.get('id');
    console.log("funcDiretor", this.funcionarios, departamentoIdParam);

    this.funcionarioService
      .recuperarFuncionarioPorDepartamentoId(+departamentoIdParam)
      .subscribe(
        (funcionarios: Funcionario[]) => {
          this.funcionarios = funcionarios;
          this.funcionariosDiretor = funcionarios.filter(
            f => f.cargos.funcao === 'Diretor');
          console.log("funcDiretor", this.funcionariosDiretor);
          this.funcionariosGerente = funcionarios.filter(
            f => f.cargos.funcao === 'Gerente');
          console.log("funcDiretor", this.funcionariosGerente);
          this.funcionariosSupervisor = funcionarios.filter(
            f => f.cargos.funcao === 'Supervisor');
          console.log("funcDiretor", this.funcionariosSupervisor);
        },
        (error: any) => {
          this.toastr.error("Não foi possível carregar funcionario", "Erro!");
          console.error(error);
        })

        .add(() => this.spinner.hide())
  }
}

