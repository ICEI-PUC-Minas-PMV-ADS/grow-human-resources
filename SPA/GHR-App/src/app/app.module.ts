import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { CadastroComponent } from './components/user/cadastro/cadastro.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { FuncionarioDetalheComponent } from './components/funcionarios/funcionario-detalhe/funcionario-detalhe.component';
import { FuncionarioListaComponent } from './components/funcionarios/funcionario-lista/funcionario-lista.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { LoginComponent } from './components/user/login/login.component';
import { MetasComponent } from './components/metas/metas.component';
import { NavComponent } from './shared/nav/nav.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { SupervisoresComponent } from './components/supervisores/supervisores.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { UserComponent } from './components/user/user.component';

import { FuncionarioService } from './services/funcionario.service';

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';

@NgModule({
  declarations: [
    AppComponent,
    CadastroComponent,
    CargosComponent,
    DateTimeFormatPipe,
    DepartamentosComponent,
    FuncionarioDetalheComponent,
    FuncionarioListaComponent,
    FuncionariosComponent,
    LoginComponent,
    PerfilComponent,
    MetasComponent,
    NavComponent,
    PerfilComponent,
    SupervisoresComponent,
    TituloComponent,
    UserComponent
   ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    NgxSpinnerModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    TooltipModule.forRoot(),
  ],
  providers: [FuncionarioService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

})
export class AppModule { }
