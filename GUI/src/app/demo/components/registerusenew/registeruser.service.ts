import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppConfigService } from "../../service/appconfigservice ";

@Injectable({ providedIn: 'root' })
export class RegisteruserService {
    gnBaseURL: any;

    constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,){
        
    }

    registerUser(user) {
        var formData = new FormData();
        formData.append("model", JSON.stringify(user));
        this.gnBaseURL = this.appURL.getServerUrl();
         
         return this.http.post(this.gnBaseURL + "Users", formData);
         
       }
}