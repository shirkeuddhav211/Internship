import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { RegisterusenewComponent } from '../registerusenew/registerusenew.component';
import { hide } from '@popperjs/core';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];
    user: any;
    staff: boolean = false
    constructor(public layoutService: LayoutService) { }

    ngOnInit() {
        this.user = JSON.parse(sessionStorage.getItem('currentUser'));
        const role = this.user.role
        // this.model = [
        //     {
                
        //         items: [
        //             { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['dashboard'] }
        //         ]
        //     },
        //     {                    
        //         items: [
        //             { label: 'Users', icon: 'pi pi-fw pi-id-card', routerLink: ['/app/list1'] },                        
        //             { label: 'Inspection', icon: 'pi pi-fw pi-check-square', routerLink: ['/app/inspectionformlist'] }, 
        //             { label: 'Inspection Type', icon: 'pi pi-fw pi-id-card', routerLink: ['/app/inspectionlist'] ,hidden: true },  
        //             { label: 'Holiday', icon: 'pi pi-fw pi-id-card', routerLink: ['/app/holidaylist'] ,hidden: true},     
        //             // { label: 'My Profile', icon: 'pi pi-fw pi-sign-in', routerLink: ['/app/register'] },     
        //         ]
        //     },                
        // ];

        if(this.user.role == "SuperAdmin" || this.user.role == "Manager" || this.user.role == "Admin"){
            this.staff = true 
            this.model = [
                // {
                //     label: 'Admin Functions',
                //     items: [
                //         { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['dashboard'] }
                //     ]
                // },
                {    
                    label: 'Admin Functions',                
                    items: [
                        { label: 'Inspections', icon: 'pi pi-fw pi-check-square', routerLink: ['/app/inspectionformlist'] }, 
                        { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['dashboard'] },
                        { label: 'Users', icon: 'pi pi-fw pi-id-card', routerLink: ['/app/list1'] },                        
                        
                        { label: 'Inspection Type', icon: 'pi pi-fw pi-id-card', routerLink: ['/app/inspectionlist'] ,hidden: true },  
                        { label: 'Holiday', icon: 'pi pi-fw pi-id-card', routerLink: ['/app/holidaylist'] ,hidden: true},          
                    ]
                },                
            ];
        }else if(this.user.role == "Inspector"){
            this.model = [                
                {                    
                    items: [   
                        { label: 'Inspections', icon: 'pi pi-fw pi-check-square', routerLink: ['/app/inspectionformlist'] },
                    ]
                },                
            ];
        }else if(this.user.role == "Resident"){
            this.model = [                
                {                    
                    items: [   
                        { label: 'Inspections', icon: 'pi pi-fw pi-check-square', routerLink: ['/app/inspectionformlist'] },
                        { label: 'My Profile', icon: 'pi pi-fw pi-sign-in', routerLink: ['/app/register'] },
                    ]
                },                
            ];
        }else if(this.user.role == "Staff"){
            this.model = [                
                {                    
                    items: [   
                        { label: 'Inspections', icon: 'pi pi-fw pi-check-square', routerLink: ['/app/inspectionformlist'] },
                    ]
                },                
            ];
        }else{
            this.model = [                
                {                    
                    items: [   
                       
                        { label: 'My Profile', icon: 'pi pi-fw pi-sign-in', routerLink: ['/app/register'] },
                    ]
                },                
            ];
        }

        

      
    }
}
