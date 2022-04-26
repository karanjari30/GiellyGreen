import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { MonthlyInvoiceComponent } from './monthly-invoice/monthly-invoice.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { path: 'suppliers-list', component: SupplierListComponent },
  { path: 'monthly-invoice', component: MonthlyInvoiceComponent },
  { path: 'profile', component: ProfileComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
