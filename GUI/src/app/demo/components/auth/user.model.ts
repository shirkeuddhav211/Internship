export class User {
    token?: string;
    id: string = "";
    userName: string = "";
    email: string = "";
    phoneNumber: string = "";
    firstName: string = "";
    lastName: string = "";
    address: string = "";
    password: string = "";
    isActive: boolean = true;
    roleId: string = "";
    roleName: string = "";
    displayUserName: string = "";
}


export class CurrentUser {
    id?: string;
    authToken?: string;
    firstName?: string;
    lastName?: string;
    email?: string;
    expiresIn?: number;
    role?: string;
    userId?: string;
}


export class AddEditUserViewModel {
    Id: string = "";
    UserName: string = "";
    Email: string = "";
    PhoneNumber: string = "";
    FirstName: string = "";
    LastName: string = "";
    Address: string = "";
    Password: string = "";
    IsActive: boolean = true;
    RoleId: string = "";
    RoleName: string = "";
    DisplayUserName: string = "";
}

export class RoleViewModel {
    Id: string = "";
    Name: string = "";

}


export class Userstatus {
    Id: number = 0;
    Status: string = "";
    DisplayName: string = "";
}