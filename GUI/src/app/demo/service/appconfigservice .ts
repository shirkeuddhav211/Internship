import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  constructor(private http: HttpClient) { }
  private appConfig: any;

  loadAppConfig() {
    return this.http
      .get("assets/config.json")
      .toPromise()
      .then(data => {
        console.log("new data "+data);
        return (this.appConfig = data);

      });
  }

  getServerUrl(): string {
    return this.appConfig.API_URL;
  }

  getHeaderText(): string {
    return this.appConfig.HeaderText;
  }

  getSecurityMessage(): string {
    return this.appConfig.SecurityMessage;
  }



  getClearanceDateText(): string {
    return this.appConfig.ClearanceDateText;
  }

  getAdvisoryText(): string {
    return this.appConfig.AdvisoryText;
  }

  getTimeOutText(): string {
    return this.appConfig.SessionTimeOutMinutes;
  }
  getBadgeAuditMessage(): string {
    return this.appConfig.SecurityMessageAuth;
  }
}