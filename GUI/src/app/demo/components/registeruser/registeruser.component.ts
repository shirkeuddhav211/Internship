import { Component } from '@angular/core';
import { RegisterModel } from './registermodel';
import { HttpClient } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { RegisterService } from './register.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registeruser',
  templateUrl: './registeruser.component.html',
  styleUrl: './registeruser.component.scss',
  
})
export class RegisteruserComponent {

  Register = new RegisterModel();
  


  constructor(private httpClient: HttpClient , private spinner: NgxSpinnerService, private regService:RegisterService,private toastr: ToastrService) {}

  onRegister() {
    this.regService.registerUser(this.Register).subscribe(
      (response) => {
        //this.spinnerText = "Saving...";
        this.spinner.show();
        if (response.statusText == 'Success') {
          this.spinner.hide();
          this.toastr.success("Register Successfully",'information');
          

        }
         else  if (response.statusText == 'Duplicate') {
          this.spinner.hide();
          this.toastr.success("This Data is Already Exits",'information');
        }
        else {
          this.spinner.hide();
          this.toastr.error("Something went Wrong",'Error');
        }
      },
      (error) => {
        this.toastr.error("Registration failed",'Error');
      }
    );
  }
}
