export class RegisterModel{
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

export class ResetPasswordModel{
     Email :string
     ConfirmPassword: string
     Password :string
     Code: string
     ActionType: string
}      