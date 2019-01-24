import { NgModule } from '@angular/core';
import {
    AuthModule,
    AUTH_SERVICE,
    PUBLIC_FALLBACK_PAGE_URI,
    PROTECTED_FALLBACK_PAGE_URI
} from 'ngx-auth';

import {
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule
  } from '@angular/material';

import { TokenService } from './token.service';
import { AuthenticationService } from './authentication.service';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SpinnerButtonModule } from '../components/partials/spinner-button/spinner-button.module';
import { LoginComponent } from './components/login/login.component';

export function factory(authenticationService: AuthenticationService) {
    return authenticationService;
}

@NgModule({
    declarations: [
        LoginComponent
    ],
    imports: [
        AuthModule,
        FormsModule,
        SpinnerButtonModule,
        TranslateModule.forChild(),

        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatCheckboxModule
    ],
    providers: [
        TokenService,
        AuthenticationService,
        { provide: PROTECTED_FALLBACK_PAGE_URI, useValue: '/' },
        { provide: PUBLIC_FALLBACK_PAGE_URI, useValue: '/login' },
        {
            provide: AUTH_SERVICE,
            deps: [AuthenticationService],
            useFactory: factory
        }
    ]
})
export class AuthenticationModule {

}
