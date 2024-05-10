import { APP_INITIALIZER, CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { ProductService } from './demo/service/product.service';
import { CountryService } from './demo/service/country.service';
import { CustomerService } from './demo/service/customer.service';
import { EventService } from './demo/service/event.service';
import { IconService } from './demo/service/icon.service';
import { NodeService } from './demo/service/node.service';
import { PhotoService } from './demo/service/photo.service';
import { LoginForm1Component } from './demo/components/login-form1/login-form1.component';
import { FormsModule } from '@angular/forms';
import { LoginAComponent } from './demo/components/login-a/login-a.component';
import { RegisteruserComponent } from './demo/components/registeruser/registeruser.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { ChangepswdComponent } from './demo/components/changepswd/changepswd.component';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { Dropdown, DropdownModule } from 'primeng/dropdown'
//import { ForgotpswdComponent } from './forgotpswd/forgotpswd.component';
import { CommonModule } from '@angular/common';
import { RegisterusenewComponent } from './demo/components/registerusenew/registerusenew.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { ChipsModule } from 'primeng/chips';
import { ForgetpasswordComponent } from './demo/components/forgetpassword/forgetpassword.component';
//import { InspectionComponent } from './inspection/inspection.component';
import { InspectiontypeComponent } from './demo/components/inspection/inspectiontype.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppConfigService } from './_services/appconfigservice';
import { HideRequestMethodInterceptor } from './helpers/HideRequestMethodInterceptor';
import { HttpRequestInterceptor } from './helpers/HttpRequestInterceptor';
import { RemoveHeaderInterceptor } from './helpers/RemoveHeaderInterceptor';
import { AuthGuard } from './helpers/auth.guard';
import { AuthService } from './helpers/auth.service';
import { LoaderInterceptor } from './interceptors/loading.interceptor';
import { MessageService, SharedModule } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { UserListComponent } from './demo/components/user-list/user-list.component';
import { InspectionComponent } from './demo/components/inspection/inspection.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InspectionaddeditComponent } from './demo/components/inspection/inspections-list/inspectionaddedit/inspectionaddedit.component';
import { InspectionsListComponent } from './demo/components/inspection/inspections-list/inspections-list.component';
import { DialogModule } from 'primeng/dialog';
import { ToolbarModule } from 'primeng/toolbar';
import { DividerModule } from 'primeng/divider';
import { PanelsDemoRoutingModule } from './demo/components/uikit/panels/panelsdemo-routing.module';
import { RippleModule } from 'primeng/ripple';
import { SplitButtonModule } from 'primeng/splitbutton';
import { AccordionModule } from 'primeng/accordion';
import { TabViewModule } from 'primeng/tabview';
import { FieldsetModule } from 'primeng/fieldset';
import { MenuModule } from 'primeng/menu';
import { SplitterModule } from 'primeng/splitter';
import { PanelModule } from 'primeng/panel';
import { DashboardComponent } from './demo/components/dashboard/dashboard.component';
import { ImageModule } from 'primeng/image';
import { MediaDemoRoutingModule } from './demo/components/uikit/media/mediademo-routing.module';
import { HolidaymasterComponent } from './demo/components/holidaymaster/holidaymaster.component';
import { HolidaymasteraddComponent } from './demo/components/holidaymaster/holidaymasteradd/holidaymasteradd.component';
import { InspectionMapComponent } from './demo/components/inspection-map/inspection-map.component';

import { ToastModule } from 'primeng/toast';
import { ResidentuserComponent } from './demo/components/residentuser/residentuser.component';
import { NewInspectionComponent } from './demo/components/inspection/new-inspection/new-inspection.component';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { InputSwitchModule } from 'primeng/inputswitch';
import { SelectButtonModule } from 'primeng/selectbutton';
import { GoogleMapsModule } from '@angular/google-maps';

const initializerConfigFn = (appConfig: AppConfigService) => {
    return () => {
        return appConfig.loadAppConfig();
    };
};
@NgModule({
    declarations: [
        AppComponent, NotfoundComponent, 
        LoginForm1Component, LoginAComponent, RegisteruserComponent, ChangepswdComponent,
        HolidaymasterComponent,HolidaymasteraddComponent,
         RegisterusenewComponent, ForgetpasswordComponent, InspectiontypeComponent,UserListComponent,InspectionComponent,InspectionaddeditComponent,
         InspectionsListComponent,DashboardComponent,InspectionMapComponent,ResidentuserComponent, NewInspectionComponent
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        CommonModule,
        ButtonModule,
        CheckboxModule,
        InputTextModule,
        FormsModule,
        PasswordModule,
        AutoCompleteModule,
        CalendarModule,
        ChipsModule,
        DropdownModule,
        NgbModule,
        TableModule,
        RadioButtonModule,
        ToastrModule.forRoot({
            timeOut: 7000,
            positionClass: "toast-bottom-full-width",
            preventDuplicates: true,
            closeButton: true,
            
        }),
        DialogModule,
        ToolbarModule,
        DividerModule,
        SharedModule,
		GoogleMapsModule,
		PanelsDemoRoutingModule,
		
		
		RippleModule,
		SplitButtonModule,
		AccordionModule,
		TabViewModule,
		FieldsetModule,
		MenuModule,
		
		
		SplitterModule,
		PanelModule,
        ImageModule,
        MediaDemoRoutingModule,
        ToastModule,
         
        InputSwitchModule, 
        ToastModule, 
        ToggleButtonModule ,
        SelectButtonModule, 
        //apiKey: "AIzaSyD-3Ak2ydS8prTaYsABf3Oe0WzhrJTbinM",
    ],
    providers: [
        MessageService,
        // { provide: LocationStrategy, useClass: HashLocationStrategy },
        {
            provide: [LocationStrategy, AuthService, AuthGuard],
            useClass: PathLocationStrategy,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpRequestInterceptor,
            multi: true,
        },
        {
            provide: APP_INITIALIZER,
            useFactory: initializerConfigFn,
            multi: true,
            deps: [AppConfigService],
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: LoaderInterceptor,
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: RemoveHeaderInterceptor,
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HideRequestMethodInterceptor,
            multi: true
        },
        CountryService, CustomerService, EventService, IconService, NodeService,
        PhotoService, ProductService
    ],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA ]
})
export class AppModule { }
