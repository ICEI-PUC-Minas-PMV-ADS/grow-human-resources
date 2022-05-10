import { Cargo } from './../../../models/Cargo';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CargoService } from 'src/app/services/Cargo.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

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

  get f(): any
  {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private cargoService: CargoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService)
  {
  }


  ngOnInit(): void
  {
    this.carregarCargo();
    this.validation();
  }
  public validation(): void
  {
    this.form = this.fb.group(
      {
        nomeCargo: ['', [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50)
        ]]
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

  public carregarCargo(): void
  {
    const cargoIdParam = this.router.snapshot.paramMap.get('id');

    if (cargoIdParam !== null)
    {
      this.spinner.show();

      this.estadoSalvar = "put";

      this.cargoService.getCargoById(+cargoIdParam).subscribe(
        (cargo: Cargo) =>
        {
          this.cargo = { ...cargo };
          this.form.patchValue(this.cargo);
        },
        (error: any) =>
        {
          this.toastr.error("Não foi possível carregar a página de departamento", "Erro!");
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void
  {
    this.spinner.show();

    if (this.form.valid)
    {
      this.cargo = (this.estadoSalvar === 'post')
        ? { ...this.form.value }
        : { id: this.cargo.id, ...this.form.value };

      this.cargoService[this.estadoSalvar](this.cargo).subscribe(
        () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),
        (error: any) => {
          console.error(error);
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
        }
      ).add(() => this.spinner.hide());
    };
  }


}
