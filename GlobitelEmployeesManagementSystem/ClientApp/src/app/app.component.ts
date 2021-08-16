import { AuthorizeService } from './../auth/authorize.service';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  public dir = 'ltr'

  constructor(
    public authorizeService: AuthorizeService,
    public translate: TranslateService,
    public spinner: NgxSpinnerService,
  ) {
    translate.setDefaultLang('en');
    translate.use('en');
  }
  ngOnInit() {
  }
  logout() {
    this.spinner.show();
    this.authorizeService.logout();
  }

  toggleLanguage() {
    const currentLang = this.translate.currentLang
    currentLang == 'en' ? this.translate.use('ar') : this.translate.use('en')
    currentLang == 'en' ? this.dir = "rtl" : this.dir = "ltr";
  }
}
