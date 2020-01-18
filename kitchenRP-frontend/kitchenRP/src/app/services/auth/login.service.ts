import {Inject, Injectable} from '@angular/core';
import {Token, UserAuth} from './user-auth';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/internal/Observable';

@Injectable({
    providedIn: 'root'
})
export class LoginService {

    constructor(private http: HttpClient, @Inject('API_BASE_URL') private baseUrl: string) {}

    public login(auth: UserAuth): Observable<Token> {
        console.log('sending user token request');
        return this.http.post<Token>(this.baseUrl + '/token', auth);
    }

    public refresh(refreshToken: string) {
        return this.http.post<Token>(this.baseUrl + '/token/refresh', {refreshToken:refreshToken});
    }

    public logout(refreshToken: string) {
        return this.http.request<any>('delete',this.baseUrl + '/token/refresh', {body: {token: refreshToken}});
    }
}
