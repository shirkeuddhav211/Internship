import { Component } from '@angular/core';
import { HolidayMaster } from '../holidaymodel';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { HolidayService } from '../holiday.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-holidaymasteradd',  
  templateUrl: './holidaymasteradd.component.html',
  styleUrl: './holidaymasteradd.component.scss'
})
export class HolidaymasteraddComponent {
  holidayModel = new HolidayMaster();
  userstorage: any;
  inspId:string;
  isEditHoliday:boolean = false;

  constructor(private toastr: ToastrService ,private route:ActivatedRoute,
    private router:Router,
    public holidayService:HolidayService/* ,private modalService : NgbModal,private editModal:ModalDismissReasons*/)
  {}

  ngOnInit(): void {
    this.userstorage = JSON.parse(sessionStorage.getItem('currentUser'));
    var isEdit = this.route.snapshot.pathFromRoot[1].queryParams['isEdit'];
    this.inspId = this.route.snapshot.pathFromRoot[1].queryParams['inspId'];

    if(isEdit == '1'){
      this.isEditHoliday = true;
      this.GetHolidayById(+this.inspId);
    }
  }
  

  close(){
    this.router.navigate(["/app/holidaylist"]);
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
    
    this.holidayService.AddHoliday(inspectionType).subscribe((response) => {
     
      this.toastr.success('Inspection Type Saved Successfully');
      formData.reset();
      
    });
  }

  saveHoliday() {
    if(this.isEditHoliday == true){
      this.holidayService.EditHoliday(this.holidayModel).subscribe((response) => {
        this.holidayModel.HolidayDate = new Date(this.holidayModel.HolidayDate)
        this.toastr.success('Holiday Saved Successfully');
        this.router.navigate(["/app/holidaylist"]); 
        
      });
    }else{
      this.holidayService.AddHoliday(this.holidayModel).subscribe((response) => {
     
        this.toastr.success('Holiday detail Saved Successfully');
        this.router.navigate(["/app/holidaylist"]); 
        
      });
    }
    
  }

  private GetHolidayById(id: number) {
    
    this.holidayService.GetHolidayById(id).subscribe((response: HolidayMaster) => {
      this.holidayModel = response
      
      this.holidayModel.Id = id; 
      this.holidayModel.HolidayDate = new Date(response.HolidayDate)
      this.holidayModel.Description = response.Description    
      
    });
  }
  

  OnDelete(index: number) {
    //this.taskarray.splice(index, 1);
  }
}
