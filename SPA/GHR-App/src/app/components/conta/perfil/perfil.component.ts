import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Conta } from 'src/app/models/contas/Conta';

import { ContaService } from 'src/app/services/contas/Conta.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public conta = {} as Conta;

  public imagemURL = '../../../../assets/img/semImagem.jfif';

  public file: File;

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private contaService: ContaService,
    private funcionarioService: FuncionarioService,
    private router: Router
  ) {}

  ngOnInit() {

  }

  public recuperarValorForm(conta: Conta): void {

    this.conta = conta;
    this.imagemURL = (conta.imagemURL !== '' && conta.imagemURL !== null)
      ? environment.apiURL + 'recursos/fotos/' + conta.imagemURL
      : "../../../../assets/img/semImagem.jfif";
  }


  public alterarImagem(event: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = event.target.files;

    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  private uploadImagem(): void {
    this.spinner.show();

    this.contaService
      .salvarImagem(this.file)
      .subscribe(
        () => this.toastr.success("Imagem atualizada com sucesso!", "Sucesso!"),

        (error: any) => {
          this.toastr.error("Falha ao fazer upload da imagem.", "Erro!");
          console.error(error);})

      .add(() => this.spinner.hide());
   }
}
