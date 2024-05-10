import { Component, ElementRef, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { Customer } from '../../api/customer';
import { CustomerService } from '../../service/customer.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionTypes } from './inspectionmodel';
import { ToastrService } from 'ngx-toastr';
import { InspectionService } from './inspection.service';

@Component({
  selector: 'app-inspection',
  templateUrl: './inspection.component.html',
  styleUrl: './inspection.component.scss'
})
export class InspectionComponent {

  inspectiontypes : InspectionTypes[];
  taskarray=[{type:'inspection1', check : true}]
  rowGroupMetadata: any;
  UserData  : any;
  activityValues: number[] = [0, 100];
  loading: boolean = true;
  @ViewChild('filter') filter!: ElementRef;

  constructor(private route:Router,private spinner: NgxSpinnerService, public inspectionTypeService: InspectionService,
    public toastr:ToastrService) { }

  ngOnInit() {
    this.loading = false
    this.GetInspectionTypeList();
  }


  Add(){
    this.route.navigate(["/app/inspection"]);
  }

  OnDelete(index: number) {
    this.taskarray.splice(index, 1);
  }

  OnEdit(id){
    this.route.navigate(['/app/inspection'], {
      queryParams: { isEdit: 1, inspId: id },
    skipLocationChange:true
    });
  }
  


  public GetInspectionTypeList() {    
    this.inspectiontypes=[];
    this.inspectionTypeService.GetInspectionTypeList().subscribe((response: InspectionTypes[]) => {
      this.inspectiontypes = response;
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Inspection TYpe', 'Error');
      //this.spinner.hide();
    });
  }
 

  formatCurrency(value: number) {
      return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
  }

  onGlobalFilter(table: Table, event: Event) {
      table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
      console.log("table", table)
  }

  clear(table: Table) {
      table.clear();
      this.filter.nativeElement.value = '';
  }
  
  deleteInspectionType(id:number) {
    var ans = confirm("Are you sure you want to delete this Inspection Category?");
    if (ans == true) {      
      this.inspectionTypeService.DeleteInspectionById(id).subscribe((response: Response) => {
        if (response.statusText == "Fail") {
          this.toastr.success('There was some error deleting the record. Please try again later.');
        }
        else if (response.statusText === "Exists") {
          this.toastr.error('Inspection Category not deleted. It is attached to a Inspection Type');
        }
        else {
          this.toastr.success('Inspection Type deleted successfully');
        }        
        this.GetInspectionTypeList();
      });
    }
  
  }
}
