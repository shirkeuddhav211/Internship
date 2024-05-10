import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Product } from '../../api/product';
import { ProductService } from '../../service/product.service';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FromTODate, Inspection } from '../inspection/inspections-list/inspectionsmodel';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionService } from '../inspection/inspection.service';
import { NgbCalendar, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit, OnDestroy {
    items!: MenuItem[];

    products!: Product[];

    chartData: any;

    chartOptions: any;

    subscription!: Subscription;

    loading: boolean = true;

  @ViewChild('filter') filter!: ElementRef;
  inspectionList:Inspection[];
  user:any;
  fromToDates: FromTODate = new FromTODate();
  fromDate: string;
  toDate: string;
  
  date3: Date;
  newFromDate :any
  display: boolean = false;
  inspId:string;
  inspectionModel= new Inspection();
  userRole:string;


    constructor( private route:Router,
        private spinner: NgxSpinnerService, 
        private productService: ProductService,
        public inspectionTypeService : InspectionService,
        private dateAdapter: NgbDateAdapter<string>,
        private ngbCalendar: NgbCalendar,
        public toastr:ToastrService) {
    }

    ngOnInit() {
        this.loading = false;
        this.GetInspectionListWithoutDate()
    }

    public GetInspectionListWithoutDate() {    
        this.inspectionList=[];
        this.inspectionTypeService.GetInspectionListWithoutDate().subscribe((response: Inspection[]) => {
          this.inspectionList = response;
        },
        (error:any)=> {
          this.toastr.error('Error while fetching Inspection TYpe', 'Error');
          //this.spinner.hide();
        });
      }
    
    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
