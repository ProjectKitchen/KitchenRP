import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {BehaviorSubject, Observable, throwError} from "rxjs";
import {AuthService} from "./services/auth/auth.service";
import {catchError, filter, switchMap, take} from "rxjs/operators";
import {Token} from "./services/auth/user-auth";
import {Injectable} from "@angular/core";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService) {
    }


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.authService.getAccessToken()) {
            req = this.addToken(req, this.authService.getAccessToken())
        }

        return next.handle(req).pipe(
            catchError(error => {
            if (error instanceof HttpErrorResponse && error.status === 401) {
                return this.handleRefreshError(req, next);
            } else {
                return throwError(error);
            }
        }));
    }


    private addToken(req: HttpRequest<any>, token: string) {
        return req.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        })
    }


    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    private handleRefreshError(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            return this.authService.refreshToken().pipe(
                switchMap((token: Token) => {
                    this.isRefreshing = false;
                    this.refreshTokenSubject.next(token.accessToken);
                    return next.handle(this.addToken(request, token.accessToken));
                }));
        } else {
            return this.refreshTokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(jwt => {
                    return next.handle(this.addToken(request, jwt));
                }));
        }
    }
}
