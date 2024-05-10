import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { Inspection } from '../inspections-list/inspectionsmodel';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionService } from '../inspection.service';
import { NgbCalendar, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Table } from 'primeng/table';
import { MessageService, SelectItem } from 'primeng/api';
import { RegisterModel } from '../../registerusenew/registermodel';

@Component({
  selector: 'app-new-inspection',
  templateUrl: './new-inspection.component.html',
  styleUrl: './new-inspection.component.scss'
})
export class NewInspectionComponent {
  inspectionList:Inspection[];
  user: any;
  userRole:string
  fromDate:Date
  toDate:Date
  loading:boolean = false
  inspectionModel= new Inspection();
  display:boolean = false;
  switch1: boolean = false; 
  @ViewChild('filter') filter!: ElementRef;
  isResient : boolean  = false
  isManager : boolean = false
  isInspector : boolean = false
  userList:RegisterModel[];

  time:SelectItem[]
  showFullAddress:boolean = false
  constructor(
    private route:Router,
    private spinner: NgxSpinnerService,     
    public inspectionTypeService : InspectionService,
    private dateAdapter: NgbDateAdapter<string>,
    private ngbCalendar: NgbCalendar,
    public toastr:ToastrService,
    public authenticateservice: AuthenticationService,
    private mService: MessageService,
    private cdRef: ChangeDetectorRef) { }
    

    ngOnInit(){
      this.time = [ 
        {  
            label:'AM',
            value: 'AM' 
        }, 
        {  
            label:'PM',
            value: 'PM' 
        }, 
      ]; 
      
      this.user = JSON.parse(sessionStorage.getItem("currentUser"));
      this.userRole = this.user.role
      this.loading = false;
      
      var inspectionsearchfromDate = JSON.parse(sessionStorage.getItem("inspectionsearchfromDate"));
      var inspectionsearchToDate = JSON.parse(sessionStorage.getItem("inspectionsearchToDate"));

      if(inspectionsearchfromDate!=null)
      {
        this.fromDate = new Date(inspectionsearchfromDate);
      }else{
        this.fromDate = new Date()
      }
      if(inspectionsearchToDate!=null)
      {
        this.toDate = new Date(inspectionsearchToDate);
      }else{
        this.toDate = new Date()
        this.datepicker()
      }
      this.GetInspectiontList(this.fromDate,this.toDate)
      this.GetUserList();
      
      if(this.userRole == "Resident"){
        this.isResient = true
      }
      if(this.userRole == "Manager"){
        this.isManager = true
      }
      if(this.userRole == "Inspector"){
        this.isInspector = true
      }
      this.cdRef.detectChanges();
    }

    async saveTIme(event,inspectionId) :Promise<any> {
      return new Promise<void>((resolve, reject) => {    
      this.inspectionModel.AmPm = event.value
      this.inspectionTypeService.UpdateInspectionTime(inspectionId,this.inspectionModel.AmPm).subscribe(
        (response) => {
            resolve();  
        },
        (error) => {
          this.toastr.error("Inspection saved failed",'Error');
          reject()
        });
        
      
    });
    
  }

  async saveTime(event,inspectionId) :Promise<any> {
    return new Promise<void>((resolve, reject) => {    
    this.inspectionModel.AmPm = event.value
    this.inspectionTypeService.UpdateInspectionTime(inspectionId,this.inspectionModel.AmPm).subscribe(
      (response) => {
          resolve();  
      },
      (error) => {
        this.toastr.error("Inspection saved failed",'Error');
        reject()
      });
      
    
  });
  
}
async saveTDate(event,inspectionId) :Promise<any> {
  return new Promise<void>((resolve, reject) => {    
  this.inspectionModel.AmPm = event.value
  this.inspectionTypeService.UpdateInspectionDate(inspectionId,this.inspectionModel.AmPm).subscribe(
    (response) => {
        resolve();  
    },
    (error) => {
      this.toastr.error("Inspection saved failed",'Error');
      reject()
    });
    
  
});

}
    datepicker(){
   
      if(this.fromDate > new Date(this.toDate.setDate(this.toDate.getDate() + 60)) ){
        alert('Rango máximo 60 dias');
        window.location.reload();}
      }
  
