import { Injectable } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import { BehaviorSubject, catchError, map } from "rxjs";
import { CurrentUser, User } from "../demo/components/auth/user.model";
import { AppConfigService } from "../demo/service/appconfigservice ";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "../demo/service/base.service";

@Injectable({
  providedIn: "root",
})
export class AuthService extends BaseService {
  public loggedIn = new BehaviorSubject<boolean>(this.tokenAvailable());
  private currentUserSubject: BehaviorSubject<CurrentUser>;
  private curusername = new BehaviorSubject<string>(this.setUsername());
  private userOrganization = new BehaviorSubject<string>(this.setUsername());
  gnBaseURL: any;
  constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,) {
    super();

    this.currentUserSubject = new BehaviorSubject<CurrentUser>(
      JSON.parse(sessionStorage.getItem('currentUser') || '{}'));

  }
  public get currentUserValue(): CurrentUser {
    return this.currentUserSubject.value;
  }
  private setUsername(): string {
    if (this.loggedIn.value) {
      var uu = JSON.parse(sessionStorage.getItem('currentUser')!);
      if (uu != null) {

        let UName = uu.firstName;
        if (UName == null) {
          return ""
        }
        else {
          return uu.firstName;
        }
      }
      else {
        return "";
      }
    }
    else {
      return ""
    }
  }

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  // Login
  login(UserName: string, Password: string) {
    this.gnBaseURL = this.appURL.getServerUrl();
    return this.http
      .post<any>(this.gnBaseURL + "Auth", { UserName, Password })
      .pipe(
        map(user => {
          this.setCurrentUser(user);
          return user.data;
        })
      )
      .pipe(catchError(this.handleError));
  }


  reset(UserName: string) {
    this.gnBaseURL = this.appURL.getServerUrl();
    return this.http
      .post<any>(this.gnBaseURL + "Auth/ResendEmamil", { UserName })      
      .pipe(catchError(this.handleError));
  }

  get getcurrentusername() {
    return this.curusername.asObservable();
  }

  get getOrganizationName() {
    return this.userOrganization.asObservable();
  }

  // Set current login user details
  setCurrentUser(user: CurrentUser) {
    // store user details and jwt token in session storage to keep user logged in between page refreshes
    sessionStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }
  private tokenAvailable(): boolean {
    return !!sessionStorage.getItem('token');
  }
  isAuthenticated() {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        if (val.url == "/authentication") {
          this.loggedIn.next(true);
        } else {
          if (sessionStorage.getItem("authToken") != null) {
            this.loggedIn.next(true);
          } else {
            this.loggedIn.next(false);
          }
        }
      } else {
        this.loggedIn.next(true);
      }
    });

    return this.isLoggedIn;
  }

  // LogOut
//   LogOut(userId: string) {
//     this.gnBaseURL = this.appURL.getServerUrl();
//     return this.http
//         .post<any>(this.gnBaseURL + "Auth/logout",userId)
//         .pipe(catchError(this.handleError));
// }

LogOut(UserName: string) {
  this.gnBaseURL = this.appURL.getServerUrl();
  return this.http
    .post<any>(this.gnBaseURL + "Auth/Logout", { UserName })    
    .pipe(catchError(this.handleError));
}


OtpCheck(Otp : number){
  this.gnBaseURL = this.appURL.getServerUrl();
  return this.http
    .post<any>(this.gnBaseURL + "Auth/OtpCheck", { Otp })    
    .pipe(catchError(this.handleError));
}
}
