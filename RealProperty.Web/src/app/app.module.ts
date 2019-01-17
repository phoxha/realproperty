import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutComponent } from './layout/app-layout/app-layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AppAuthComponent } from './layout/app-auth/app-auth.component';
import { LoginComponent } from './components/auth/login/login.component';
import { LocalStorageService } from './providers/local-storage.service';
import { AuthGuardProvider } from './providers/guards/auth-guard.provider';

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    DashboardComponent,
    AppAuthComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    AuthGuardProvider,
    LocalStorageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
