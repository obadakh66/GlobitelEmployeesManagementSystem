import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { MaterialModule } from './material.module';
import { YesNoDialogComponent } from './yes-no-dialog/yes-no-dialog.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule(
  {
    exports: [
      MaterialModule,
      YesNoDialogComponent,
      TranslateModule
    ],
    imports: [
      CommonModule,
      HttpClientModule,
      MaterialModule,
      SimpleNotificationsModule.forRoot()
    ],
    declarations: [
      YesNoDialogComponent
    ]
  }
)
export class SharedModule {
}
