import { Component } from '@angular/core';
import { Inspection } from '../inspectionsmodel';
import { HttpClient } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { RegisteruserService } from '../../../registerusenew/registeruser.service';
import { InspectionService } from '../../inspection.service';
import { InspectionTypes } from '../../inspectionmodel';
import { RoleViewModel } from '../../../auth/user.model';
import { RegisterModel, RegisterModelNew } from '../../../registerusenew/registermodel';
import { HolidayMaster } from '../../../holidaymaster/holidaymodel';
import { HolidayService } from '../../../holidaymaster/holiday.service';


@Component({
  selector: 'app-inspectionaddedit',  
  templateUrl: './inspectionaddedit.component.html',
  styleUrl: './inspectionaddedit.component.scss'
})
export class InspectionaddeditComponent {
  inspectionInfo= new Inspection()
  inspectiontypes : InspectionTypes[];
  activeInspectionTypes: InspectionTypes[];
  roleList:RoleViewModel[];
  userList:RegisterModel[];
  Register= new RegisterModel()
  date3: Date;

  holidaylist : HolidayMaster[]; 
  
  role = [
    { name: 'Type1', code: 'Option 1' },
    { name: 'type2', code: 'Option 2' },
    { name: 'Type3', code: 'Option 3' }
];

status = [
  { name: 'Other', code: 'Other' },
  { name: 'Passed', code: 'Passed' },
  { name: 'Failed', code: 'Failed' },
  { name: 'New', code: 'New' }
];
  user: any;
  inspectionId:string
  isAm : boolean = false
  isType:boolean = false
  display: boolean = false;
  userRole:string;
  newdate1:string
  newdate2 :string
  datetest:boolean=false
  isResient:boolean = false

constructor(private httpClient: HttpClient , 
  private spinner: NgxSpinnerService, 
  private route: ActivatedRoute,
  private toastr: ToastrService,
  public router: Router,
  public authenticateservice: AuthenticationService,
  public registeruserservice:RegisteruserService,
  public inspectionTypeService :InspectionService,
  public holidayService: HolidayService,) {}

  ngOnInit(){
    let today = new Date();
    let month = today.getMonth();

    this.user = JSON.parse(sessionStorage.getItem('currentUser'));
    var isEdit = this.route.snapshot.pathFromRoot[1].queryParams['isEdit'];
    this.inspectionId = this.route.snapshot.pathFromRoot[1].queryParams['inspectionId'];
    this.userRole = this.user.role
    this.Register.City = "National City";
    this.Register.State = "California"
    if(isEdit == '1'){
      this.GetInspectionDetailsById(+this.inspectionId)
    }
    
    this.GetInspectionTypeList();
    this.GetRoleList();
    this.GetUserList();
    this.GetHolidayList();
    if(this.user.role == "Resident"){
      this.GetUserById(this.user.userId)
      this.isResient = true
    }
    
    this.inspectionInfo.UpdatedBy = this.user.userId
    this.inspectionInfo.CreatedBy = this.user.userId

   // this.activeInspectionTypes = this.inspectiontypes.filter(type => type.InspectionVideo === true);

  }

  checkStatus(status){
    if(status == 'AM'){
      this.isAm = true;
      this.inspectionInfo.AmPm = 'AM'
    }else{
      this.isAm = false
      this.inspectionInfo.AmPm = 'PM'
    }
  }

  checkType(type){
    if(type == 'Residential'){
      this.isType = true
      this.inspectionInfo.Type ='Residential'
    }else{
      this.isType = false
      this.inspectionInfo.Type ='Commercial'
    }
  }
  onSubmit(){
   this.inspectionInfo.FirstName = this.Register.FirstName
   this.inspectionInfo.LastName = this.Register.LastName
   this.inspectionInfo.Email = this.Register.Email
   this.inspectionInfo.AddressLine1 = this.Register.Address
   this.inspectionInfo.Apartment = this.Register.Apartment
   this.inspectionInfo.PhoneNumber = this.Register.PhoneNumber
   this.inspectionInfo.City = this.Register.City
   this.inspectionInfo.State = this.Register.State
   this.inspectionInfo.Zip = this.Register.Zip
    this.inspectionInfo.InspectionType1 = this.inspectionInfo.InspectionType1
    this.inspectionInfo.InspectionType2 = this.inspectionInfo.InspectionType2
    this.inspectionInfo.InspectionType3 = this.inspectionInfo.InspectionType3
    this.inspectionInfo.InspectionType4 = this.inspectionInfo.InspectionType4
    if(this.inspectionInfo.InspectionStatus == ''||this.inspectionInfo.InspectionStatus == undefined || this.inspectionInfo.InspectionStatus == null){
      this.inspectionInfo.InspectionStatus = 'New'
    }
    if((this.inspectionInfo.InspectionType1 == undefined || this.inspectionInfo.InspectionType1 == '' )&&
       (this.inspectionInfo.InspectionType2 == undefined || this.inspectionInfo.InspectionType2 == '' ) &&
       (this.inspectionInfo.InspectionType3 == undefined || this.inspectionInfo.InspectionType3 == '') &&
       (this.inspectionInfo.InspectionType4 == undefined || this.inspectionInfo.InspectionType4 == '')) {     
           this.toastr.error("Please select at least one inspection type",'Error');
        
    }else if (this.inspectionInfo.AmPm == '' || this.inspectionInfo.AmPm == undefined){
          this.toastr.error("Please select Time",'Error');
        }else if(this.inspectionInfo.PermitNo == '' || this.inspectionInfo.PermitNo == undefined){
          this.toastr.error("Please select Permit",'Error');
        }else if(this.inspectionInfo.Type == '' || this.inspectionInfo.Type == undefined){
          this.toastr.error("Please select Inspection For",'Error');
        }else if(this.inspectionInfo.InspectionDate == null){
          this.toastr.error("Please select Date",'Error');
        }
        else{
          this.inspectionTypeService.AddInspectionDetail(this.inspectionInfo).subscribe(
            (response:string) => {
              this.spinner.show();        
                this.spinner.hide();
                this.toastr.success("Inspection saved successfully.",'Information');
                this.router.navigate(["/app/inspectionformlist"])
                // .then(() => {
                //   this.router.navigate(['/app/inspecform'], {
                //     queryParams: {
                //       inspectionId: +response,
                //       isEdit: 1,               
                //       isView: 0
                //     }
                //   });
                // });
                //this.router.navigate(["/app/inspectionlist"]);
            },
            (error) => {
              this.toastr.error("Inspection saved failed",'Error');
            }
          );
        }
    
  }

