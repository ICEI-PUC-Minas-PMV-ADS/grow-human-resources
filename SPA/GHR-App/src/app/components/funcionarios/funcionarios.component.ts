import { FuncionarioService } from '../../services/funcionario.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Funcionario } from '../../models/Funcionario';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-funcionarios',
  templateUrl: './funcionarios.component.html',
  styleUrls: ['./funcionarios.component.scss']
  //,providers: [FuncionarioService]
})
export class FuncionariosComponent implements OnInit {

  constructor() { }

  public ngOnInit(): void {
  }
}
