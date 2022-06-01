import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { NgxCurrencyModule } from 'ngx-currency';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { CadastroComponent } from './components/conta/cadastro/cadastro.component';
import { CargoDetalheComponent } from './components/cargos/cargo-detalhe/cargo-detalhe.component';
import { CargoListaComponent } from './components/cargos/cargo-lista/cargo-lista.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { ContaComponent } from './components/conta/conta.component';
import { DepartamentoDetalheComponent } from './components/departamentos/departamento-detalhe/departamento-detalhe.component';
import { DepartamentoListaComponent } from './components/departamentos/departamento-lista/departamento-lista.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { FuncionarioContaComponent } from './components/funcionarios/funcionario-detalhe/funcionario-conta/funcionario-conta.component';
import { FuncionarioDadosPessoaisComponent } from './components/funcionarios/funcionario-detalhe/funcionario-dados-pessoais/funcionario-dados-pessoais.component';
import { FuncionarioDetalheComponent } from './components/funcionarios/funcionario-detalhe/funcionario-detalhe.component';
import { FuncionarioEmpresaComponent } from './components/funcionarios/funcionario-detalhe/funcionario-empresa/funcionario-empresa.component';
import { FuncionarioEnderecoComponent } from './components/funcionarios/funcionario-detalhe/funcionario-endereco/funcionario-endereco.component';
import { FuncionarioListaComponent } from './components/funcionarios/funcionario-lista/funcionario-lista.component';
import { FuncionarioMetaComponent } from './components/funcionarios/funcionario-meta/funcionario-meta.component';
import { FuncionarioMetaAssociarComponent } from './components/funcionarios/funcionario-meta/funcionario-meta-associar/funcionario-meta-associar.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/conta/login/login.component';
import { MetaDetalheComponent } from './components/metas/meta-Detalhe/meta-Detalhe.component';
import { MetaListaComponent } from './components/metas/meta-lista/meta-lista.component';
import { MetasComponent } from './components/metas/metas.component';
import { NavComponent } from './shared/nav/nav.component';
import { PerfilComponent } from './components/conta/perfil/perfil.component';
import { PerfilDetalheComponent } from './components/conta/perfil/perfil-detalhe/perfil-detalhe.component';
import { TituloComponent } from './shared/titulo/titulo.component';


import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';
import { DateFormatPipe } from './helpers/DateFormat.pipe';

import { CargoService } from './services/cargos/Cargo.service';
import { ContaService } from './services/contas/Conta.service';
import { DadosPessoaisService } from './services/funcionarios/dadosPessoais.service';
import { DepartamentoService } from './services/departamentos/departamento.service';
import { EnderecoService } from './services/funcionarios/endereco.service';
import { FuncionarioService } from './services/funcionarios/funcionario.service';

import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { MetaService } from './services/metas/Meta.service';
import { FuncionarioMetaService } from './services/funcionarios/funcionarioMeta.service';
import { PerfilSenhaComponent } from './components/conta/perfil/perfil-senha/perfil-senha.component';
import { FuncionarioMetaContaComponent } from './components/funcionarios/funcionario-meta/funcionario-meta-conta/funcionario-meta-conta.component';
import { FuncionarioMetaListaComponent } from './components/funcionarios/funcionario-meta/funcionario-meta-lista/funcionario-meta-lista.component';
import { FuncionarioDetalheMetaComponent } from './components/funcionarios/funcionario-detalhe-meta/funcionario-detalhe-meta.component';

defineLocale('pt-br', ptBrLocale);
@NgModule({
  declarations: [
    AppComponent,
    CadastroComponent,
    CargoDetalheComponent,
    CargoListaComponent,
    CargosComponent,
    ContaComponent,
    DateFormatPipe,
    DateTimeFormatPipe,
    DepartamentoDetalheComponent,
    DepartamentoListaComponent,
    DepartamentosComponent,
    FuncionarioContaComponent,
    FuncionarioDetalheComponent,
    FuncionarioDetalheMetaComponent,
    FuncionarioEmpresaComponent,
    FuncionarioEnderecoComponent,
    FuncionarioListaComponent,
    FuncionarioMetaAssociarComponent,
    FuncionarioMetaComponent,
    FuncionarioMetaContaComponent,
    FuncionarioMetaListaComponent,
    FuncionariosComponent,
    HomeComponent,
    LoginComponent,
    PerfilComponent,
    PerfilDetalheComponent,
    MetaDetalheComponent,
    MetaListaComponent,
    MetasComponent,
    NavComponent,
    PerfilComponent,
    PerfilSenhaComponent,
    TituloComponent,
    FuncionarioDadosPessoaisComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    NgxCurrencyModule,
    NgxSpinnerModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    TooltipModule.forRoot(),
  ],
  providers: [
    CargoService,
    ContaService,
    DadosPessoaisService,
    DepartamentoService,
    EnderecoService,
    FuncionarioService,
    FuncionarioMetaService,
    MetaService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

})
export class AppModule { }