  close(){
    this.router.navigate(["/app/inspectionformlist"]);
  }
  public GetInspectionTypeList() {    
    this.inspectiontypes=[];
    this.inspectionTypeService.GetInspectionTypeList().subscribe((response: InspectionTypes[]) => {
      this.inspectiontypes = response;
      this.activeInspectionTypes = this.inspectiontypes.filter(x=>x.InspectionVideo == true)

    },
    (error:any)=> {
      this.toastr.error('Error while fetching Inspection TYpe', 'Error');
      
    });
  }

  setDefaultValue(event:any){
    this.inspectionInfo.AmPm = 'AM'; // Set your default radio option here
  }

  isPmDisabled(): boolean {
    // Disable the PM radio button if AM is selected by default
    return this.inspectionInfo.AmPm === 'AM';
  }

  convert(str) {
    var date = new Date(str),
      mnth = ("0" + (date.getMonth() + 1)).slice(-2),
      day = ("0" + date.getDate()).slice(-2);     
    return [date.getFullYear(), mnth, day].join("-");
  }

  GetUserById(id) {
    this.authenticateservice.getUserById(id).subscribe((response: any) => {
      this.Register = response;
    });
  }
  
  public GetRoleList() {
    
    this.authenticateservice.GetAllRolesList().subscribe((response: RoleViewModel[]) => {
      console.log(response)
      this.roleList = response;
      this.roleList = this.roleList.filter(x=>x.Name == "Inspector");
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

  private GetInspectionDetailsById(id: number) {
    
    this.inspectionTypeService.GetInspectionDetailsById(id).subscribe((response: Inspection) => {
      this.inspectionInfo = response   
      // var n1 = response.InspectionDate.split('T');
      // const n2 = n1[0] 
      // this.inspectionInfo.InspectionDate = n2;
      // this.inspectionInfo.InspectionDate = this.convert(this.inspectionInfo.InspectionDate)
      this.Register.FirstName = this.inspectionInfo.FirstName
      this.Register.LastName = this.inspectionInfo.LastName
      this.Register.Address = this.inspectionInfo.AddressLine1
      this.Register.PhoneNumber = this.inspectionInfo.PhoneNumber
      this.Register.Email = this.inspectionInfo.Email
      this.Register.City = this.inspectionInfo.City
      this.Register.State = this.inspectionInfo.State
      this.Register.Apartment = this.inspectionInfo.Apartment
      this.inspectionInfo.InspectionDate = new Date(this.inspectionInfo.InspectionDate)
      this.inspectionInfo.InspectionType1 = this.inspectionInfo.InspectionType1
      this.inspectionInfo.InspectionType2 = this.inspectionInfo.InspectionType2
      this.inspectionInfo.InspectionType3 = this.inspectionInfo.InspectionType3
      this.inspectionInfo.InspectionType4 = this.inspectionInfo.InspectionType4 
      
    });
  }

  formatUserPhoneNumber() {
    if( this.inspectionInfo.PhoneNumber != null){
      let valLength = this.inspectionInfo.PhoneNumber.length;
      if (valLength == 1) {
        this.inspectionInfo.PhoneNumber = "(" + this.inspectionInfo.PhoneNumber;
      }
      if (valLength == 4) {
        this.inspectionInfo.PhoneNumber = this.inspectionInfo.PhoneNumber + ") ";
      }
      if (valLength == 9) {
        this.inspectionInfo.PhoneNumber = this.inspectionInfo.PhoneNumber + "-";
      }
    }
    
  }

  public GetHolidayList() {    
    this.holidaylist=[];
    this.holidayService.GetHolidayList().subscribe((response: HolidayMaster[]) => {
      this.holidaylist = response;
      //this.holidaylist = this.holidaylist.filter( new Date(x.HolidayDate))
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Holiday List', 'Error');
     
    });
   }

   onEmailChange(event: any){
    const EMAIL_REGEXP = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    if (this.Register.Email !== '' && (this.Register.Email.length <= 5 || !EMAIL_REGEXP.test(this.Register.Email))) {
      this.toastr.warning("Please enter valid email",'information');
    }
  
    return "true";
   }
   
  checkHoliday(){
    var abc = this.inspectionInfo.InspectionDate
    var test =this.convert(abc)
    var newt = 'T00:00:00'
    const testy = test + newt
    //this.datetest = false
    this.holidaylist.forEach(element => {
     this.newdate1 = element.HolidayDate.toString()
      this.newdate2 = testy.toString()
       if( this.newdate1 == this.newdate2)
       {
        this.datetest = true         
       }
    });
   
    
    if(this.datetest == true){
      this.toastr.error('Sorry, our offices are closed for the selected date. Please select another date', 'Error');
     this.inspectionInfo.InspectionDate = undefined
     this.datetest = false
    }
  }
}
