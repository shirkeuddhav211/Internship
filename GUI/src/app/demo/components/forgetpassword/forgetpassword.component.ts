import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { RegisterModel } from '../registeruser/registermodel';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  styleUrl: './forgetpassword.component.scss'
})
export class ForgetpasswordComponent {
  Register: any = {}
  
  constructor(private route:Router , public layoutService: LayoutService,
    public authenticateservice: AuthenticationService,
    private router: ActivatedRoute,private toastr: ToastrService,){

  }
   ngOnInit(){
    let param = this.router.snapshot.paramMap.get('userid')
    let split = param.split('userid=') 
   
    let userid = split[1]
    let code = userid.split('&&')
    this.Register.userid = code[0]
    this.Register.code = code[1]
   }

  onsubmit(){
    this.authenticateservice.resetpassword(this.Register.userid, this.Register.code,this.Register.password, this.Register.password).
    subscribe((response: any) => {
      this.Register = response;
      if (response != null) {
        let result = '';
        try {
          result = response.errorText;
        } catch { }
        if (result === 'Error') {
          this.toastr.error('Something went wrong while Reseting password');
        } else if (result === 'Success') {
          this.toastr.success('Password changed');
          this.route.navigate(["/"]);
        } 
        else {
          this.toastr.error('Error in resetting password. Create restet link again');
        }
      }
    });
  }
}
