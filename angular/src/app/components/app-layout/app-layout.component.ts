import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

import { HostBinding } from '@angular/core';
import { AuthService, ConfigStateService } from '@abp/ng.core';
import { NgIf } from '@angular/common';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './app-layout.component.html',
  imports: [RouterOutlet, RouterLink, NgIf],
})
export class AppLayoutComponent {
  currentUser: any;

  constructor(private config: ConfigStateService,private authService:AuthService) {
    const currentUser = this.config.getOne('currentUser');
    this.currentUser = currentUser;
  }
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  handleLogin():void{
     this.authService.navigateToLogin();
  }

  handleLogout():void{
    this.authService.logout();
  }
}
