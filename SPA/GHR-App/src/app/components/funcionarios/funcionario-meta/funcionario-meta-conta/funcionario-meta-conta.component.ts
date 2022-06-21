import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';

import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';

@Component({
  selector: 'app-funcionario-meta-conta',
  templateUrl: './funcionario-meta-conta.component.html',
  styleUrls: ['./funcionario-meta-conta.component.scss']
})
export class FuncionarioMetaContaComponent implements OnInit {

  public form: FormGroup;

  public funcionario = {} as Funcionario;

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private funcionarioService: FuncionarioService) { }

  ngOnInit(): void
  {
    this.spinner.show();
    this.validarFormulario();
    this.consultarFuncionario();
  }

  public validarFormulario(): void {
    this.form = this.fb.group(       {
      id: [''],
      nomeCompleto: [''],
      nomeDepartamento: [''],
      nomeCargo: [''],
      supervisor: [''],
      gerente: [''],
      diretor: [''],
      dataAdmissao: [''],
      dataDemissao: [''],
      phoneNumber: [''],
    });
  }

  public consultarFuncionario(): void {

    const funcionarioIdParam = this.activatedRouter.snapshot.paramMap.get('id');
    if (funcionarioIdParam !== null) {
      this.spinner.show();
      this.funcionarioService
        .recuperarFuncionarioPorId(+funcionarioIdParam)
        .subscribe(
          (funcionario: Funcionario) => {
            this.funcionario = funcionario;
            this.form.patchValue(this.funcionario);
            this.form.patchValue(this.funcionario.contas);
            this.form.patchValue(this.funcionario.cargos)
            this.form.patchValue(this.funcionario.departamentos)
          },
          (error: any) => {
            this.toastr.error("Não foi possível carregar funcionarios", "Erro!");
            console.error(error);}      )
        .add(() => this.spinner.hide());
    };

  }

}