    convert(str) {
      var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
      return [date.getFullYear(), mnth, day].join("-");
    }

    GetInspectionDetailsById(id: number) {
   
      this.inspectionTypeService.GetInspectionDetailsById(id).subscribe((response: Inspection) => {
        this.inspectionModel = response   
        
        this.inspectionModel.InspectionDate = new Date(this.inspectionModel.InspectionDate)
        this.inspectionModel.InspectionType1 = this.inspectionModel.InspectionType1
        this.inspectionModel.InspectionType2 = this.inspectionModel.InspectionType2
        this.inspectionModel.InspectionType3 = this.inspectionModel.InspectionType3      
      })
    }

    public GetInspectiontList(fromDate,toDate)
    {
      sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
      sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
      fromDate = this.convert(fromDate)
      toDate = this.convert(toDate)
      this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
        (response: Inspection[]) => {      
           let inspdate : any  
            this.inspectionList = response; 
            this.inspectionList = this.inspectionList.filter(x=>x.InspectionStatus == 'New')             
          
         this.inspectionList.forEach(element => {
            element.InspectionDate = new Date(element.InspectionDate);
        });
                            
        },
      
      );
    }

  GotoInsp(){
      this.route.navigate(["/app/inspectionformlist"]);
    }

    sendReject(values:any, id:string){
    this.inspectionModel.Id = id
    this.inspectionModel.IsRejected = values.currentTarget.checked
    

  }

  isAck(values:any, id:string):void {
    console.log(values.currentTarget.checked);
    const ackVALUE = values.currentTarget.checked
    const newackid = id
    this.inspectionTypeService.EditInspectionackValue(id,ackVALUE).subscribe((response) => {
     
      this.toastr.success('Inspection  Saved Successfully');
      
    });
   
  }

  rejectValue(){
    //this.inspectionModel.RejectionComments 
    if(this.inspectionModel.RejectionComments != '' || this.inspectionModel.RejectionComments != undefined )
    {
      this.inspectionTypeService.EditInspectionRejectValue(this.inspectionModel.Id,this.inspectionModel.IsRejected ,this.inspectionModel.RejectionComments)
      .subscribe((response) => {
     
        this.toastr.success('Inspection  Saved Successfully');      
        
      });
    }
    this.display = false
  }

  isReject(values:any, id:string){
    console.log(values.currentTarget.checked);
    const rejectVALUE = values.currentTarget.checked
    const newackid = id
    this.inspectionTypeService.EditInspectionackValue(id,rejectVALUE).subscribe((response) => {
     
      this.toastr.success('Inspection  Saved Successfully');      
      
    });
  }
  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    console.log("table", table)
  }

  clear(table: Table) {
    table.clear();
    this.filter.nativeElement.value = '';
  }

  GetUserList() {    
    this.authenticateservice.GetUserListforInspection().subscribe((response: RegisterModel[]) => {
      this.userList = response;
      this.userList = this.userList.filter(x=>x.RoleName == "Inspector" && x.IsActive == true)

      
      console.log(this.userList);
    }, (error:any)=> {
     
      console.log("error list");
    });
  }

  async saveInspector(event,inspectionId) :Promise<any> {
    return new Promise<void>((resolve, reject) => {    
      this.showFullAddress = true
    this.inspectionModel.InspectorName = event.value
    this.inspectionTypeService.UpdateInspectionInspector(inspectionId,this.inspectionModel.InspectorName).subscribe(
      (response) => {
          resolve();  
      },
      (error) => {
        this.toastr.error("Inspection saved failed",'Error');
        reject()
      });    
    });  
  }

  viewAddress(inspectionId){
    this.showFullAddress = true
  }
  
  async saveDate(event,inspectionId) :Promise<any> {
    return new Promise<void>((resolve, reject) => {  

      let inspdate = this.convert( event) 
    //this.inspectionModel.InspectionDate = event.

    this.inspectionTypeService.UpdateInspectionDate(inspectionId,inspdate).subscribe(
      (response) => {
          resolve();  
      },
      (error) => {
        this.toastr.error("Inspection saved failed",'Error');
        reject()
      });    
    });  
  }
}
