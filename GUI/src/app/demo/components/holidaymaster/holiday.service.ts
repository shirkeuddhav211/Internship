import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

import { HttpClient } from "@angular/common/http";
import { HolidayMaster } from "./holidaymodel";
import { AppConfigService } from "src/app/_services/appconfigservice";

@Injectable({
    providedIn: 'root'
  })
  export class HolidayService {
    gnBaseURL:any
    constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,) { 
      this.gnBaseURL = appURL.getServerUrl();
    }
  
    public AddHoliday(holiday) {
      var formData = new FormData();
      formData.append("holiday", JSON.stringify(holiday));
  
      return this.http.post(this.gnBaseURL + "HolidayMaster/AddEditHoliday", formData);
    }

    public GetHolidayList() {
      return this.http.get<HolidayMaster[]>(this.gnBaseURL +"HolidayMaster/GetHolidayList");
    }
  
    public GetHolidayById(id: number) {
  
      return this.http.get<HolidayMaster>(this.gnBaseURL +"HolidayMaster/GetHolidayById/" + id);
    }
  
    public EditHoliday(model: HolidayMaster) {
      return this.http
        .put(this.gnBaseURL + "HolidayMaster/" + model.Id, model);
    }
  
    public DeleteHolidayById(id: number) {
  
      return this.http.get(this.gnBaseURL + "HolidayMaster/DeleteHolidayById/" + id );
    }
  
  }  