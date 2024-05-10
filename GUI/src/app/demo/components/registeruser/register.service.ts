import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterModel } from './registermodel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = 'https://your-api-endpoint.com/register';

  constructor(private http: HttpClient) {}

  registerUser(data: RegisterModel): Observable<any> {
    var formData = new FormData();
    formData.append("Data", JSON.stringify(data));
    return this.http.post(this.apiUrl + "User", formData);
  }

}
