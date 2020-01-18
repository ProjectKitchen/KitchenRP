import { Component, OnInit } from '@angular/core';
import {LoginService} from '../../services/auth/login.service';
import {AuthService} from "../../services/auth/auth.service";
import {log} from "util";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService){}

  loginUser(){
      this.authService.login( "if17b094", "********")
          .subscribe(() => console.log("works"));
  }

  ngOnInit() {
  }

}
