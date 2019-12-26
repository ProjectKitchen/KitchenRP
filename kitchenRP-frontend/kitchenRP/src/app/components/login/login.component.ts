import { Component, OnInit } from '@angular/core';
import {LoginService} from '../../services/auth/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService){}

  loginUser(){
      var testToken = this.loginService.login({username: "if17b029", password: "test"});
      testToken.subscribe(res => console.log(res));
  }

  ngOnInit() {
  }

}
