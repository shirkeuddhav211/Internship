import { Component } from '@angular/core';
import { LoginModel } from './loginmodel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-a',
  templateUrl: './login-a.component.html',
  styleUrl: './login-a.component.scss'
})
export class LoginAComponent {
constructor(private route:Router){

}
  login:LoginModel={
    username:'',
    password:'',
    rememberme:true,

  }
  Onlogin(){
this.route.navigate(["/app"]);
  }
}
