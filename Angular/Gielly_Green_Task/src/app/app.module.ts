import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { MonthlyInvoiceComponent } from './monthly-invoice/monthly-invoice.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzNotificationModule } from 'ng-zorro-antd/notification';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { ToastrModule } from 'ngx-toastr';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { DatePipe, DecimalPipe } from '@angular/common'
import { NgxPrintModule } from 'ngx-print';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { ProfileComponent } from './profile/profile.component';
import { AdvancePayBracketsDirective } from './advance-pay-brackets.directive';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SupplierListComponent,
    MonthlyInvoiceComponent,
    ProfileComponent,
    AdvancePayBracketsDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NzFormModule,
    NzInputModule,
    NzButtonModule,
    NzIconModule,
    NzLayoutModule,
    NzBreadCrumbModule,
    NzMenuModule,
    NzTableModule,
    NzSwitchModule,
    NzDividerModule,
    FontAwesomeModule,
    FontAwesomeModule,
    NzModalModule,
    NzInputNumberModule,
    NzUploadModule,
    NzNotificationModule,
    NgxPrintModule,
    NzPopconfirmModule,
    NzDatePickerModule,
    NzDropDownModule,
    NzSpinModule,
    NzTypographyModule
  ],
  providers: [{ provide: NZ_I18N, useValue: en_US }, DatePipe, DecimalPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
