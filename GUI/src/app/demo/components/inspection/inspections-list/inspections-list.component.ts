import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Table } from 'primeng/table';
import { Customer, Representative } from 'src/app/demo/api/customer';
import { Product } from 'src/app/demo/api/product';
import { CustomerService } from 'src/app/demo/service/customer.service';
import { ProductService } from 'src/app/demo/service/product.service';
import { FromTODate, Inspection } from './inspectionsmodel';
import { InspectionService } from '../inspection.service';
import { NgbCalendar, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { InspectorForInspection, RegisterModel } from '../../registerusenew/registermodel';
import { InspectionTypes } from '../inspectionmodel';
interface expandedRows {
  [key: string]: boolean;
}
@Component({
  selector: 'app-inspections-list',
  
  templateUrl: './inspections-list.component.html',
  styleUrl: './inspections-list.component.scss'
})
export class InspectionsListComponent {
  loading: boolean = true;

  @ViewChild('filter') filter!: ElementRef;
  inspectionList:Inspection[];
  user:any;
  fromToDates: FromTODate = new FromTODate();
  fromDate: Date;
  toDate: Date;
  
  date3: Date;
  newFromDate :any
  display: boolean = false;
  inspId:string;
  inspectionModel= new Inspection();
  userRole:string;
  userList:RegisterModel[];
  userListForInspctor:InspectorForInspection[];
  selectall:InspectorForInspection
  inspectiontypes : InspectionTypes[];
  selectallType:InspectionTypes;
  inspectionInfo= new Inspection()
  isResient : boolean  = false
  isManager : boolean = false
  isInspector : boolean = false
  status = [
    {name:'Select All', code:'Select All'},
    { name: 'Other', code: 'Other' },
    { name: 'Passed', code: 'Passed' },
    { name: 'Failed', code: 'Failed' },
    { name: 'New', code: 'New' },

  ];
  InspStatus = [
    {name:'Select All', code:'Select All'},
    {name:'Acknowledge', code:'Acknowledge'},
    {name:'Rejected', code:'Rejected'}
  ]
  public windowRef: Window;
  constructor(private customerService: CustomerService,
     private route:Router,
     private spinner: NgxSpinnerService, 
     private productService: ProductService,
     public inspectionTypeService : InspectionService,
     private dateAdapter: NgbDateAdapter<string>,
     private ngbCalendar: NgbCalendar,
     public toastr:ToastrService,
     public authenticateservice: AuthenticationService,) { }

  ngOnInit() {
    
    this.user = JSON.parse(sessionStorage.getItem("currentUser"));
    this.windowRef = window;
    this.userRole = this.user.role
    this.newFromDate = new Date();
  //  this.fromDate = this.convert(this.newFromDate)
  //   this.toDate =  this.convert(this.newFromDate)
    this.GetUserList();
    this.GetUserListForInspection();
    this.GetInspectionTypeList();
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
    
    if(this.userRole == "Resident"){
      this.isResient = true
    }
    if(this.userRole == "Manager"){
      this.isManager = true
    }
    if(this.userRole == "Inspector"){
      this.isInspector = true
    }
    this.loading = false;
    if(this.userRole !== "Staff"){
      
      this.GetInspectiontList(this.fromDate,this.toDate)
    }
    else{
       this.GetInspectionListWithoutDate()
    }
    
   
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

  fromModel(value: string | null): NgbDateStruct | null {
    if (value) {      
      const date = value.split("-");
      return {
        month : parseInt(date[0], 10),
        day : parseInt(date[1], 10),
        year : parseInt(date[2], 10)
      };
    }
    return null;
  }

  GotoNewInsp(){
    this.route.navigate(["/app/newinspection"]);
  }

  GetUserListForInspection() {    
    this.authenticateservice.GetUserListforInspection().subscribe((response: InspectorForInspection[]) => {     
      this.selectall = {
        Id: '0', Name: "Select All", IsActive: true, RoleName: "Inspector"
      }
      this.userListForInspctor = response.filter(x=>x.RoleName == "Inspector" && x.IsActive == true);
      this.userListForInspctor.unshift(this.selectall)

      
    }, (error:any)=> {
     
      console.log("error list");
    });
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

  getInspections(event,inspector,fromDate,toDate){
    sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
    sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
    fromDate = this.convert(fromDate)
    toDate = this.convert(toDate)
    this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
      (response: Inspection[]) => {        
          this.inspectionList = response; 
          if(inspector != "Select All"){
            this.inspectionList = this.inspectionList.filter(x=>x.InspectorName == inspector) 
          }
            
          console.log(this.inspectionList)              
      },
    
    );
  }

  public GetInspectionTypeList() {    
    this.inspectiontypes=[];
    this.inspectionTypeService.GetInspectionTypeList().subscribe((response: InspectionTypes[]) => {
      this.selectallType = {
        Id: 0, InspectionTypeName: "Select All", IsActive: true
      }
      this.inspectiontypes = response
      this.inspectiontypes.unshift(this.selectallType)
      
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Inspection TYpe', 'Error');
      
    });
  }

  getInspectionFromType(event,inspector,fromDate,toDate){
    sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
    sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
    fromDate = this.convert(fromDate)
    toDate = this.convert(toDate)
    this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
      (response: Inspection[]) => {        
          this.inspectionList = response; 
          if(inspector != "Select All"){
            this.inspectionList = this.inspectionList.filter(x=>x.InspectionType1 == inspector || x.InspectionType2 == inspector ||
              x.InspectionType3 == inspector || x.InspectionType4 == inspector) 
          }
            
          console.log(this.inspectionList)              
      },
    
    );
    
  }

  getInspectionFromStatus(event,inspector,fromDate,toDate){
    sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
    sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
    fromDate = this.convert(fromDate)
    toDate = this.convert(toDate)
    this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
      (response: Inspection[]) => {        
          this.inspectionList = response; 
          if(inspector != "Select All"){
            this.inspectionList = this.inspectionList.filter(x=>x.Status1 == inspector || x.Status2 == inspector ||
              x.Status3 == inspector || x.Status4 == inspector) 
          }
            
          console.log(this.inspectionList)              
      },
    
    );
    
  }

  getInspFromStatus(event,inspector,fromDate,toDate){
    sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
    sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
    fromDate = this.convert(fromDate)
    toDate = this.convert(toDate)
    this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
      (response: Inspection[]) => {        
          this.inspectionList = response; 
          if(inspector != "Select All"){
            this.inspectionList = this.inspectionList.filter(x=>x.InspectionStatus == inspector ) 
          }
            
          console.log(this.inspectionList)              
      },
    
    );
    
  }

  AddInspection(){
    this.route.navigate(["/app/inspecform"]);
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

  getrejected(fromDate,toDate){
    sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
    sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
    fromDate = this.convert(fromDate)
    toDate = this.convert(toDate)
    this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
      (response: Inspection[]) => {        
          this.inspectionList = response; 
          this.inspectionList = this.inspectionList.filter(x=>x.IsRejected == true)                 
      },
    
    );
  }

  async saveInspector(event,inspectionId) :Promise<any> {
    return new Promise<void>((resolve, reject) => {    
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

  formatCurrency(value: number) {
      return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
  }

  onGlobalFilter(table: Table, event: Event) {
      table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  clear(table: Table) {
      table.clear();
      this.filter.nativeElement.value = '';
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

  public GetInspectiontList(fromDate,toDate)
  {
    sessionStorage.setItem("inspectionsearchfromDate",JSON.stringify(this.fromDate));
    sessionStorage.setItem("inspectionsearchToDate",JSON.stringify(this.toDate));
    fromDate = this.convert(fromDate)
    toDate = this.convert(toDate)
    this.inspectionTypeService.GetInspectiontList(fromDate,toDate).subscribe(
      (response: Inspection[]) => {        
        if(this.isResient == true){
          this.inspectionList = response;  
         this.inspectionList = this.inspectionList.filter(x=>x.CreatedBy == this.user.userId )
        }
        else{
          this.inspectionList = response;  
          this.inspectionList  = this.inspectionList .filter(x => x.InspectionStatus != "New");
        }
            

      },
    
    );
  }
  
  
  isAck(values:any, id:string):void {
    console.log(values.currentTarget.checked);
    const ackVALUE = values.currentTarget.checked
    const newackid = id
    this.inspectionTypeService.EditInspectionackValue(id,ackVALUE).subscribe((response) => {
     
      this.toastr.success('Inspection  Saved Successfully');
      
    });
   
  }

  sendReject(values:any, id:string){
    this.inspectionModel.Id = id
    this.inspectionModel.IsRejected = values.currentTarget.checked
    

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
  
  OnEdit(id){
    this.route.navigate(['/app/inspecform'], {
      queryParams: { inspectionId: id,
        isEdit: 1,               
        isView: 0 },
    // skipLocationChange:true
    });
  }

  OnDelete(id:number) {
    var ans = confirm("Are you sure you want to delete this Inspection?");
    if (ans == true) {      
      this.inspectionTypeService.DeleteInspection(id).subscribe((response: Response) => {
        if (response.statusText == "Fail") {
          this.toastr.success('There was some error deleting the record. Please try again later.');
        }
        else if (response.statusText === "Exists") {
          this.toastr.error('Inspection  not deleted. It is attached to a Inspection Type');
        }
        else {
          this.toastr.success('Inspection  deleted successfully');
        }        
        this.GetInspectiontList(this.fromDate,this.toDate)
      });
    }
  
  }
  
  // exportExcel() {
  //   import("xlsx").then(xlsx => {
  //       const worksheet = xlsx.utils.json_to_sheet(this.inspectionList);
  //       const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
  //       const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
  //       this.saveAsExcelFile(excelBuffer, "products");
  //   });
  // }
  // saveAsExcelFile(buffer: any, fileName: string): void {
  //   import("file-saver").then(FileSaver => {
  //     let EXCEL_TYPE =
  //       "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";
  //     let EXCEL_EXTENSION = ".xlsx";
  //     const data: Blob = new Blob([buffer], {
  //       type: EXCEL_TYPE
  //     });
  //     FileSaver.saveAs(
  //       data,
  //       fileName + "_export_" + new Date().getTime() + EXCEL_EXTENSION
  //     );
  //   });
  // }
  gotomap(){
    this.route.navigate(['/app/map'])
  }
  
}
