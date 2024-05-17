import { Component } from '@angular/core';
import { InspectionTypes } from '../inspection/inspectionmodel';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { InspectionService } from '../inspection/inspection.service';
import { NgForm } from '@angular/forms';
import { NoticeType } from './NoticeModel';

@Component({
  selector: 'app-noticeaddedit',
  // standalone: true,
  // imports: [],
  templateUrl: './noticeaddedit.component.html',
  styleUrl: './noticeaddedit.component.scss'
})
export class NoticeaddeditComponent {
  noticetype = new NoticeType();
  inspectionType = new InspectionTypes();
  userstorage: any;
  inspId:string;
  isEditInsp:boolean = false;

  constructor(private toastr: ToastrService ,private route:ActivatedRoute,
    private router:Router,
    public inspectionTypeService:InspectionService/* ,private modalService : NgbModal,private editModal:ModalDismissReasons*/)
  {}

  ngOnInit(): void {
    this.userstorage = JSON.parse(sessionStorage.getItem('currentUser'));
    var isEdit = this.route.snapshot.pathFromRoot[1].queryParams['isEdit'];
    this.inspId = this.route.snapshot.pathFromRoot[1].queryParams['inspId'];

    if(isEdit == '1'){
      this.isEditInsp = true;
      this.GetInspectionTypeById(+this.inspId);
    }
  }
  

  close(){
    this.router.navigate(["/app/inspectionlist"]);
  }

  onSubmit(formData: any) {
    var inspectiontype = {
      Id: formData.value.Id,
      InspectionTypeName: formData.value.InspectionTypeName,
      IsActive:formData.value.IsActive      
    };
    this.saveInspectionType(inspectiontype, formData);
  }

  saveInspectionType(inspectionType, formData: NgForm) {
    
    this.inspectionTypeService.AddInspectiType(inspectionType).subscribe((response) => {
     
      this.toastr.success('Inspection Type Saved Successfully');
      formData.reset();
      
    });
  }

  saveInspection() {
    if(this.isEditInsp == true){
      this.inspectionTypeService.EditInspectionType(this.inspectionType).subscribe((response) => {
     
        this.toastr.success('Inspection Type Saved Successfully');
        this.router.navigate(["/app/inspectionlist"]); 
        
      });
    }else{
      this.inspectionTypeService.AddInspectiType(this.inspectionType).subscribe((response) => {
     
        this.toastr.success('Inspection Type Saved Successfully');
        this.router.navigate(["/app/inspectionlist"]); 
        
      });
    }
    
  }

  private GetInspectionTypeById(id: number) {
    
    this.inspectionTypeService.GetInspectionTypeById(id).subscribe((response: InspectionTypes) => {
      this.inspectionType = response
      
      this.inspectionType.Id = id; 
      this.inspectionType.InspectionTypeName = response.InspectionTypeName
      this.inspectionType.IsActive = response.IsActive    
      
    });
  }
  // OnEdit(index: number) {
  //   this.editIndex = index;
    
  //   this.modalService.open(this.editModal, { centered: true });
  // }

  // OnUpdate(form: NgForm) {
  //   if (this.editIndex !== null) {
     
  //     this.taskarray[this.editIndex] = {
  //       type: form.controls['type'].value,
  //       check: form.controls['check'].value
  //     };

    
  //     this.modalService.dismissAll();
  //   }
  // }

  OnDelete(index: number) {
    //this.taskarray.splice(index, 1);
  }
}
