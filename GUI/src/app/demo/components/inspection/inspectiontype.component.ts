import { Component, OnInit ,ViewChild  } from '@angular/core';
import { NgForm } from '@angular/forms';
//import * as $ from 'jquery';
import { ToastrService } from 'ngx-toastr';
import { InspectionService } from './inspection.service';
import { InspectionTypes } from './inspectionmodel';
import { ActivatedRoute, Router } from '@angular/router';

//import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
//import { Placement as PopperPlacement, Options } from '@popperjs/core';

@Component({
  selector: 'app-inspectiontype',
  templateUrl: './inspectiontype.component.html',
  styleUrl: './inspectiontype.component.scss'
})
export class InspectiontypeComponent {  
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
    }else {
      this.inspectionType.IsActive = true; // Set default value for IsActive to true for new inspection types
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
  // deparments = [
  //   { name: 'Inspection Type 1', code: 'Option 1' },
  //   { name: 'Inspection Type 2', code: 'Option 2' },
  //   { name: 'Inspection Type 3', code: 'Option 2' },
  //   { name: 'Inspection Type 4', code: 'Option 3' }
  // ]; 
}
