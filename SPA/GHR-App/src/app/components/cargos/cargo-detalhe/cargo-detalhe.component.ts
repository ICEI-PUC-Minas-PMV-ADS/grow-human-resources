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

import { Cargo } from './../../../models/Cargo';

import { CargoService } from 'src/app/services/Cargo.service';

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

  get f(): any {

    return this.form.controls;
  }

  constructor(
    private cargoService: CargoService,
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {
  }


  ngOnInit(): void {
  
    this.validarFormulario();
    this.consultarCargos();
  }
  
  public validarFormulario(): void {
    this.form = this.fb.group({
      nomeCargo: ['', [ Validators.required,
                        Validators.minLength(4),
                        Validators.maxLength(50)]],
      nivel: ['', Validators.required],
      recursosHumanos: ['', Validators.required],
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

      this.cargoService.getCargoById(+cargoIdParam).subscribe(
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

      this.cargoService[this.estadoSalvar](this.cargo).subscribe(
        () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),
        (error: any) => {
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
          console.error(error);
        }).add(() => this.spinner.hide());
    };
  }

}
