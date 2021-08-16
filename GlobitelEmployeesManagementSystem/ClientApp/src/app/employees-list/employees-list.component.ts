import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from '../../auth/authorize.service';
import { Actions, Controllers } from '../../shared/services/api-config';
import { BaseService } from '../../shared/services/base.service';
import { STATUS } from '../../shared/services/lookups';
import { NotificationService } from '../../shared/services/notification.service';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.scss']
})
/** employees-list component*/
export class EmployeesListComponent implements OnInit {
  public isLoading = false;
  dataSource: MatTableDataSource<any>;
  public baseSearch;
  public totalListCount;
  public statusList = [];
  public userRole = '';
  public employeesList;


  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  displayedColumns: string[] = ['fullNameEn', 'fullNameAr', 'positionEn', 'positionAr', 'age', 'mobileNumber', 'isActive', 'actions'];
  public filterForm = new FormGroup({
    searchValue: new FormControl(''),
    pageIndex: new FormControl(1),
    pageSize: new FormControl(10),
  });
  constructor(
    private baseService: BaseService,
    public spinner: NgxSpinnerService,
    public notification: NotificationService,
    public translate: TranslateService,
    public router: Router,
  ) {

  }


  ngOnInit() {

    this.getEmployees();
  }

  public changePage(event): void {
    this.getFilterFormControlByName('pageIndex').setValue(event.pageIndex + 1)
    this.getFilterFormControlByName('pageSize').setValue(event.pageSize)
    this.getEmployees();
  }
  public getFilterFormControlByName(controlName: string): FormControl {
    return this.filterForm.get(controlName) as FormControl;
  }
  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }

    this.getEmployees();
  }

  getEmployees(): void {
    this.spinner.show();
    this.baseService.postItem(Controllers.Employee, Actions.GetList, this.filterForm.value).subscribe(res => {
      this.employeesList = res.entities;
      console.log(this.employeesList);
      this.dataSource = new MatTableDataSource(this.employeesList);
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, 1);
      this.spinner.hide();
    }, error => {
      this.spinner.hide();
    });
  }

  changeEmployeeStatus(event, id) {
    console.log(event, id);
    this.baseService.changeStatus(id, event).subscribe(response => {
      console.log(response);
      if (this.translate.currentLang == 'en') {
        this.notification.showNotification('Status', 'Status Modified Successfully', 'success');
      }
      else {
        this.notification.showNotification('الحالة', 'تم تغيير الحالة بنجاح', 'success');
      }
    }, error => {
    })
  }
  exportToExcel() {
    this.baseService.exportToExcel(this.translate.currentLang == 'en' ? 1 : 2).subscribe(response => {
      console.log(response);
      const blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }); // you can change the type
      const url = window.URL.createObjectURL(blob);
      window.open(url);
      if (this.translate.currentLang == 'en') {
        this.notification.showNotification('Export', 'Export Data Done Successfully', 'success');
      }
      else {
        this.notification.showNotification('تصدير', 'تم تصدير البيانات بنجاح', 'success');
      }
    }, error => {
      console.log(error);

    })
  }
  editItem(employeeId: number) {
    this.router.navigate(['/add-edit-employee/' + employeeId]);
  }
}
