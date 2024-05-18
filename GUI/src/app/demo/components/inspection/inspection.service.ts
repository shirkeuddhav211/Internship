import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { AppConfigService } from 'src/app/_services/appconfigservice';
import { InspectionTypes } from './inspectionmodel';
import { Inspection } from './inspections-list/inspectionsmodel';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class InspectionService {
  gnBaseURL:any
  constructor(private router: Router, private appURL: AppConfigService, private http: HttpClient,) { 
    this.gnBaseURL = appURL.getServerUrl();
  }

  public AddInspectiType(inspectiontyp) {
    var formData = new FormData();
    formData.append("inspectiontype", JSON.stringify(inspectiontyp));

    return this.http.post(this.gnBaseURL + "InspectionType/AddEditInspectionType", formData);
  }

  public GetInspectionTypeList() {
    return this.http.get<InspectionTypes[]>(this.gnBaseURL +"InspectionType/GetInspectionTypesList");
  }

  public GetInspectionTypeById(id: number) {

    return this.http.get<InspectionTypes>(this.gnBaseURL +"InspectionType/GetInspectionTypeById/" + id);
  }

  public EditInspectionType(model: InspectionTypes) {
    return this.http
      .put(this.gnBaseURL + "InspectionType/" + model.Id, model);
  }

  public DeleteInspectionById(id: number) {

    return this.http.get(this.gnBaseURL + "InspectionType/DeleteInspectionTypeById/" + id );
  }


  ///
  public AddInspectionDetail(inspectionDetails)
  {   
    var formData = new FormData();
    formData.append("InspectionDetail", JSON.stringify(inspectionDetails));   
    return this.http
    .post<string>(this.gnBaseURL + "Inspection/AddInspectionDetail", formData);    
  } 

  public EditInspectionDetail(inspectionDetails)
  {   
    var formData = new FormData();
    formData.append("InspectionDetail", JSON.stringify(inspectionDetails));   
    return this.http
    .put<string>(this.gnBaseURL + "Inspection/EditInspectionDetail/", formData);    
  } 


  public GetInspectionDetailsById(inspectionId: number): Observable<Inspection> {
    return this.http
      .get<Inspection>(this.gnBaseURL + "Inspection/GetInspectionDetailsById?inspectionId=" + inspectionId);
  } 

  public GetInspectiontList(fromDate,toDate) {
    return this.http
      .get<Inspection[]>(this.gnBaseURL + "Inspection/GetInspectionList?fromDate=" + fromDate + `&toDate=` + toDate);
  }

  public GetInspectionListWithoutDate() {
    return this.http.get<Inspection[]>(this.gnBaseURL +"Inspection/GetInspectionListWithoutDate");
  }

  public EditInspectionackValue(id,ackvalue) {
    return this.http
      .get<Inspection>(this.gnBaseURL + "Inspection/EditInspectionackValue?id=" + id +"&ackvalue="+ ackvalue);
  }
  
  public EditInspectionRejectValue(id,rejectvalue, rejectComments) {
    return this.http
      .get<Inspection>(this.gnBaseURL + "Inspection/EditInspectionRejectValue?id=" + id +"&rejectvalue="+ rejectvalue + "&rejectComments="+ rejectComments);
  }

  public DeleteInspection(id: number) {

    return this.http.get(this.gnBaseURL + "Inspection/DeleteInspectionById/" + id );
  }

  public UpdateInspectionInspector(id,inspector) {
    return this.http
      .get<Inspection>(this.gnBaseURL + "Inspection/UpdateInspectionInspector?id=" + id +"&inspector="+ inspector );
  }

  public UpdateInspectionTime(id,time) {
    return this.http
      .get<Inspection>(this.gnBaseURL + "Inspection/UpdateInspectionTime?id=" + id +"&time="+ time );
  }

  public UpdateInspectionDate(id,date) {
    return this.http
      .get<Inspection>(this.gnBaseURL + "Inspection/UpdateInspectionDate?id=" + id +"&date="+ date );
  }


}
