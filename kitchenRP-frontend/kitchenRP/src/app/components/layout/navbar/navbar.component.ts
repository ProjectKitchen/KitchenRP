﻿import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

    toggleSidebar() {
        document.getElementById("sidebar").classList.toggle("sidebar-collapsed");
        document.getElementById("content-wrapper").classList.toggle("sidebar-collapsed");
    }
}
