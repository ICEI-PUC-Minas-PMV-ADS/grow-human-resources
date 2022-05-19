import { AuthGuard } from './guard/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CadastroComponent } from './components/conta/cadastro/cadastro.component';
import { CargosComponent } from './components/cargos/cargos.component';
import { DepartamentosComponent } from './components/departamentos/departamentos.component';
import { FuncionarioDetalheComponent } from './components/funcionarios/funcionario-detalhe/funcionario-detalhe.component';
import { FuncionarioListaComponent } from './components/funcionarios/funcionario-lista/funcionario-lista.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { LoginComponent } from './components/conta/login/login.component';
import { MetasComponent } from './components/metas/metas.component';
import { PerfilComponent } from './components/conta/perfil/perfil.component';
import { DepartamentoDetalheComponent } from './components/departamentos/departamento-detalhe/departamento-detalhe.component';
import { DepartamentoListaComponent } from './components/departamentos/departamento-lista/departamento-lista.component';
import { MetaListaComponent } from './components/metas/meta-lista/meta-lista.component';
import { MetaDetalheComponent } from './components/metas/meta-Detalhe/meta-Detalhe.component';
import { CargoDetalheComponent } from './components/cargos/cargo-detalhe/cargo-detalhe.component';
import { CargoListaComponent } from './components/cargos/cargo-lista/cargo-lista.component';
import { FuncionarioMetaComponent } from './components/funcionarios/funcionario-meta/funcionario-meta.component';
import { HomeComponent } from './components/home/home.component';
import { ContaComponent } from './components/conta/conta.component';


const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },

  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [

      { path: 'conta', redirectTo: 'conta/perfil', pathMatch: 'full' },
      { path: 'conta/perfil', component: PerfilComponent },

      { path: 'funcionarios', redirectTo: 'funcionarios/lista', pathMatch: 'full' },
      { path: 'funcionarios', component: FuncionariosComponent,
        children: [
          { path: 'detalhe/:id', component: FuncionarioDetalheComponent },
          { path: 'detalhe', component: FuncionarioDetalheComponent },
          { path: 'lista', component: FuncionarioListaComponent },
          { path: 'meta/:id', component: FuncionarioMetaComponent }
        ]
      },

      { path: 'departamentos', redirectTo: 'departamentos/lista', pathMatch: 'full' },
      { path: 'departamentos', component: DepartamentosComponent,
        children: [
          { path: 'detalhe/:id', component: DepartamentoDetalheComponent },
          { path: 'detalhe', component: DepartamentoDetalheComponent },
          { path: 'lista', component: DepartamentoListaComponent }
        ]
      },

      { path: 'metas', redirectTo: 'metas/lista', pathMatch: 'full' },
      { path: 'metas', component: MetasComponent,
        children: [
          { path: 'detalhe/:id', component: MetaDetalheComponent },
          { path: 'detalhe', component: MetaDetalheComponent },
          { path: 'lista', component: MetaListaComponent }
        ]
      },

      { path: 'cargos', redirectTo: 'cargos/lista', pathMatch: 'full' },
      { path: 'cargos', component: CargosComponent,
        children: [
          { path: 'detalhe/:id', component: CargoDetalheComponent },
          { path: 'detalhe', component: CargoDetalheComponent },
          { path: 'lista', component: CargoListaComponent }
        ]
      },
    ]
  },

  { path: 'conta', component: ContaComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: CadastroComponent }
    ]
  },

  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
