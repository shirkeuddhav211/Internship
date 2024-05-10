import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'; 
import { login, register } from './login';

@Component({
  selector: 'app-login-form1',
  templateUrl: './login-form1.component.html',
  styleUrl: './login-form1.component.scss'
})
export class LoginForm1Component implements OnInit {
  
  loginmodel:login={
    userName:'',
    password:''
  };

  Resitermodel:register={
    userName:'',
    email:'',
    password:''
  }


  // signupUsers: any[]=[];
  // signupobj:any={
  //   userName: '',
  //   email: '',
  //   password: ''
  // };

  // loginObj: any={
  //   userName: '',
  //   password: ''
  // };
  
  constructor(private routes:Router){
    
  }

  ngOnInit(): void{
    // const localData =localStorage.getItem('signUpUsers');
    // if(localData != null){
    //   this.signupUsers = JSON.parse(localData);
    // }
  }
  // onSignUp(){
  //   this.signupUsers.push(this.signupobj);
  //   localStorage.setItem('signUpUsers',JSON.stringify(this.signupUsers));
  //   this.signupobj= {
  //     userName: '',
  //     email: '',
  //     password: ''
  //   }
 
  // }
  // onLogin(){
  //   const isUserExist = this.signupUsers.find(m => m.userName == this.loginObj.userName && m.password == this.loginObj.password);
  //   if(isUserExist != undefined){
  //     alert("User Login Succesfully");
  //   this.routes.navigate(['/app']);
      
  //   }
  //   else{
  //     alert("Incorrect Credentials");
  //   }
  // }

SubmitLogin(){

  alert("User Login Succesfully");
}

SubmitRegister(){
  alert("Data Ragister");


  // GetData(){
  //   const obj:register = {
  //     userName: '',
  //     email: '',
  //     password: ''
  //   }
  //   return obj;
  // }

  
}
}