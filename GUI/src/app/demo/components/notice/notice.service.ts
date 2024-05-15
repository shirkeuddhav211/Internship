import { Injectable } from '@angular/core';
import { AppConfigService } from '../../service/appconfigservice ';
import { Router } from '@angular/router';
import { NoticeType } from './NoticeModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NoticeService {
  gnBaseURL:any
  constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,) { 
    this.gnBaseURL = appURL.getServerUrl();
  }

  public GetNoticeList() {
    return this.http.get<NoticeType[]>(this.gnBaseURL +"Notice/GetNoticeList");
  }

  public GetNoticeById(id: number) {

    return this.http.get<NoticeType>(this.gnBaseURL +"Notice/GetNoticeById/" + id);
  }

  public AddEditNotice(model: NoticeType) {
    return this.http
      .put(this.gnBaseURL + "Notice/" + model.Id, model);
  }

  public DeleteNoticeById(id: number) {

    return this.http.get(this.gnBaseURL + "Notice/DeleteNoticeTypeById/" + id );
  }
}
