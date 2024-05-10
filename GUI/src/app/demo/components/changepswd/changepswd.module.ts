
import { ChangepswdComponent } from './changepswd.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { LoginRoutingModule } from './login-routing.module';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
//import { LoginRoutingModule } from '../auth/login/login-routing.module';


@NgModule({
  imports: [
    CommonModule,
    ButtonModule,
    CheckboxModule,
    InputTextModule,
    FormsModule,
    PasswordModule,

],
declarations: []
})
export class ChangepswdModule { }
