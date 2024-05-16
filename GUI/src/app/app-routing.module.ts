import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { AppLayoutComponent } from "./layout/app.layout.component";
import { LoginForm1Component } from './demo/components/login-form1/login-form1.component';
import { LoginAComponent } from './demo/components/login-a/login-a.component';
import { DashboardComponent } from './demo/components/dashboard/dashboard.component';
import { RegisteruserComponent } from './demo/components/registeruser/registeruser.component';
import { ChangepswdComponent } from './demo/components/changepswd/changepswd.component';
import { RegisterusenewComponent } from './demo/components/registerusenew/registerusenew.component';
import { ForgetpasswordComponent } from './demo/components/forgetpassword/forgetpassword.component';
import { InspectiontypeComponent } from './demo/components/inspection/inspectiontype.component';
import { LoginComponent } from './demo/components/auth/login/login.component';
import { UserListComponent } from './demo/components/user-list/user-list.component';
import { InspectionComponent } from './demo/components/inspection/inspection.component';
import { InspectionaddeditComponent } from './demo/components/inspection/inspections-list/inspectionaddedit/inspectionaddedit.component';
import { InspectionsListComponent } from './demo/components/inspection/inspections-list/inspections-list.component';
import { HolidaymasteraddComponent } from './demo/components/holidaymaster/holidaymasteradd/holidaymasteradd.component';
import { HolidaymasterComponent } from './demo/components/holidaymaster/holidaymaster.component';
import { InspectionMapComponent } from './demo/components/inspection-map/inspection-map.component';
import { ResidentuserComponent } from './demo/components/residentuser/residentuser.component';
import { NewInspectionComponent } from './demo/components/inspection/new-inspection/new-inspection.component';
import { NoticeComponent } from './demo/components/notice/notice.component';
import { NoticeAddEditComponent } from './demo/components/notice/notice-add-edit/notice-add-edit.component';



@NgModule({
    imports: [
        RouterModule.forRoot([
            {
                path:'',component: LoginComponent,
            },
            {
                path:'residentuser',component: ResidentuserComponent,
            },
            {
                path: 'app', component: AppLayoutComponent,

               

                children: [
                    {path:'list1' , component:UserListComponent},
                    {path:'holidayaddedit' , component:HolidaymasteraddComponent},
                    {path:'holidaylist', component:HolidaymasterComponent},
                    {path:'map', component:InspectionMapComponent},
                    {path:'register' , component:RegisterusenewComponent},
                    {path:'inspection' , component:InspectiontypeComponent},
                    {path:'inspectionlist' , component:InspectionComponent},
                    {path:'inspecform' , component:InspectionaddeditComponent},
                    {path:'inspectionformlist' , component:InspectionsListComponent},
                    {path:'newinspection',component:NewInspectionComponent},
                    {path:'notice', component:NoticeComponent},
                    {path:'noticaddedit', component:NoticeAddEditComponent}, 
                    { path: 'dashboard', loadChildren: () => import('./demo/components/dashboard/dashboard.module').then(m => m.DashboardModule) },
                    { path: 'uikit', loadChildren: () => import('./demo/components/uikit/uikit.module').then(m => m.UIkitModule) },
                    { path: 'utilities', loadChildren: () => import('./demo/components/utilities/utilities.module').then(m => m.UtilitiesModule) },
                    { path: 'documenApproutestation', loadChildren: () => import('./demo/components/documentation/documentation.module').then(m => m.DocumentationModule) },
                    { path: 'blocks', loadChildren: () => import('./demo/components/primeblocks/primeblocks.module').then(m => m.PrimeBlocksModule) },
                    { path: 'pages', loadChildren: () => import('./demo/components/pages/pages.module').then(m => m.PagesModule) },
                    
                ]
            },
            {path:'register' , component:RegisterusenewComponent},
            {path:'changepswd' , component:ChangepswdComponent},
            {path:'forgotpassword/:userid' , component:ForgetpasswordComponent},
            { path: 'auth', loadChildren: () => import('./demo/components/auth/auth.module').then(m => m.AuthModule) },
            { path: 'landing', loadChildren: () => import('./demo/components/landing/landing.module').then(m => m.LandingModule) },
            { path: 'notfound', component: NotfoundComponent },
            { path: '**', redirectTo: '/notfound' }
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
