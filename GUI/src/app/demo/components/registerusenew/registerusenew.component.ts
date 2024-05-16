import { Component } from '@angular/core';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

import { HttpClient } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { RegisterService } from '../registeruser/register.service';
import * as $ from 'jquery';

import { AuthenticationService } from 'src/app/_services/authentication.service';
import { RegisteruserService } from './registeruser.service';
import { RoleViewModel } from '../auth/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterModel } from './registermodel';
import { el } from '@fullcalendar/core/internal-common';

//import { SelectItem } from './registermodel';

@Component({
  selector: 'app-registerusenew',
  templateUrl: './registerusenew.component.html',
  styleUrl: './registerusenew.component.scss'
})

export class RegisterusenewComponent {

  Register = new RegisterModel()
  userId: string

  selectedDrop: string = '';
  // selectedDrop: SelectItem = { value: '' };
  public layoutService: LayoutService
  role = [
    { name: 'Admin', code: 'Option 1' },
    { name: 'Local user', code: 'Option 2' },
    { name: 'Gobal user', code: 'Option 3' }
  ];

  deparments = [
    { name: 'HR', code: 'HR' },
    { name: 'IT', code: 'IT' },
    { name: 'Software', code: 'Software' },
    { name: 'Hardware', code: 'Hardware' }
  ];
  roleList: RoleViewModel[];
  isEditUser: boolean = false
  userstorage: any;
  isResident: boolean = false
  isEdit: any

  constructor(private httpClient: HttpClient,
    private spinner: NgxSpinnerService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    public authenticateservice: AuthenticationService,
    public registeruserservice: RegisteruserService,
    private router: Router) { }

  ngOnInit() {
    this.userstorage = JSON.parse(sessionStorage.getItem('currentUser'));
    this.isEdit = this.route.snapshot.pathFromRoot[1].queryParams['isEdit'];
    this.userId = this.route.snapshot.pathFromRoot[1].queryParams['userId'];
    this.Register.City = "National City";
    this.Register.State = "California"
    this.GetRoleList();

    if (this.userstorage.role == "Resident") {
      this.userId = this.userstorage.userId
      this.isEditUser = true
      this.GetUserById(this.userId)
      this.isResident = true
    }
    if (this.isEdit == '1') {
      this.isEditUser = true

      this.GetUserById(this.userId)


    }
  }

  GetUserById(id) {
    this.authenticateservice.getUserById(id).subscribe((response: any) => {
      this.Register = response;
    });
  }
  public GetRoleList() {

    this.authenticateservice.GetAllRolesList().subscribe((response: RoleViewModel[]) => {
      console.log(response)
      this.roleList = response;

    }, (error: any) => {
      console.log("error list");

    });
  }

