import { ContaService } from 'src/app/services/contas/Conta.service';
import { Paginacao, ResultadoPaginacao } from '../../../models/suporte/paginacao/paginacao';
import { ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ValidadorFormularios } from 'src/app/helpers/ValidadorFormularios';

import { Funcionario } from 'src/app/models/funcionarios/Funcionario';
import { FuncionarioMeta } from 'src/app/models/funcionarios/FuncionarioMeta';
import { Meta } from 'src/app/models/metas/Meta';

import { FuncionarioService } from 'src/app/services/funcionarios/funcionario.service';
import { FuncionarioMetaService } from 'src/app/services/funcionarios/funcionarioMeta.service';
import { MetaService } from 'src/app/services/metas/Meta.service';
import { NavegacaoEntreForms } from 'src/app/models/suporte/navegacaoEntreForms/navegacaoEntreForms';
import { ContaAtiva } from 'src/app/models/contas/ContaAtiva';

@Component({
  selector: 'app-funcionario-meta',
  templateUrl: './funcionario-meta.component.html',
  styleUrls: ['./funcionario-meta.component.scss']
})
export class FuncionarioMetaComponent implements OnInit {
  public formsParametros = {} as NavegacaoEntreForms;

  public prosseguir: boolean;

  public contaAtiva = {} as ContaAtiva;

  public visaoRH = false;

  public get mostrarAbas(): boolean {

    return (this.visaoRH);
  }

  constructor(
    private router: Router,
    private contaService: ContaService,
  ) {
      router.events.subscribe(
        (verificaContaAtiva) => {
          if (verificaContaAtiva instanceof NavigationEnd) {
            this.contaService
              .contaAtual$
              .subscribe(
                (value) => {
                  this.contaAtiva = { ...value };
                  this.visaoRH = this.contaAtiva.visao?.includes('RH');})
      }})
     }

  ngOnInit(): void {

  }

}

