import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../services/auth/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    private username: string = "";
    private password: string = "";

    private invalidLogin: boolean = false;

    constructor(private authService: AuthService, private router: Router){}

    public loginUser() {
        let loginResult = this.authService.login(this.username, this.password);
        loginResult.subscribe((val) => {
            if (val === true) {
                this.invalidLogin = false;
                this.router.navigate(['calendar']);
            } else {
                this.invalidLogin = true;
            }
        });
    }

    ngOnInit() {
    }

}
