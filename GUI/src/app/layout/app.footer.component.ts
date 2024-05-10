import { Component } from '@angular/core';
import { LayoutService } from "./service/app.layout.service";

@Component({
    selector: 'app-footer',
    templateUrl: './app.footer.component.html'
})
export class AppFooterComponent {
    date:Date;
    year:any
    constructor(public layoutService: LayoutService) {
        let today = new Date(); 
        this.year = today.getFullYear();
    }
    
}
