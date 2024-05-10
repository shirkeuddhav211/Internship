import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { DividerModule } from 'primeng/divider';
import { ImageModule } from 'primeng/image';
import { MediaDemoRoutingModule } from '../../uikit/media/mediademo-routing.module';
import { DialogModule } from 'primeng/dialog';


@NgModule({
    imports: [
        CommonModule,
        LoginRoutingModule,
        ButtonModule,
        CheckboxModule,
        InputTextModule,
        FormsModule,
        PasswordModule,
        DividerModule,
        ImageModule,
        MediaDemoRoutingModule,
        DialogModule,
		
    ],
    declarations: [LoginComponent]
})
export class LoginModule { }