  onRegister() {
    //this.Register.Role = "Resident"
    this.Register.UserName = this.Register.Email
    if (this.isEditUser == true) {
      if (this.Register.FirstName == '' || this.Register.FirstName == undefined) {
        this.toastr.error("Please enter FirstName", 'Error');
      }
      else if (this.Register.LastName == '' || this.Register.LastName == undefined) {
        this.toastr.error("Please enter LastName", 'Error');
      } else if (this.Register.Address == '' || this.Register.Address == undefined) {
        this.toastr.error("Please enter Address", 'Error');
      } else if (this.Register.Email == '' || this.Register.Email == undefined) {
        this.toastr.error("Please enter Email", 'Error');
      } else if (this.Register.newPassword == '' || this.Register.newPassword == undefined) {
        this.toastr.error("Please enter Password", 'Error');
      } else if (this.Register.RoleName == '' || this.Register.RoleName == undefined) {
        this.toastr.error("Please select Role", 'Error');
      } else if (this.Register.PhoneNumber == '' || this.Register.PhoneNumber == undefined) {
        this.toastr.error("Please enter PhoneNumber", 'Error');
      } else if (this.isResident != true && (this.Register.Department == '' || this.Register.Department == undefined)) {
        this.toastr.error("Please select Department", 'Error');
      } else if (this.Register.Zip == '' || this.Register.Zip == undefined) {
        this.toastr.error("Please select Zip code", 'Error');
      }
      else {
        this.Register.UpdatedBy = this.userstorage.userId
        this.authenticateservice.EditUser(this.Register).subscribe(
          (response: Response) => {
            this.spinner.show();
            this.toastr.success("Updated Successfully", 'information');
            if (this.isResident == true) {
              this.router.navigate(["/app/inspectionformlist"])
            } else {
              this.router.navigate(["/app/list1"]);
            }
          },
          (error) => {
            this.toastr.error("Registration failed", 'Error');
          }
        );
      }

    } else {
      if (this.Register.FirstName == '' || this.Register.FirstName == undefined) {
        this.toastr.error("Please enter FirstName", 'Error');
      }
      else if (this.Register.LastName == '' || this.Register.LastName == undefined) {
        this.toastr.error("Please enter LastName", 'Error');
      } else if (this.Register.Address == '' || this.Register.Address == undefined) {
        this.toastr.error("Please enter Address", 'Error');
      } else if (this.Register.Email == '' || this.Register.Email == undefined) {
        this.toastr.error("Please enter Email", 'Error');
      } else if (this.Register.Password == '' || this.Register.Password == undefined || this.Register.Password == null) {
        this.toastr.error("Please enter Password", 'Error');
      } else if (this.Register.RoleName == '' || this.Register.RoleName == undefined) {
        this.toastr.error("Please select Role", 'Error');
      } else if (this.Register.PhoneNumber == '' || this.Register.PhoneNumber == undefined) {
        this.toastr.error("Please enter PhoneNumber", 'Error');
      } else if (this.isResident != true && (this.Register.Department == '' || this.Register.Department == undefined)) {
        this.toastr.error("Please select Department", 'Error');
      } else if (this.Register.Zip == '' || this.Register.Zip == undefined) {
        this.toastr.error("Please select Zip code", 'Error');
      }
      else {
        this.Register.UpdatedBy = this.userstorage.userId
        this.authenticateservice.registerUser(this.Register).subscribe(
          (response: Response) => {
            this.spinner.show();

            this.spinner.hide();
            this.toastr.success("Register Successfully", 'information');
            if (this.isResident == true) {
              this.router.navigate(["/app/inspectionformlist"])
            } else {
              this.router.navigate(["/app/list1"]);
            }
            //this.router.navigate(["/app/list1"]); 
          },
          (error) => {
            if (error.error.Errors.ERROR[0] != null) {
              this.toastr.error("Registration failed", error.error.Errors.ERROR);
            }
            else{
              this.toastr.error("Registration failed",'Error');

            }
          }
        );
      }

    }

  }
  onEmailChange(event: any) {
    const EMAIL_REGEXP = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    if (this.Register.Email !== '' && (this.Register.Email.length <= 5 || !EMAIL_REGEXP.test(this.Register.Email))) {
      this.toastr.warning("Please enter valid email", 'information');
    }

    return "true";
  }
  enableSave() {
    if (this.Register.LastName == '' || this.Register.LastName == undefined) {
      return true
    } else if (this.Register.Address == '' || this.Register.Address == undefined) {
      return true
    } else if (this.Register.Email == '' || this.Register.Email == undefined) {
      return true
    }
    else if (this.Register.UserName == '' || this.Register.UserName == undefined) {
      return true
    } else if ((this.Register.Password == '' || this.Register.Password == undefined) && this.isEditUser == false) {
      return true
    } else if ((this.Register.Password == '' || this.Register.Password == undefined || this.Register.Password == null) && this.isEditUser != true) {
      return true
    } else if (this.Register.RoleName == '' || this.Register.RoleName == undefined) {
      return true
    } else if (this.Register.PhoneNumber == '' || this.Register.PhoneNumber == undefined) {
      return true
    } else if (this.Register.Department == '' || this.Register.Department == undefined) {
      return true
    }
    else {
      return false
    }
  }

  test() {
    console.log("new test")
  }

  close() {
    if (this.isResident == true) {
      this.router.navigate(["/app/inspectionformlist"])
    } else {
      this.router.navigate(["/app/list1"]);
    }

  }
}
