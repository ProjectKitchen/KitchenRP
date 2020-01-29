import {Injectable, OnInit} from "@angular/core";
import {LoginService} from "./login.service";
import {Observable, of, ReplaySubject, Subject} from "rxjs";
import {catchError, map, mapTo, tap} from "rxjs/operators";
import {Token} from "./user-auth";
import {User} from "../../types/user";
import {UserService} from "../user/user.service";


@Injectable({
    providedIn: 'root'
})
export class AuthService implements  OnInit {
    private readonly ACCESS_TOKEN_KEY = "ACCESS_TOKEN";
    private readonly REFRESH_TOKEN_KEY = "REFRESH_TOKEN";

    private loggedInUser: string;
    private currentUser: Subject<User> = new ReplaySubject(1);
    public currentUser$: Observable<User> = this.currentUser.asObservable();

    public constructor(private loginService: LoginService, private userService: UserService) {

    }

    ngOnInit(): void {
        if(this.getAccessToken()){
            let token = this.getAccessToken();
            let [head, body, sig] = token.split('.');
            let payload = JSON.parse(atob(body));

            let user = payload.sub;
            this.userService.getByName(user)
                .subscribe(u => this.currentUser.next(u));
        }
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

    public isAnyUser(): Observable<boolean> {
        return  this.currentUser$.pipe(
            tap(u => console.log(u)),
            map((val) => val !== null && (val.role === 'user' || val.role === 'moderator' || val.role === 'admin')));
    }

    public userScope(): Observable<string> {
        return this.currentUser$
            .pipe(
                map(u => u.role)
            )
    }

    public isUser(): Observable<boolean> {
      return this.userScope().pipe(
          map(scope => scope === "user" || scope === "moderator" || scope === "admin")
      )
    }
    public isAdmin(): Observable<boolean> {
        return this.userScope().pipe(
            map(scope => scope === "admin")
        )
    }

    public isModerator(): Observable<boolean> {
        return this.userScope().pipe(
            map(scope => scope === "moderator" || scope === "admin")
        )
    }

    public getUsername() {
        return this.loggedInUser;
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
        this.userService.getByName(username).subscribe((user) => this.currentUser.next(user));
        this.storeTokens(tokens);
    }
}
