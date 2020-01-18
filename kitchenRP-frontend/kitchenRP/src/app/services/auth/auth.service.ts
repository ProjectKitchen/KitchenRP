import { Injectable } from '@angular/core';
import {Token, UserAuth, Jwt} from "./user-auth";
import {BehaviorSubject, Observable, ReplaySubject, Subject} from "rxjs";
import {LoginService} from "./login.service";
import {map} from "rxjs/operators";
import {User} from "../../types/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    private accessToken: string = null;
    private refreshToken: string = null;

    get accessJwt(): Jwt {
        return this.jwtFromString(JSON.parse(this.accessToken));
    }

    get refreshJwt(): Jwt {
        return this.jwtFromString(JSON.parse(this.refreshToken));
    }

    // wer, wie lange, welche rolle, welche id, userobject (name, email, email_notifications)
    get username(): string {
        return this.accessJwt.body.sub;
    }

    get role(): string {
        return this.accessJwt.body.role;
    }

    get isLoggedIn(): boolean {
        return this.accessJwt != null;
    }

    get isAdmin(): boolean {
        return null;
    }

    get userObject(): boolean {
        return null;
    }

    constructor(private loginService: LoginService) {
        this.accessToken = localStorage.getItem("accessToken") || null;
        this.refreshToken = localStorage.getItem("refreshToken") || null;

        // let userObject: Observable<User> = this.userService.get(this.username);
    }

    jwtFromString(s: string) : Jwt {
        let split = s.split('.');
        let header = JSON.parse(atob(split[0]));
        let body = JSON.parse(atob(split[1]));
        let expires = body.exp;

        return {header, body, expires};
    }

    updateFromToken(token: Token) {
        this.accessToken = token.accessToken;
        this.refreshToken = token.refreshToken;
    }

    updateAuth(userAuth: UserAuth, invalidate: boolean = false) {
        if(invalidate) {
            this.accessToken = null;
            this.refreshToken = null;
        }
        this.loginService.login(userAuth)
            .subscribe((token) => {
                this.updateFromToken(token);
            });
    }

    refresh() {
        this.loginService.refresh(this.refreshToken)
            .subscribe((token) => {
                this.updateFromToken(token);
            });
    }
}
