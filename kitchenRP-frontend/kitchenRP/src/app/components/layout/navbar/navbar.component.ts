import {AuthService} from "../../../services/auth/auth.service";

﻿import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {User} from "../../../types/user";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  currentUser: Observable<User>;
  constructor(private authService: AuthService) {
    this.currentUser = this.authService.currentUser$;
  }

  ngOnInit() {
  }

    toggleSidebar() {
        document.getElementById("sidebar").classList.toggle("sidebar-collapsed");
        document.getElementById("content-wrapper").classList.toggle("sidebar-collapsed");
    }
}
