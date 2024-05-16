import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { AppConfigService } from 'src/app/_services/appconfigservice';
import { Observable } from 'rxjs';
import { Notice } from './noticemodel';


@Injectable({
  providedIn: 'root'
})
export class NoticeService {
  gnBaseURL:any
  constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,) { 
    this.gnBaseURL = appURL.getServerUrl();
  }

  public GetNoticeList() {
    return this.http.get<Notice[]>(this.gnBaseURL +"Notice/GetNoticeList");
  }

  public GetNoticeById(id: number) {

    return this.http.get<Notice>(this.gnBaseURL +"Notice/GetNoticeById/" + id);
  }

  public AddNotice(notice) {
    var formData = new FormData();
    formData.append("notice", JSON.stringify(notice));

    return this.http.post(this.gnBaseURL + "Notice/AddEditNotice", formData);
  }

  public EditNotice(model: Notice) {
    return this.http
      .put(this.gnBaseURL + "Notice/" + model.Id, model);
  }

  public DeleteNoticeById(id: number) {

    return this.http.get(this.gnBaseURL + "Notice/DeleteNoticeById/" + id );
  }
}
