import { Component, ElementRef, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { InspectionTypes } from '../inspection/inspectionmodel';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionService } from '../inspection/inspection.service';
import { ToastrService } from 'ngx-toastr';
import { NoticeService } from './notice.service';
import { NoticeType } from './NoticeModel';
@Component({
  selector: 'app-notice',
  // standalone: true,
  // imports: [],
  templateUrl: './notice.component.html',
  styleUrl: './notice.component.scss'
})
export class NoticeComponent {
  noticetypes : NoticeType[]; 
  inspectiontypes : InspectionTypes[];
  taskarray=[{type:'notice1', check : true}]
  rowGroupMetadata: any;
  UserData  : any;
  activityValues: number[] = [0, 100];
  loading: boolean = true;
  @ViewChild('filter') filter!: ElementRef;

  constructor(private route:Router,private spinner: NgxSpinnerService, public inspectionTypeService: InspectionService, public noticeService : NoticeService,
    public toastr:ToastrService) { }

  ngOnInit() {
    this.loading = false
    this.GetNoticeList();
  }


  Add(){
    this.route.navigate(["/app/noticeaddedit"]);
  }

  OnDelete(index: number) {
    this.taskarray.splice(index, 1);
  }

  OnEdit(id){
    this.route.navigate(['/app/noticeaddedit'], {
      queryParams: { isEdit: 1, inspId: id },
    skipLocationChange:true
    });
  }

  public GetNoticeList() {    
    this.noticetypes=[];
    this.noticeService.GetNoticeList().subscribe((response: NoticeType[]) => {
      this.noticetypes = response;
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
    var ans = confirm("Are you sure you want to delete this Notice Category?");
    if (ans == true) {      
      this.noticeService.DeleteNoticeById(id).subscribe((response: Response) => {
        if (response.statusText == "Fail") {
          this.toastr.success('There was some error deleting the record. Please try again later.');
        }
        else if (response.statusText === "Exists") {
          this.toastr.error('Notice Category not deleted. It is attached to a Notice Type');
        }
        else {
          this.toastr.success('Notice Type deleted successfully');
        }        
        this.GetNoticeList();
      });
    }
  
  }
}
