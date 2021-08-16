import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import decode from 'jwt-decode';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(public jwtHelper: JwtHelperService, private router: Router, private spinner: NgxSpinnerService) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(environment.token);
    // Check whether the token is expired and return
    // true or false
    if (token) {
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }

  /**
   * setToken
   */
  // tslint:disable-next-line: typedef
  public setToken(token: string) {
    localStorage.setItem(environment.token, token);
  }

  public logout() {
    localStorage.removeItem(environment.token);
    this.router.navigate(['/auth/login']);
    this.spinner.hide();
  }

  getUserName() {
    const token = localStorage.getItem(environment.token);
    // decode the token to get its payload
    if (token) {
      var tokenPayload = decode(token);
    }
    return ((tokenPayload as any)[environment.userName])
  }

  getUserRoles() {
    const token = localStorage.getItem(environment.token);
    // decode the token to get its payload
    if (token) {
      var tokenPayload = decode(token);

      return ((tokenPayload as any)[environment.roleClaim])
    }
    return null;
  }
  get loggedInUser(): string {
    const token = localStorage.getItem(environment.token);
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return token;
    }
    return null;
  }
}
