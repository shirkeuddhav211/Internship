import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Table } from 'primeng/table';
import { RegisterModel } from '../registerusenew/registermodel';
import { HttpClient } from '@angular/common/http';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { RoleViewModel } from '../auth/user.model';

@Component({
  selector: 'app-residentuser',
  templateUrl: './residentuser.component.html',
  styleUrl: './residentuser.component.scss'
})
export class ResidentuserComponent {
  Register= new RegisterModel()
  userId : string
 
   selectedDrop: string = '';
   isEditUser:boolean= false
   userstorage: any;

   deparments = [
    { name: 'HR', code: 'HR' },
    { name: 'IT', code: 'IT' },
    { name: 'Software', code: 'Software' },
    { name: 'Hardware', code: 'Hardware' }
  ];
  
  loading: boolean = true;
  roleList:RoleViewModel[];
  isResident:boolean = false
  
  @ViewChild('filter') filter!: ElementRef;

  constructor(private httpClient: HttpClient , 
    private spinner: NgxSpinnerService, 
    private route: ActivatedRoute,
    private toastr: ToastrService,
    public authenticateservice: AuthenticationService,
    private router:Router) { }

  ngOnInit() {
    this.loading = false
    this.GetRoleList();
    this.Register.City = "National City";
    this.Register.State = "California"
    this.Register.RoleName = "Resident" 
  }

  onRegister() {
    if(this.isEditUser == true){
      this.Register.UpdatedBy = this.userstorage.userId
      this.authenticateservice.EditUser(this.Register).subscribe(
        (response:Response) => {
          this.spinner.show();
          this.toastr.success("Updated Successfully",'information');
            this.router.navigate(["/app/list1"]); 
        },
        (error) => {
          this.toastr.error("Registration failed",'Error');
        }
      );
    }else{
      this.Register.Role = "Resident"
      this.Register.UserName = this.Register.Email
      if(this.Register.FirstName == '' || this.Register.FirstName == undefined){
        this.toastr.error("Please enter FirstName",'Error');
      }
      else if(this.Register.LastName == '' || this.Register.LastName == undefined)
      {
        this.toastr.error("Please enter LastName",'Error');
      }else if(this.Register.Address == '' || this.Register.Address == undefined)
      {
        this.toastr.error("Please enter Address",'Error');
      }else if(this.Register.Email == '' || this.Register.Email == undefined)
      {
        this.toastr.error("Please enter Email",'Error');
      }else if(this.Register.UserName == '' || this.Register.UserName == undefined)
      {
        this.toastr.error("Please enter UserName",'Error');
      }else if(this.Register.newPassword == '' || this.Register.newPassword == undefined)
      {
        this.toastr.error("Please enter Password",'Error');
      }else if(this.Register.RoleName == '' || this.Register.RoleName == undefined)
      {
        this.toastr.error("Please select Role",'Error');
      }else if(this.Register.PhoneNumber == '' || this.Register.PhoneNumber == undefined)
      {
        this.toastr.error("Please enter PhoneNumber",'Error');
      }else if(this.Register.Zip == '' || this.Register.Zip == undefined)
      {
        this.toastr.error("Please enter Zip code",'Error');
      }
      else{
      //this.Register.UpdatedBy = this.userstorage.userId
      
      this.authenticateservice.registerUser(this.Register).subscribe(
        (response:Response) => {
          this.spinner.show();
          
            this.spinner.hide();
            this.toastr.success("Register Successfully",'information');
            this.router.navigate(["/"]); 
        },
        (error) => {
          this.toastr.error("Registration failed",'Error');
        }
      );
      }
    }
    
  }
  
  public GetRoleList() {
    
    this.authenticateservice.GetAllRolesList().subscribe((response: RoleViewModel[]) => {
      console.log(response)
      this.roleList = response;
      this.roleList = this.roleList.filter(x=>x.Name == "Resident")
     
    }, (error:any)=> {
      console.log("error list");
     
    });
  }

  onEmailChange(event: any){
    const EMAIL_REGEXP = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    if (this.Register.Email !== '' && (this.Register.Email.length <= 5 || !EMAIL_REGEXP.test(this.Register.Email))) {
      this.toastr.warning("Please enter valid email",'information');
    }
  
    return "true";
   }

  // public GetInspectionTypeList() {    
  //   this.inspectiontypes=[];
  //   this.inspectionTypeService.GetInspectionTypeList().subscribe((response: InspectionTypes[]) => {
  //     this.inspectiontypes = response;
  //   },
  //   (error:any)=> {
  //     this.toastr.error('Error while fetching Inspection TYpe', 'Error');
  //     //this.spinner.hide();
  //   });
  // }
 

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
  
  // deleteInspectionType(id:number) {
  //   var ans = confirm("Are you sure you want to delete this Inspection Category?");
  //   if (ans == true) {      
  //     this.inspectionTypeService.DeleteInspectionById(id).subscribe((response: Response) => {
  //       if (response.statusText == "Fail") {
  //         this.toastr.success('There was some error deleting the record. Please try again later.');
  //       }
  //       else if (response.statusText === "Exists") {
  //         this.toastr.error('Inspection Category not deleted. It is attached to a Inspection Type');
  //       }
  //       else {
  //         this.toastr.success('Inspection Type deleted successfully');
  //       }        
  //       this.GetInspectionTypeList();
  //     });
  //   }
  
  // }
  close(){
    this.router.navigate(["/"]);
  }
}
