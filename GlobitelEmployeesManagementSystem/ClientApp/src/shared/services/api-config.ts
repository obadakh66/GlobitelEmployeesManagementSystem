import { HttpHeaders } from '@angular/common/http';

export const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
    })
};


export const httpFormDataOptions = {
    headers: new HttpHeaders({
        // 'Content-Type': undefined
    })
};


export enum Controllers {
  Auth = 'Auth/',
  Employee = "Employee/"
}
















export enum Actions {
  GetList = 'GetList',
  ChangeStatus = 'ChangeStatus',
  Login = 'Login',
  Signup = 'Register',
  Submit = 'Create',
  ExportActiveEmployeesToExcel = "ExportActiveEmployeesToExcel",
  GetById = "GetById",
  Edit = "Edit",
  GetDashboardData = "GetDashboardData"
}
