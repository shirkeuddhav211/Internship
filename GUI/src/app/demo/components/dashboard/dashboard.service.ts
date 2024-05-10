import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/demo/service/base.service';
import { AppConfigService } from 'src/app/demo/service/appconfigservice ';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { CountList, Datascrapingpdf, MySubmission, reportResult } from '../uikit/mysubmission/mysubmission.model';
import { SearchModel } from '../uikit/circularmaster/circularmaster.model';


@Injectable({
    providedIn: 'root'
  })

  export class DashboardService extends BaseService {
    gnBaseURL: any;
  constructor(protected http: HttpClient, private appURL: AppConfigService) {
    super();
    this.gnBaseURL = appURL.getServerUrl();
  }

  public  getAllDashboardFilling(): Observable <MySubmission[]> {
    return this.http
    .get<MySubmission[]>(this.gnBaseURL + "Dashboard");
  }


  
  public  getOnlinePdfs(): Observable <Datascrapingpdf[]> {
    return this.http
    .get<Datascrapingpdf[]>(this.gnBaseURL + "Dashboard/GetOnlineCircular");
  }
  

  // public  getReportsByDates(): Observable <MySubmission[]> {
  //   return this.http
  //   .post<MySubmission[]>(this.gnBaseURL + "Dashboard/Search");
  // }

  searchReportsByDates(model: SearchModel) {  
    return this.http
      .post<MySubmission[]>(this.gnBaseURL + "Dashboard/Search", model)
      .pipe(catchError(this.handleError));
  }

   getDataByThreds(model: SearchModel) {
    return this.http
    .post<any[]>(this.gnBaseURL + "Dashboard/GetDataByThreds", model)
    .pipe(catchError(this.handleError));
    //.get<any>(this.gnBaseURL + "Dashboard/GetDataByThreds");
    
  }

  getReports(model: SearchModel): Observable <reportResult[]> {
    return this.http
    .post<reportResult[]>(this.gnBaseURL + "Dashboard/GetReports", model)
    .pipe(catchError(this.handleError));
    //.get<MySubmission[]>(this.gnBaseURL + "Dashboard/GetReports");
  }



  GetPendingAssignmentReport(model: SearchModel) {
 
    return this.http.post(this.gnBaseURL + "Dashboard/GetPendingAssingementReport",model,{responseType: "blob"});
  }

  }