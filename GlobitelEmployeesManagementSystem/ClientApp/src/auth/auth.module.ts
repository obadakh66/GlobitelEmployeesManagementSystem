import { environment } from './../environments/environment';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { AuthRouting } from './auth-routing.module';

@NgModule(
  {
    imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      SharedModule,
      AuthRouting
    ],
    declarations: [
      LoginComponent
    ]
  }
)
export class AuthModule {
}
