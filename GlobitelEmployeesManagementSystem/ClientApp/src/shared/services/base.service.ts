import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Actions, Controllers, httpOptions } from './api-config';
const apiPreLink = environment.apiPreLink;
@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private http: HttpClient) {

  }



  public getList(controllerName: string): Observable<any> {
    return this.http.get(apiPreLink + controllerName + Actions.GetList);
  }

  public getListWithoutFilter(controllerName: string): Observable<any> {
    return this.http.get(apiPreLink + controllerName + Actions.GetList);
  }

  public changeStatus(id, statusId): Observable<any> {
    return this.http.get(apiPreLink + Controllers.Employee + Actions.ChangeStatus + '?employeeId=' + id);
  }
  public exportToExcel(lang: number): Observable<any> {
    return this.http.get(apiPreLink + Controllers.Employee + Actions.ExportActiveEmployeesToExcel + '?language=' + lang, { responseType: 'arraybuffer' });
  }

  public getById(id: number): Observable<any> {
    return this.http.get(apiPreLink + Controllers.Employee + Actions.GetById + '?employeeId=' + id);
  }
  public getDashboardData(): Observable<any> {
    return this.http.get(apiPreLink + Controllers.Employee + Actions.GetDashboardData);
  }

  public postItem(controllerName: string, actionName: string, postObject: any): Observable<any> {
    return this.http.post(apiPreLink + controllerName + actionName, JSON.stringify(postObject), httpOptions);
  }


}
