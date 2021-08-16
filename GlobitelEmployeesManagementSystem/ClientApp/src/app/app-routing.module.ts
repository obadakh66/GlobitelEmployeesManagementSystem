import { AuthorizeGuard } from './../auth/authorize.guard';
import { AuthModule } from './../auth/auth.module';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleGuard } from 'src/auth/role.guard';
import { EmployeesListComponent } from './employees-list/employees-list.component';
import { AddEditEmployeeComponent } from './add-edit-employee/add-edit-employee.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: 'auth', loadChildren: () => import('src/auth/auth.module').then(m => m.AuthModule) },
  { path: '', component: HomeComponent, canActivate: [AuthorizeGuard] },
  { path: 'employees-list', component: EmployeesListComponent, canActivate: [RoleGuard], data: { expectedRole: 'Admin' } },
  { path: 'add-edit-employee', component: AddEditEmployeeComponent, canActivate: [RoleGuard], data: { expectedRole: 'Admin' } },
  { path: 'add-edit-employee/:id', component: AddEditEmployeeComponent, canActivate: [RoleGuard], data: { expectedRole: 'Admin' } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    // preloadingStrategy: PreloadAllModules,
    scrollPositionRestoration: 'enabled',
    relativeLinkResolution: 'corrected',
    anchorScrolling: 'enabled',
    useHash: true
  }) ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
