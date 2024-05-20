import { Component,ElementRef, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { Customer } from '../../api/customer';
import { CustomerService } from '../../service/customer.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NoticeService } from './notice.service';
import { Notice } from './noticemodel';

@Component({
  selector: 'app-notice',
  templateUrl: './notice.component.html',
  styleUrl: './notice.component.scss'
})
export class NoticeComponent {
  notice : Notice[];
  loading: boolean = true;
  taskarray=[{type:'notice', check : true}]
  @ViewChild('filter') filter!: ElementRef;

  constructor(private route:Router,private spinner: NgxSpinnerService, public noticeService: NoticeService,
    public toastr:ToastrService) { }

  ngOnInit() {
    this.loading = false
    this.GetNoticeList();
  }


  GetNoticeList() {
    this.notice=[];
    this.noticeService.GetNoticeList().subscribe((response: Notice[]) => {
      //this.notice = response;
      response.forEach((notice, index) => {
        if (index === 0) {
          this.notice.push(notice); // Assign the first element of the response array to this.notice
        }
      });      
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Notice', 'Error');
      //this.spinner.hide();
    });
  }

  
  Add(){
    this.route.navigate(["/app/noticaddedit"]);
  }

  OnDelete(index: number) {
    this.taskarray.splice(index, 1);
  }

  OnEdit(id){
    this.route.navigate(['/app/noticaddedit'], {
      queryParams: { isEdit: 1, noticeId: id },
    skipLocationChange:true
    });
  }

  clear(table: Table) {
    table.clear();
    this.filter.nativeElement.value = '';
}

deleteNotice(id:number) {
  var ans = confirm("Are you sure you want to delete this Notice?");
  if (ans == true) {      
    this.noticeService.DeleteNoticeById(id).subscribe((response: Response) => {
      if (response.statusText == "Fail") {
        this.toastr.success('There was some error deleting the record. Please try again later.');
      }
      else if (response.statusText === "Exists") {
        this.toastr.error('Notice not deleted. It is attached to a Notice');
      }
      else {
        this.toastr.success('Notice deleted successfully');
      }        
      this.GetNoticeList();
    });
  }

}
}
