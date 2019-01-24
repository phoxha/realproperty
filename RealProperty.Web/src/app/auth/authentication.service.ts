
import { Injectable } from '@angular/core';
import { AuthService } from 'ngx-auth';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { TokenService } from './token.service';
import { Observable } from 'rxjs';
import { TokenModel } from './models/token.model';
import { LoginModel } from './models/login.model';
import { environment } from 'src/environments/environment';
import { tap, map } from 'rxjs/operators';
import { ApiResponse } from '../models/common/api-response.model';

@Injectable()
export class AuthenticationService implements AuthService {
    private interruptedUrl: string;

    constructor(
        private http: HttpClient,
        private tokenService: TokenService
    ) {}

    public isAuthorized(): Observable<boolean> {
        return this.tokenService.getAccessToken().pipe(map(token => !!token));
    }

    public getAccessToken(): Observable<string> {
        return this.tokenService.getAccessToken();
    }

    public refreshToken(): Observable<TokenModel> {
        throw new Error('Not Implemented');
    }

    public refreshShouldHappen(response: HttpErrorResponse): boolean {
        return response.status === 401;
    }

    public verifyTokenRequest(url: string): boolean {
        return url.endsWith('/refresh');
    }

    public getInterruptedUrl(): string {
        return this.interruptedUrl;
    }

    public setInterruptedUrl(url: string): void {
        this.interruptedUrl = url;
    }

    /**
     * EXTRA AUTH METHODS
     */

     public login(model: LoginModel): Observable<any> {
         const url = `${environment.apiUrl}/auth/login`;
         return this.http.post(url, model)
            .pipe(tap((response: ApiResponse<TokenModel>) => this.saveToken(response.model)));
     }

     public logout(): void {
         this.tokenService.clear();
     }

     private saveToken(token: TokenModel) {
        this.tokenService
            .setAccessToken(token.accessToken)
            .setRefreshToken(token.refreshToken);
     }
}
