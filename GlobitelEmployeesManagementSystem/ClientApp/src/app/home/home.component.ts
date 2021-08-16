import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from '../../auth/authorize.service';
import { BaseService } from '../../shared/services/base.service';
import { NotificationService } from '../../shared/services/notification.service';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Color } from 'ng2-charts';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
/** complaint-list component*/
export class HomeComponent implements OnInit {
  public isLoading = false;
  public userName;
  public dashboardData: any;
  public userRole = '';
  public maleText = '';
  public femaleText = '';
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;

  public barChartColors2: Color[] = [
    { backgroundColor: '#f48220' },
    { backgroundColor: '#35A4E8' },
    { backgroundColor: '#acd059' },
  ];
  public ageChartData: ChartDataSets[];
  public genderChartData: ChartDataSets[]
  public barChartLabels: string[] = [this.translate.currentLang == 'en' ? 'Age Range' : 'معدل الأعمار'];


  constructor(
    private baseService: BaseService,
    public spinner: NgxSpinnerService,
    private authService: AuthorizeService,
    public notification: NotificationService,
    public translate: TranslateService,
  ) {

  }


  ngOnInit() {
    this.userRole = this.authService.getUserRoles();
    this.userName = this.authService.getUserName();
    if (this.userRole === 'Admin') {
      this.spinner.show();
      this.getDashboardData();
    }
  }
  public getDashboardData() {
    this.baseService.getDashboardData().subscribe(res => {
      this.dashboardData = res;
      this.ageChartData = [
        { data: [this.dashboardData.ageChart.smallClassCount], label: '20-40', stack: 'a' },
        { data: [this.dashboardData.ageChart.medClassCount], label: '40-60' },
        { data: [this.dashboardData.ageChart.oldClassCount], label: '60-70' },
      ];

      this.translate.onLangChange.subscribe(res => {
        this.translate.get('Global.male').subscribe(res => {
          this.maleText = res;
        });
        this.translate.get('Global.female').subscribe(res => {
          this.femaleText = res;
        });
        console.log(this.femaleText);

        this.genderChartData = [
          { data: [this.dashboardData.genderChart.maleCount], label: this.maleText, stack: 'a' },
          { data: [this.dashboardData.genderChart.femaleCount], label: this.femaleText }
        ];
      });
      this.translate.get('Global.male').subscribe(res => {
        this.maleText = res;
      });
      this.translate.get('Global.female').subscribe(res => {
        this.femaleText = res;
      });
      this.genderChartData = [
        { data: [this.dashboardData.genderChart.maleCount], label: this.maleText, stack: 'a' },
        { data: [this.dashboardData.genderChart.femaleCount], label: this.femaleText }
      ];
      this.spinner.hide();
    }, error => {
      this.spinner.hide();
    });
  }
  public barChartOptions: ChartOptions = {
    responsive: true,
    scales: {
      /* xAxes: [{
        barPercentage: 0.5
      }] */
    }
  };

}
