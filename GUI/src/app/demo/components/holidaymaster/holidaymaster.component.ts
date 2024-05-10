import { Component, ElementRef, ViewChild } from '@angular/core';
import { HolidayMaster } from './holidaymodel';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { HolidayService } from './holiday.service';
import { ToastrService } from 'ngx-toastr';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-holidaymaster',  
  templateUrl: './holidaymaster.component.html',
  styleUrl: './holidaymaster.component.scss'
})
export class HolidaymasterComponent {
   
  holidayModel = new HolidayMaster()
  holidaylist : HolidayMaster[];

  
  rowGroupMetadata: any;
 
  loading: boolean = true;
  
  @ViewChild('filter') filter!: ElementRef;

  constructor(private route:Router,private spinner: NgxSpinnerService, public holidayService: HolidayService,
    public toastr:ToastrService) { }

  ngOnInit() {
    this.loading = false
    this.GetHolidayList();
  }


  Add(){
    this.route.navigate(["/app/holidayaddedit"]);
  }

  

  OnEdit(id){
    this.route.navigate(['/app/holidayaddedit'], {
      queryParams: { isEdit: 1, inspId: id },
    skipLocationChange:true
    });
  }
  


  public GetHolidayList() {    
    this.holidaylist=[];
    this.holidayService.GetHolidayList().subscribe((response: HolidayMaster[]) => {
      this.holidaylist = response;
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Inspection TYpe', 'Error');
     
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
  
  deleteHoliday(id:number) {
    var ans = confirm("Are you sure you want to delete this Holiday Detail?");
    if (ans == true) {      
      this.holidayService.DeleteHolidayById(id).subscribe((response: Response) => {
        if (response.statusText == "Fail") {
          this.toastr.success('There was some error deleting the record. Please try again later.');
        }
        else if (response.statusText === "Exists") {
          this.toastr.error('Holiday not deleted. It is attached to a Inspection Type');
        }
        else {
          this.toastr.success('Holiday deleted successfully');
        }        
        this.GetHolidayList();
      });
    }
  
  }
}
