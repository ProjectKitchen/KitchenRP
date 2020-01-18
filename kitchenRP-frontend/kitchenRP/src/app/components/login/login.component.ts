import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../services/auth/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    private username: string = "";
    private password: string = "";

    constructor(private authService: AuthService){}

    public loginUser() {
        console.log(this.authService.isLoggedIn());

        let loginResult = this.authService.login(this.username, this.password);
        loginResult.subscribe((val) => console.log("result " + val));

        console.log(this.authService.isLoggedIn());
    }

    ngOnInit() {
    }

}
