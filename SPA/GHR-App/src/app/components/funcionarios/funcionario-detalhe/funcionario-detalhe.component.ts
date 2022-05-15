import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Funcionario } from 'src/app/models/Funcionario';
import { Departamento } from 'src/app/models/Departamento';

import { FuncionarioService } from 'src/app/services/funcionario.service';
import { DepartamentoService } from 'src/app/services/departamento.service';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';
import { environment } from 'src/environments/environment';
import { Cargo } from 'src/app/models/Cargo';
import { CargoService } from 'src/app/services/Cargo.service';

@Component({
  selector: 'app-funcionario-detalhe',
  templateUrl: './funcionario-detalhe.component.html',
  styleUrls: ['./funcionario-detalhe.component.scss']
})
export class FuncionarioDetalheComponent implements OnInit {

  public form!: FormGroup;
  public locale = 'pt-br';
  public funcionario = {} as Funcionario;
  public estadoSalvar: string = "post";
  public departamentos: Departamento[] = [];
  public cargos: Cargo[] = [];
  public funcionarioId: number;
  public imagemUpload = 'assets/img/upload.jfif';
  public file: File;

  get f(): any
  {
    return this.form.controls;
  }

  get modoEditar(): Boolean {
    return this.estadoSalvar === 'put';
  }
  
  get bsConfig(): any
  {
    return {isAnimated: true, adaptivePosition: true, dateInputFormat: 'DD/MM/YYYY h:mm a', containerClass: 'theme-default'};
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private routerActivated: ActivatedRoute,
    private router: Router,
    private funcionarioService: FuncionarioService,
    private departamentoService: DepartamentoService,
    private cargoService: CargoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService)
  {
    this.localeService.use(this.locale);
  }


  ngOnInit(): void
  {
    this.validation();
    this.carregarFuncionario();
    this.consultarCargos();
    this.ConsultarDepartametos();
  }
  public validation(): void
  {
    this.form = this.fb.group(
      {
        nomeCompleto: ['', [  Validators.required,
                              Validators.minLength(4),
                              Validators.maxLength(50)]],
        email: ['', [ Validators.required,
                      Validators.email]],
        telefone: ['', Validators.required],
        salario: ['', Validators.required],
        dataAdmissao: ['', Validators.required],
        dataDemissao: [null],
        imagemURL: ['semfoto.jpg', Validators.required],
        departamentoId: [''],
        supervisorId: [''],
        cargoId: [''],
        funcionarioAtivo: ['true']
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

  public carregarFuncionario(): void
  {
    this.funcionarioId = +this.routerActivated.snapshot.paramMap.get('id');
    if (this.funcionarioId  !== null && this.funcionarioId !== 0)
    {
      this.spinner.show();

      this.estadoSalvar = "put";

      this.funcionarioService
        .getFuncionarioById(this.funcionarioId)
        .subscribe(
          (funcionario: Funcionario) => {
            this.funcionario = { ...funcionario };
            this.form.patchValue(this.funcionario);
            if (this.funcionario.imagemURL !== 'semfoto.jpg') {
              this.imagemUpload = environment.apiURL + 'Resources/images/' + this.funcionario.imagemURL;
            }
          },
          (error: any) =>
          {
            this.toastr.error("Não foi possível carregar a página de funcionário", "Erro!");
            console.error(error);
          }
        )
        .add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void
  {
    this.spinner.show();

    if (this.form.valid)
    {
      this.funcionario = (this.estadoSalvar === 'post')
        ? { ...this.form.value }
        : { id: this.funcionario.id, ...this.form.value };

       console.log(this.funcionario.departamentoId)
      this.funcionario.loginId = 1;
      this.funcionario.supervisorId = 1;

      this.funcionarioService[this.estadoSalvar](this.funcionario).subscribe(
        () => this.toastr.success("Alterações salvas com sucesso!", "Salvo!"),
        (error: any) => {
          console.error(error);
          this.toastr.error("Erro ao salvar alterações.", "Erro!");
        }
      ).add(() => this.spinner.hide());
    };
  }

  public ConsultarDepartametos(): void {
    this.spinner.show();

    this.departamentoService.getDepartamentos().subscribe(
      (departamentoRetorno: Departamento[]) => {
        this.departamentos = departamentoRetorno;
      },
      (error: any) => {
        this.toastr.error("Não foi possível carregar os departamentos", "Erro!");
        console.error(error);
      }
    ).add(() => this.spinner.hide());
   }
  
  public consultarCargos(): void  {
      this.spinner.show();

      this.cargoService.getCargos().subscribe(
        (cargoRetorno: Cargo[]) => {
          this.cargos = cargoRetorno;
        },
        (error: any) =>
        {
          this.toastr.error("Não foi possível carregar os cargos", "Erro!");
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }

 
  public onFileChange(ev: any): void {
    
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemUpload = event.target.result;
    
    this.file = ev.target.files;

    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {

    this.spinner.show();
    this.funcionarioService.postUpload(this.funcionarioId, this.file).subscribe(
      () => {
  //      this.router.navigate([`funcionarios/detalhe/${this.funcionarioId}`]);
        location.reload();
        this.toastr.success("Foto Atualizada!", "Sucesso!");  
      },
      (error: any) => {
        this.toastr.error('Falha ao atualziar foto', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }
}


