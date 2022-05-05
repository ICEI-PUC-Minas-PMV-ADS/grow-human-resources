import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { LoginComponent } from './components/login/login.component';
import { MetasComponent } from './components/metas/metas.component';
import { SupervisoresComponent } from './components/supervisores/supervisores.component';


const routes: Routes = [
  { path: 'cargos', component: CargosComponent },
  { path: 'departamentos', component: DepartamentosComponent },
  { path: 'funcionarios', component: FuncionariosComponent },
  { path: 'login', component: LoginComponent },
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
