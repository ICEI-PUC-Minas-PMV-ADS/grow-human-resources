import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CadastroComponent } from './components/user/cadastro/cadastro.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { FuncionarioDetalheComponent } from './components/funcionarios/funcionario-detalhe/funcionario-detalhe.component';
import { FuncionarioListaComponent } from './components/funcionarios/funcionario-lista/funcionario-lista.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { LoginComponent } from './components/user/login/login.component';
import { MetasComponent } from './components/metas/metas.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { SupervisoresComponent } from './components/supervisores/supervisores.component';
import { UserComponent } from './components/user/user.component';


const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: CadastroComponent }
    ]
  },

  {
    path: 'user/perfil', component: PerfilComponent
  },

  { path: 'funcionarios', redirectTo: 'funcionarios/lista', pathMatch: 'full' },

  {
    path: 'funcionarios', component: FuncionariosComponent,
    children: [
      { path: 'detalhe/:id', component: FuncionarioDetalheComponent },
      { path: 'detalhe', component: FuncionarioDetalheComponent },
      { path: 'lista', component: FuncionarioListaComponent }
    ]
  },

  { path: 'cargos', component: CargosComponent },
  { path: 'departamentos', component: DepartamentosComponent },
  { path: 'metas', component: MetasComponent },
  { path: 'supervisores', component: SupervisoresComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }