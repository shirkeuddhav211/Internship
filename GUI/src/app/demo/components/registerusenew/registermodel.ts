import { cl } from "@fullcalendar/core/internal-common";

export class RegisterModel{
    Id:string;
    UserName:string;
    Repassword:string;
    FirstName:string;
    LastName:string;
    Department:string;
    Role:string;
    Email:string;
    PhoneNumber:string
    Address:string;
    RoleId:string;
    IsActive:boolean;
    RoleName:string; 
    DisplayUserName:string;
    Alias:string;
    UpdatedBy:string;
    UpdatedDate:Date;
    AddressLine1:string;
    AddressLine2:string;
    City:string;
    Password:string;
    newPassword:string ="xxzzxx";
    Name:string;
    State:string;
    Apartment:string;
    Zip:string
}


export class RegisterModelNew{
    id:string;
    userName:string;
    repassword:string;
    firstName:string;
    lastName:string;
    department:string;
    role:string;
    email:string;
    phoneNumber:string
    address:string;
    roleId:string;
    isActive:boolean;
    roleName:string; 
    displayUserName:string;
    alias:string;
    updatedBy:string;
    updatedDate:Date;
    addressLine1:string;
    addressLine2:string;
    city:string;
    password:string
    state:string
}

export class InspectorForInspection{
    Id: string;
    Name: string;
    IsActive: boolean;
    RoleName:string
}