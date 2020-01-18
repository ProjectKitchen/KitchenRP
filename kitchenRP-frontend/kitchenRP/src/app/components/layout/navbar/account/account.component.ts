import {Component, OnInit} from "@angular/core";
import {AuthService} from "../../../../services/auth/auth.service";
import {Observable} from "rxjs";
import {User} from "../../../../types/user";

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

    currentUser: Observable<User>;
    constructor(private authService: AuthService) {
        this.currentUser = this.authService.currentUser$;
    }

    ngOnInit() {
    }

    private async logoutUser() {
        console.log(this.authService.isLoggedIn());
        let user = await this.authService.currentUser$.toPromise();
        console.log(this.authService.isLoggedIn());
    }

}
