import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: "root",
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    console.log("CanActivate called");
    const currentUser = this.authService.currentUserValue;
    if (currentUser && currentUser.authToken) {
      // Check if user has permission to access the route
      return true;
    }
    // not logged in so redirect to login page with the return url
    this.router.navigate(['/']);
    return false;

  }
}
