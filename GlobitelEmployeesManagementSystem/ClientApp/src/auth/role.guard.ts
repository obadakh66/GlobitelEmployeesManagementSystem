import { environment } from './../environments/environment.prod';
import { Injectable } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot
} from '@angular/router';
import decode from 'jwt-decode';
import { AuthorizeService } from './authorize.service';
@Injectable()
export class RoleGuard implements CanActivate {

  constructor(public auth: AuthorizeService, public router: Router) {}
  canActivate(route: ActivatedRouteSnapshot): boolean {
    // this will be passed from the route config
    // on the data property
    const expectedRole = route.data.expectedRole;
    const token = localStorage.getItem(environment.token);
    // decode the token to get its payload
    if(token){
      var tokenPayload = decode(token);
    }
    else {
      this.auth.logout();
      this.router.navigate(['/auth/login']);
      return false;
    }
    console.log((tokenPayload as any)[environment.roleClaim]);
    console.log(expectedRole);
    if (
        !this.auth.isAuthenticated() ||
      Array.isArray((tokenPayload as any)[environment.roleClaim])
      ? !((tokenPayload as any)[environment.roleClaim].find(x => x === expectedRole))
      :  (tokenPayload as any)[environment.roleClaim] !== expectedRole
    ) {
      console.log("Unauthorized");
      this.auth.logout();
      this.router.navigate(['/auth/login']);
      return false;
    }
    return true;
  }
}
