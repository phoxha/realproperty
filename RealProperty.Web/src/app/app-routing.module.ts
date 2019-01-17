import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppLayoutComponent } from './layout/app-layout/app-layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuardProvider } from './providers/guards/auth-guard.provider';
import { AppAuthComponent } from './layout/app-auth/app-auth.component';
import { LoginComponent } from './components/auth/login/login.component';

const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      { path: '', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthGuardProvider] }
    ]
  },

  {
    path: '',
    component: AppAuthComponent,
    children: [
      { path: 'login', component: LoginComponent, pathMatch: 'full', data: { title: 'Archysoft' }}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
