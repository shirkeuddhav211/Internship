import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { ImageModule } from 'primeng/image';
import { MediaDemoRoutingModule } from '../uikit/media/mediademo-routing.module';

@NgModule({
    imports: [
        CommonModule,
        AuthRoutingModule,
        ImageModule,
        MediaDemoRoutingModule
    ]
})
export class AuthModule { }
