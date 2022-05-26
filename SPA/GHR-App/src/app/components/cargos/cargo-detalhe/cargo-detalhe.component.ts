import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators} from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { Cargo } from '../../../models/cargos/Cargo';
import { Departamento } from 'src/app/models/departamentos/Departamento';

import { DepartamentoService } from 'src/app/services/departamentos/departamento.service';
import { CargoService } from 'src/app/services/cargos/Cargo.service';
import { ResultadoPaginacao } from 'src/app/models/paginacao/paginacao';

@Component({
  selector: 'app-cargo-detalhe',
  templateUrl: './cargo-detalhe.component.html',
  styleUrls: ['./cargo-detalhe.component.scss']
})

export class CargoDetalheComponent implements OnInit {

  form!: FormGroup;
  locale = 'pt-br';
  cargo = {} as Cargo;
  estadoSalvar: string = "post";
  public departamentos: Departamento[] = [];

  get f(): any {

    return this.form.controls;
  }

  constructor(
    private cargoService: CargoService,
    private departamentoService: DepartamentoService,
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {
  }


  ngOnInit(): void {

    this.validarFormulario();
    this.consultarCargos();
    this.consultarDepartamento();
  }

  public validarFormulario(): void {
    this.form = this.fb.group({
      nomeCargo: ['', [ Validators.required,
                        Validators.minLength(4),
                        Validators.maxLength(50)]],
      funcao: ['', Validators.required],
      departamentoId: ['', Validators.required],
    });
  }

  public limparFormulario(): void   {

    this.form.reset();
  }

  public validarCampo(campoForm: FormControl): any {

    return ValidadorFormularios.verificarErroCampo(campoForm);
  }

  public retornarValidacao(nomeCampo: FormControl, nomeElemento: string): any {

    return ValidadorFormularios.retornarMensagemErro(nomeCampo, nomeElemento);
  }

  public consultarCargos(): void {

    const cargoIdParam = this.router.snapshot.paramMap.get('id');

    if (cargoIdParam !== null){

      this.spinner.show();

      this.estadoSalvar = "put";

      this.cargoService
        .recuperarCargoPorId(+cargoIdParam).subscribe(
        (cargo: Cargo) => {
          this.cargo = { ...cargo };
          this.form.patchValue(this.cargo);},
        (error: any) => {
          this.toastr.error("Não foi possível carregar os dados de cargos", "Erro!");
          console.error(error);
        }).add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void {

    this.spinner.show();

    if (this.form.valid) {

      this.cargo = (this.estadoSalvar === 'post')
        ? { ...this.form.value }
        : { id: this.cargo.id, ...this.form.value };


      console.log(this.cargo.departamentoId, this.cargo, this.estadoSalvar)
      this.cargoService[this.estadoSalvar](this.cargo).subscribe(
        () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),
        (error: any) => {
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
          console.error(error);
        }).add(() => this.spinner.hide());
    };
  }

  public consultarDepartamento(): void
  {
    this.spinner.show();

    this.departamentoService
      .recuperarDepartamentos()
      .subscribe(
        (departamentoRetorno: ResultadoPaginacao<Departamento[]>) =>{
          this.departamentos = departamentoRetorno.resultado;},
        (error: any) => {
          this.toastr.error("Não foi possível carregar os departamentos", "Erro!");
          console.error(error); }
      ).add(() => this.spinner.hide());
    }
}
