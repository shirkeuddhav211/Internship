import { Component } from '@angular/core';
import { Notice } from '../noticemodel';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { NoticeService } from '../notice.service';
import { NgForm } from '@angular/forms';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-notice-add-edit',
  templateUrl: './notice-add-edit.component.html',
  styleUrl: './notice-add-edit.component.scss'
})
export class NoticeAddEditComponent {
  notice = new Notice();
  userstorage: any;
  noticeId:string;
  isEditNotice:boolean = false;

  constructor(private toastr: ToastrService ,private route:ActivatedRoute,
    private router:Router,
    public noticeService:NoticeService/* ,private modalService : NgbModal,private editModal:ModalDismissReasons*/)
  {}

  ngOnInit(): void {
    this.userstorage = JSON.parse(sessionStorage.getItem('currentUser'));
    var isEdit = this.route.snapshot.pathFromRoot[1].queryParams['isEdit'];
    this.noticeId = this.route.snapshot.pathFromRoot[1].queryParams['noticeId'];

    if(isEdit == '1'){
      this.isEditNotice = true;
      this.GetNoticeById(+this.noticeId);
    }
  }

  close(){
    this.router.navigate(["/app/notice"]);
  }

  onSubmit(formData: any) {
    var notice = {
      Id: formData.value.Id,
      Notice: formData.value.Notice,    
    };
    this.saveNotices(notice, formData);
  }

  saveNotices(notice, formData: NgForm) {
    
    this.noticeService.AddNotice(notice).subscribe((response) => {
     
      this.toastr.success('Notice Saved Successfully');
      formData.reset();
      
    });
  }

  saveNotice() {
    if(this.isEditNotice == true){
      this.noticeService.EditNotice(this.notice).subscribe((response) => {
     
        this.toastr.success('Notice Saved Successfully');
        this.router.navigate(["/app/notice"]); 
        
      });
    }else{
      this.noticeService.AddNotice(this.notice).subscribe((response) => {
     
        this.toastr.success('Notice Saved Successfully');
        this.router.navigate(["/app/notice"]); 
        
      });
    }
    
  }

  private GetNoticeById(id: number) {
    
    this.noticeService.GetNoticeById(id).subscribe((response: Notice) => {
      this.notice = response
      
      this.notice.Id = id; 
      this.notice.Notice1 = response.Notice1
 
      
    });
  }

  OnDelete(index: number) {
    //this.taskarray.splice(index, 1);
  }
}
