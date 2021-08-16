import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/shared/services/base.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from '../authorize.service';
import { NotificationService } from '../../shared/services/notification.service';
import { Actions, Controllers } from '../../shared/services/api-config';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  getFormControlByName(controlName: string): FormControl {
    return this.loginForm.get(controlName) as FormControl;
  }
  public dir = 'ltr'

  constructor(
    private baseService: BaseService,
    public spinner: NgxSpinnerService,
    private authService: AuthorizeService,
    private router: Router,
    public notification: NotificationService,
    public translate: TranslateService
  ) { }

  ngOnInit(): void {
  }

  submitForm(): void {
    if (this.loginForm.invalid){
      this.notification.showNotification('Warning', 'Check form fields', 'warning');
    }
    else {
      this.spinner.show();
      const loginForm = this.loginForm.value;
      this.baseService.postItem(Controllers.Auth, Actions.Login, loginForm).subscribe(response => {
        this.spinner.hide();
        this.authService.setToken((response as any).accessToken);
        this.router.navigate(['/']);
      }, error => {
        console.log(error);
        if(error.status === 400){
          this.notification.showNotification('Login Failed', error.error.Message, 'error');
        }
        else {
          this.notification.showNotification('Login Failed', 'Something went wrong please contact system admin', 'error');
        }
        this.spinner.hide();
      });
    }
  }
}
