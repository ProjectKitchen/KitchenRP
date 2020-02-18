import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {AuthService} from "./auth.service";
import {User} from "../../types/user";
import {map, tap} from "rxjs/operators";

@Injectable()
export class AuthGuardUser implements CanActivate {

    constructor(private router: Router, private authService: AuthService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        return this.authService.isAnyUser()
            .pipe(
                tap(
                    isUser => {
                        // console.log("is not a user");
                        if(!isUser) this.router.navigate(["login"]).then();
                    }
                )
            )
    }
}
