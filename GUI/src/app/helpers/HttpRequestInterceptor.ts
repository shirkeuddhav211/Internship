import { Injectable } from "@angular/core";
import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpHeaders,
} from "@angular/common/http";

import { Observable, throwError } from "rxjs";
import { AuthService } from "./auth.service";
import { Router } from "@angular/router";
import { catchError } from "rxjs/operators";
import { MessageService } from "primeng/api";

/** Inject With Credentials into the request */
@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthService,private messageService: MessageService,
        private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        let currentUser = this.authenticationService.currentUserValue;
        if (currentUser && currentUser.authToken) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.authToken}`
                }
            });
        }

        // return next.handle(request);
        return next.handle(request).pipe(
            catchError(err => {
                if (err.status === 401 || err.status === 403) {
                    // auto redirect to login page if 401 response returned from api
                    sessionStorage.clear();
                    // location.reload(true);
                    this.router.navigate(["/"]);
                    this.messageService.add({
                        key: "error",
                        severity: "error",
                        summary: "Failed",
                        detail: "Session Expired. Please login again.",
                        life: 3000
                      });
                }
                else if(err.status === 500){
                    sessionStorage.clear();
                    // location.reload(true);
                    this.router.navigate(["/"]);                    
                }
                if (err.status === 406){
                    sessionStorage.clear();
                    // location.reload(true);
                   // this.router.navigate(["/"]);
                  
                  
                   for (const key in err.error.Errors.ERROR) {
                    if (err.error.Errors.ERROR[key]) {
                        const errorCode = err.error.Errors.ERROR[key];
                        if (typeof errorCode == 'string') {
                            err.message = errorCode;
                        }
                        else {
                            err.message = errorCode.reduce(
                                (accumulator: any, currentValue: any) =>
                                    accumulator + currentValue + ' <br />',
                                    err.message
                            );
                        }
                        sessionStorage.clear();
                        this.router.navigate(["/"]);               
                   return throwError(() => err.message);
                   
                }}}
               
                return throwError(() => err);
            })
        );
    }
}