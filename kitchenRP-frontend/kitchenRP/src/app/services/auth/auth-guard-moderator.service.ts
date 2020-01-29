import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {AuthService} from "./auth.service";
import {User} from "../../types/user";
import {tap} from "rxjs/operators";

@Injectable()
export class AuthGuardModerator implements CanActivate {
    constructor(private router: Router, private authService: AuthService) {
    }

    canActivate(route: ActivatedRouteSnapshot,
                state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return this.authService.isModerator()
            .pipe(
                tap(
                    isUser => {
                        // console.log("is not a moderator");
                        if(!isUser) this.router.navigate(["login"]).then();
                    }
                )
            )
    }
}
