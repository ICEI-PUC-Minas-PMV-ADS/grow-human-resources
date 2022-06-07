import { Component, OnInit } from '@angular/core';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ContaVisao } from 'src/app/models/contas/ContaVisao';

import { ContaService } from 'src/app/services/contas/Conta.service';
import { environment } from 'src/environments/environment';
import { Conta } from 'src/app/models/contas/Conta';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public contaVisao = {} as ContaVisao;


  public imagemURL = '../../../../assets/img/semImagem.jfif';

  public file: File;

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private contaService: ContaService) {

  }

  ngOnInit() {
    this.carregarContaAtiva();
  }

  public recuperarValorForm(contaVisao: ContaVisao): void {

    this.contaVisao = contaVisao;
  }

  public carregarContaAtiva(): void {

    this.contaService
      .recuperarContaAtiva()
      .subscribe(
        (conta: Conta) => {
          this.contaVisao = { ...conta };
          this.imagemURL = (conta.imagemURL !== '' && conta.imagemURL !== null)
            ? environment.apiURL + 'recursos/fotos/' + conta.imagemURL
            : "../../../../assets/img/semImagem.jfif";
        })
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
          console.error(error);
        })

      .add(() => this.spinner.hide());
   }
}
