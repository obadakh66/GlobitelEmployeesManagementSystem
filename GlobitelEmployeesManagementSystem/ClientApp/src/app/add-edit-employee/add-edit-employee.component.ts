import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Actions, Controllers } from '../../shared/services/api-config';
import { BaseService } from '../../shared/services/base.service';
import { NotificationService } from '../../shared/services/notification.service';

@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.scss']
})
/** add-edit-employee component*/
export class AddEditEmployeeComponent implements OnInit {

  public isUpdateMode = false;
  /** Component Intialization **/
  constructor(
    private baseService: BaseService,
    public spinner: NgxSpinnerService,
    private router: Router,
    private route: ActivatedRoute,
    public notification: NotificationService,
    public translate: TranslateService,
  ) {

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id) {
        console.log(params);

        this.isUpdateMode = true;
        this.getEmployeeById(Number(params.id))
      }
    })
  }

  /** End Component Intialization **/

  /** Form Setup **/
  public employeeForm = new FormGroup({
    id: new FormControl(0),
    fullNameEN: new FormControl('', Validators.required),
    fullNameAR: new FormControl('', Validators.required),
    positionEN: new FormControl('', Validators.required),
    positionAR: new FormControl('', Validators.required),
    dateOfBirth: new FormControl('', Validators.required),
    dateOfEmployement: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    gender: new FormControl('', Validators.required),
    mobileNumber: new FormControl('', Validators.required)
  });

  getControlByName(name: string) {
    return this.employeeForm.get(name) as FormControl;
  }

  setControlValue(name: string, value: any) {
    this.employeeForm.get(name).setValue(value);
  }
  /** End Form Setup **/

  getEmployeeById(employeeId: number) {
    this.baseService.getById(employeeId).subscribe(response => {
      this.employeeForm.patchValue(response);
    }, error => {
      console.log(error);
      if (error.status === 400) {
        this.notification.showNotification('Create Complaint Failed', error.error.Message, 'error');
      }
      else {
        this.notification.showNotification('Create Complaint Failed', 'Something went wrong please contact system admin', 'error');
      }

    });
  }

  /** Form Submittion **/
  submitForm(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      if (this.translate.currentLang == 'en') {
        this.notification.showNotification('Warning', 'Please Fill Required Fields', 'warn');
      }
      else {
        this.notification.showNotification('تحذير', 'يرجى تعبئة الحقول المطلوبة', 'warn');
      }
    }
    else {
      this.spinner.show();
      const empForm = this.employeeForm.value;
      this.baseService.postItem(Controllers.Employee, Actions.Submit, empForm).subscribe(response => {
        this.spinner.hide();
        if (this.translate.currentLang == 'en') {
          this.notification.showNotification('Success', 'Employee Created Successfully', 'success');
        }
        else {
          this.notification.showNotification('تم بنجاح', 'تم اضافة الموظف بنجاح', 'success');
        }
        this.router.navigate(['/employees-list'])
      }, error => {
        this.spinner.hide();
        console.log(error);
          if (this.translate.currentLang == 'en') {
            this.notification.showNotification('Error', 'Employee Created Failed', 'error');
          }
          else {
            this.notification.showNotification('حصل خطأ', 'لم يتم اضافة الموظف بنجاح', 'error');
          }
      });
    }
  }
  /** End Form Submittion **/

  /**Edit Form Submittion **/
  submitEditForm(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      if (this.translate.currentLang == 'en') {
        this.notification.showNotification('Warning', 'Please Fill Required Fields', 'warn');
      }
      else {
        this.notification.showNotification('تحذير', 'يرجى تعبئة الحقول المطلوبة', 'warn');
      }
    }
    else {
      this.spinner.show();
      const empForm = this.employeeForm.value;
      this.baseService.postItem(Controllers.Employee, Actions.Edit, empForm).subscribe(response => {
        this.spinner.hide();
        if (this.translate.currentLang == 'en') {
          this.notification.showNotification('Success', 'Employee Modified Successfully', 'success');
        }
        else {
          this.notification.showNotification('تم بنجاح', 'تم تعديل الموظف بنجاح', 'success');
        }
        this.router.navigate(['/employees-list'])
      }, error => {
        this.spinner.hide();
        console.log(error);
          if (this.translate.currentLang == 'en') {
            this.notification.showNotification('Success', 'Employee Modified Failed', 'error');
          }
          else {
            this.notification.showNotification('حصل خطأ', 'لم يتم تعديل الموظف بنجاح', 'error');
          }
      });
    }
  }
  /** End Edit Form Submittion **/


}
