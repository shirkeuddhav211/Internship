import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Customer, Representative } from '../../api/customer';
import { Product } from '../../api/product';
import { CustomerService } from '../../service/customer.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductService } from '../../service/product.service';
import { Table } from 'primeng/table';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { RegisterModel, RegisterModelNew } from '../registerusenew/registermodel';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent {
  
  UserData  : any; 
  //activityValues: number[] = [0, 100];

 
  isResident:boolean = false
  loading: boolean = true;
  userstorage:any
  @ViewChild('filter') filter!: ElementRef;

  userList:RegisterModel[];
  
  
  constructor(private customerService: CustomerService, 
    private route:Router,private spinner: NgxSpinnerService, private productService: ProductService,
    public authenticateservice: AuthenticationService,
    private toastr: ToastrService,) { }

  ngOnInit() {
    this.userstorage = JSON.parse(sessionStorage.getItem('currentUser'));
    this.GetUserList()     
    this.loading = false;
    if(this.userstorage.role == "Resident"){
      this.isResident = true
    }
  }

  GetUserList() {    
    this.authenticateservice.GetUserList().subscribe((response: RegisterModel[]) => {
      this.userList = response;
      this.userList = this.userList.filter(x=>x.RoleName !== "Resident");
      
    }, (error:any)=> {
     
      console.log("error list");
    });
  }


  change(event:any){
    console.log("hfg"+ event.checked)
    if(event.checked == true){
      this.authenticateservice.GetUserList().subscribe((response: RegisterModel[]) => {
        this.userList = response;
        //this.userList = this.userList(.filter(x=>x.RoleName == "Resident"));
        
      }, (error:any)=> {
       
        console.log("error list");
      });
    }else{
      this.GetUserList()
    }
   
  }
  OnEdit(id) {
    const method = 'e';
    this.route.navigate(['/app/register'], {
      queryParams: { isEdit: 1, userId: id },
    skipLocationChange:true
    });
    
  }

  Add(){
    this.route.navigate(["/app/register"]);
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

  DeleteUser(id) {
    var ans = confirm("Do you want to delete User?");
    if (ans == true) {
      //this.spinner.show()
      this.authenticateservice.DeleteUserByid(id).subscribe((response: Response) => {
        //this.spinner.hide();
        this.toastr.success("User Deleted Successfully");
       
        this.GetUserList();
      }, (error:any)=> {
        //this.spinner.hide();
        console.log("error list");
      });
    }
  }

  ActivateUser(id:any) {
    var ans = confirm("Do you want to activate User?");
    if (ans == true) {
      //this.spinner.show()
      this.authenticateservice.activateUserByid(id).subscribe((response: Response) => {
        //this.spinner.hide();
        this.toastr.success("User Activated Successfully");        
        this.GetUserList();
      }, (error:any)=> {
        //this.spinner.hide();
        console.log("error list");
      });
    }
  }
}
