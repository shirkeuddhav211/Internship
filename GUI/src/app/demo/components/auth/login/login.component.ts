import { Component } from '@angular/core';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { LoginModel } from './loginmodel';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { el } from '@fullcalendar/core/internal-common';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs';
import { NoticeService } from '../../notice/notice.service';
import { Notice } from '../../notice/noticemodel';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent {

    constructor(private router:Router , public layoutService: LayoutService,
      public authenticateservice: AuthenticationService, public noticeService:NoticeService,
      private toastr: ToastrService,){

    }
      login = new LoginModel()
      user:any
      notice : Notice[];
      noticeTxt: string =""
      
      display:boolean = true
      isForgotPassword: boolean = false;
      error = "";

  ngOnInit(){
     this.GetNoticeList();
     }

  onSubmit(){
    this.login.username = this.login.username.toLowerCase()
    if (!this.isForgotPassword){
      
      this.authenticateservice.login(this.login.username,this.login.password).pipe(first()).subscribe(
        (data: any) => {
          if(data.errorText == "Invalid"){
            this.error = "Invalid Credentials";
          }
          if(data.errorText == "Deactivated"){
            this.error = "User Deactivated";
          }
          this.user = JSON.parse(sessionStorage.getItem('currentUser'));
          // if(this.user.role == "SuperAdmin" || this.user.role == "Manager" || this.user.role == "Admin"){
          //   this.router.navigate(["/app/dashboard"]);
          // }else {
            this.router.navigate(["/app/inspectionformlist"]);
          //}
          
          
        },(error: any) => {
          this.error = "Invalid Credentials";
          
        }
        )
    }else{
      this.authenticateservice
        .forgotPassword(this.login.username)
        .subscribe(
          (data: any) => {
           var abc = data.id
              this.router.navigate(["/forgotpassword/userid="+ abc]);
          },
          (error: any) => {
            this.error = error;
            //this.spinner.hide();
            if (error == "OK") {
              this.toastr.error("Password reset link has been sent to email.");
            }

            this.isForgotPassword = false;
            this.error = "";
            this.router.navigate(["/"]);
          }
        );
    }
    
  }
  
  forgotPassword() {
    this.isForgotPassword = true;
  }

  residentsignup(){
    this.router.navigate(["/residentuser"]);
  }

  GetNoticeList() {
    this.notice=[];
    this.noticeService.GetNoticeList().subscribe((response: Notice[]) => {
      this.notice = response;
      this.noticeTxt=this.notice[0].Notice1
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Notice', 'Error');
      //this.spinner.hide();
    });
  }
}
