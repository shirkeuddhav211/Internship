import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
  export class AppConfigService {
    
    constructor(private http: HttpClient) { }
    private appConfig;
    
    loadAppConfig() {
        return this.http
          .get("assets/config.json")
          .toPromise()
          .then(data => {
            console.log(data);
            return (this.appConfig = data);
            
          });
      }
    
      getServerUrl(): string {
        return this.appConfig.API_URL;
      }
      
  }