import {Injectable} from '@angular/core';
import {Token, UserAuth, Jwt} from "./user-auth";
import {BehaviorSubject, Observable, of, ReplaySubject, Subject} from "rxjs";
import {LoginService} from "./login.service";
import {catchError, map, mapTo, tap} from "rxjs/operators";
import {User} from "../../types/user";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly ACCESS_TOKEN_KEY = "ACCESS_TOKEN";
    private readonly REFRESH_TOKEN_KEY = "REFRESH_TOKEN";

    private loggedInUser: string;

    public constructor(private loginService: LoginService) {

    }


    public login(username: string, password: string): Observable<boolean> {
        return this.loginService.login({username, password})
            .pipe(
                tap(tokens => this.doLoginUser(username, tokens)),
                mapTo(true),
                catchError(err => {
                    console.log(err);
                    return of(false)
                })
            );
    }


    public logout() {
        return this.loginService.logout(this.getRefreshToken())
            .pipe(
                tap(() => this.doLogoutUser()),
                mapTo(true),
                catchError((err) => {
                    console.log(err);
                    return of(false);
                })
            );
    }

    public refreshToken() {
        return this.loginService.refresh(this.getRefreshToken())
            .pipe(
                tap(tokens => this.storeTokens(tokens)),
            );
    }


    private storeTokens(tokens: Token) {
        localStorage.setItem(this.ACCESS_TOKEN_KEY, tokens.accessToken);
        localStorage.setItem(this.REFRESH_TOKEN_KEY, tokens.refreshToken);
    }

    isLoggedIn() {
        return !!this.getAccessToken();
    }

    public getRefreshToken() {
        return localStorage.getItem(this.REFRESH_TOKEN_KEY);
    }

    public getAccessToken() {
        return localStorage.getItem(this.ACCESS_TOKEN_KEY);
    }

    private removeTokens() {
        localStorage.removeItem(this.ACCESS_TOKEN_KEY);
        localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    }

    private doLogoutUser() {
        this.loggedInUser = null;
        this.removeTokens();
    }

    private doLoginUser(username: string, tokens: Token) {
        this.loggedInUser = username;
        this.storeTokens(tokens);
    }
}
