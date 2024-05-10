import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfigService } from "./appconfigservice";
import { NgxSpinnerService } from "ngx-spinner";
import { NavigationEnd, Router } from "@angular/router";
import { BehaviorSubject, Observable, catchError, map } from "rxjs";
import { BaseService } from "../demo/service/base.service";
import { CurrentUser, RoleViewModel } from "../demo/components/auth/user.model";
import { RegisterModel, RegisterModelNew } from "../demo/components/registerusenew/registermodel";


@Injectable({ providedIn: 'root' })
export class AuthenticationService extends BaseService {
    public loggedIn = new BehaviorSubject<boolean>(this.tokenAvailable());
    private currentUserSubject: BehaviorSubject<CurrentUser>;
    private curusername = new BehaviorSubject<string>(this.setUsername());
    private userOrganization = new BehaviorSubject<string>(this.setUsername());
    gnBaseURL: any;
    constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,) {
        super();

        this.currentUserSubject = new BehaviorSubject<CurrentUser>(
            JSON.parse(sessionStorage.getItem('currentUser') || '{}'));

        this.gnBaseURL = this.appURL.getServerUrl();    

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
        console.log("url "+this.gnBaseURL)
        return this.http
            .post<any>(this.gnBaseURL + "Auth", { UserName, Password })
            .pipe(
                map(user => {
                    this.setCurrentUser(user);
                    return user;
                })
            )
            .pipe(catchError(this.handleError));
    }

    forgotPassword(username: string) {
        this.gnBaseURL = this.appURL.getServerUrl();

        username = username.toLowerCase();
        let headers = new HttpHeaders();
        headers = headers.append('noToken', 'noToken');
        headers = headers.append('Content-Type', 'application/json');

        return this.http.post<any>(this.gnBaseURL + "Auth/ForgotPassword", { Email: username }, { headers: headers })
            .pipe(map(a => {
                return a;
            }));
    }

    resetpassword(UserId: string, Code:string, Password: string, ConfirmPassword: string) {
        this.gnBaseURL = this.appURL.getServerUrl();
        let headers = new HttpHeaders();
        headers = headers.append('noToken', 'noToken');
       // headers = headers.append('Content-Type', 'application/json');
        return this.http.post<any>(this.gnBaseURL + "Auth/updatepassword", { UserId, Code,Password, ConfirmPassword })
            .pipe(map(a => {
                return a;
            }));
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


    OtpCheck(Otp: number) {
        this.gnBaseURL = this.appURL.getServerUrl();
        return this.http
            .post<any>(this.gnBaseURL + "Auth/OtpCheck", { Otp })
            .pipe(catchError(this.handleError));
    }

    registerUser(user) {
        var formData = new FormData();
        formData.append("model", JSON.stringify(user));
        this.gnBaseURL = this.
        appURL.getServerUrl();
         
         return this.http.post(this.gnBaseURL + "Users", formData);
         
    }

    GetAllRolesList() {
        this.gnBaseURL = this.appURL.getServerUrl();
        return this.http.get<RoleViewModel[]>(this.gnBaseURL +"Role/GetAllRolesList");
    }

    GetUserList() {
        return this.http.get<RegisterModel[]>(this.gnBaseURL +"Users/GetAllUsers");
    }

    GetUserListforInspection() {
        return this.http.get<RegisterModel[]>(this.gnBaseURL +"Users/GetAllUsers");
    }

    DeleteUserByid(id: string) {
        return this.http.get(this.gnBaseURL +"Users/DeleteUserById?id=" + id);
    }

    activateUserByid(id: string) {
        return this.http.get(this.gnBaseURL +"Users/activateUserById?id=" + id);
    }

    public getUserById(id: number) {
        return this.http
          .get(this.gnBaseURL + "Users/GetUserById?id=" + id);
      }
    
      public EditUser(model: RegisterModel) {
        var formData = new FormData();
        formData.append("model", JSON.stringify(model));
        return this.http
          .put(this.gnBaseURL + "Users/" + model.Id, formData);
      }
}
