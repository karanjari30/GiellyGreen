import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe, DecimalPipe } from '@angular/common';
import { faCheck, faTruck, faFileInvoiceDollar, faEllipsisVertical, faUser, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import Swal from 'sweetalert2';
import { ApiDataService } from '../api-data.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
interface ItemData {
  id: number;
  name: string;
  age: number;
  address: string;
}
@Component({
  selector: 'app-monthly-invoice',
  templateUrl: './monthly-invoice.component.html',
  styleUrls: ['./monthly-invoice.component.css']
})
export class MonthlyInvoiceComponent implements OnInit {

  isCollapsed = false;
  approveButtonIcon = faCheck;
  supplierIcon = faTruck;
  invoiceIcon = faFileInvoiceDollar;
  menuIcon = faEllipsisVertical;
  month = null;
  globalVAT: number = 0;
  showLoader = false;
  invoiceDate: any;
  profileIcon = faUser;
  logoutIcon = faRightFromBracket;
  monthlyInvoiceData: any;
  netTotal:number = 0;
  isDataSaved: boolean = false;
  tempMonthlyInvoiceData: any[] = [];
  monthForInvoice: any;
  monthToGetInvoice: any;
  yearToGetInvoice: any;
  customHeader1: any;
  customHeader2: any;
  customHeader3: any;
  customHeader4: any;
  customHeader5: any;
  arrayOfID: any;
  token: any;
  tempDate: any;
  invoiceReferenceNumber: any;
  isVisible = false;
  checked = false;
  indeterminate = false;
  listOfCurrentPageData: readonly ItemData[] = [];
  setOfCheckedId = new Set<number>();
  counter: number = 0;
  listOfSelection = [
    {
      text: 'Select All Row',
      onSelect: () => {
        this.onAllChecked(true);
      }
    },
  ];

  //This method will give month and year for the report printing and also will generate an invoice reference number
  onChange(date: any) {
    this.isVisible = true;
    this.tempDate = date;
    this.monthToGetInvoice = this.datepipe.transform(date, 'MM');
    this.yearToGetInvoice = date.getFullYear();
    date = new Date();
    this.invoiceDate = this.datepipe.transform(date, 'yyyy/MM/dd');
    this.monthForInvoice = String(this.monthToGetInvoice + "/" + this.yearToGetInvoice);
    this.counter++;
    this.netTotal = 0;
    this.getInvoicesData(this.monthToGetInvoice, this.yearToGetInvoice)
  }

  onDateChange(date: any) {
    this.invoiceDate = this.datepipe.transform(date, 'yyyy/MM/dd');
  }

  //this method is called whenever a row is selected
  updateCheckedSet(id: number): void {
    if (this.setOfCheckedId.has(id)) {
      this.setOfCheckedId.delete(id);
      this.arrayOfID = Array.from(this.setOfCheckedId);
    } else {
      this.setOfCheckedId.add(id);
      this.arrayOfID = Array.from(this.setOfCheckedId);
    }
  }

  //This method will 
  onItemChecked(id: number): void {
    this.updateCheckedSet(id);
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.monthlyInvoiceData.forEach((item: any) => this.updateCheckedSet(item.SupplierId));
    this.refreshCheckedStatus();
  }

  //This will check the changes on the current page if pagination is there
  onCurrentPageDataChange($event: readonly ItemData[]): void {
    this.listOfCurrentPageData = $event;
    this.refreshCheckedStatus();
  }

  //This method is a default method 
  refreshCheckedStatus(): void {
    this.checked = this.listOfCurrentPageData.every(item => this.setOfCheckedId.has(item.id));
    this.indeterminate = this.listOfCurrentPageData.some(item => this.setOfCheckedId.has(item.id)) && !this.checked;
  }

  //This method is for sending email
  sendEmail() {
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.emailSelectedSuppliers(this.arrayOfID, this.monthToGetInvoice, this.yearToGetInvoice, this.token).subscribe(
      (response: any) => {
        console.log(response)
        if (response.ResponseStatus == 1) {
          Swal.fire({
            title: 'Mailed Successfully!',
            text: 'You just sent mail to suppliers',
            icon: 'success',
            confirmButtonText: 'Ok',
          });
          this.showLoader = false;
        } else {
          Swal.fire({
            title: 'Error!',
            text: 'Error in sending mail!',
            icon: 'error',
            confirmButtonText: 'Ok',
          });
          this.showLoader = false;
        }
      }
    )

  }

  //This method is for printing the monthly invoice table 
  printReport() {
    setTimeout(function () {
      window.print();
    }, 2000);
  }

  //This method is for getting all the invoice data from API
  getInvoicesData(month: number, year: number) {
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.getMonthlyInvoiceData(month, year, this.token).subscribe((response: any) => {
      if (response.ResponseStatus == 1) {
        this.monthlyInvoiceData = response.Result;
        this.customHeader1 = this.monthlyInvoiceData[0].Custom1;
        this.customHeader2 = this.monthlyInvoiceData[0].Custom2;
        this.customHeader3 = this.monthlyInvoiceData[0].Custom3;
        this.customHeader4 = this.monthlyInvoiceData[0].Custom4;
        this.customHeader5 = this.monthlyInvoiceData[0].Custom5;
        if(this.monthlyInvoiceData[0].InvoiceReferenceId !=null){
          this.invoiceReferenceNumber = this.monthlyInvoiceData[0].InvoiceReferenceId
        } else {
          this.invoiceReferenceNumber = this.datepipe.transform(this.tempDate, "MMM") + this.tempDate.getFullYear()
        }
        this.globalVAT = this.monthlyInvoiceData[0].VAT;
        this.showLoader = false;
        this.onCurrentPageDataChange(response.Result);
      }

      // this.invoiceReferenceNumber = this.monthlyInvoiceData[0].InvoiceReferenceId;
    })
  }

  //This method will save the table into database using API
  saveData() {
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    var monthlyJSONObject: any = this.setMonthlyInvoiceObjectAPIData();
    this.apiService.addMonthlyInvoicesData(monthlyJSONObject, this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Data saved successfully!'
          );
          this.showLoader = false;
          this.isDataSaved = true;
          this.getInvoicesData(this.monthToGetInvoice, this.yearToGetInvoice);
        } else {
          this.notification.create(
            'error',
            'Error',
            'Failed to save data. Please try again!'
          );
          this.showLoader = false;
        }
      });
  }

  //This method is called to set the Monthly Invoice Object
  setMonthlyInvoiceObjectAPIData() {
    var body = {
      Custom1: this.customHeader1,
      Custom2: this.customHeader2,
      Custom3: this.customHeader3,
      Custom4: this.customHeader4,
      Custom5: this.customHeader5,
      InvoiceReferenceId: this.invoiceReferenceNumber,
      InvoiceYear: this.yearToGetInvoice,
      InvoiceMonth: this.monthToGetInvoice,
      InvoiceDate: this.invoiceDate,
      VAT: this.globalVAT,
      InvoiceViewList: this.monthlyInvoiceData
    }
    return body;
  }

  //This method is called whenever the amount in any input field of supplier changes
  onAmountChange(data: any) {
    if (this.tempMonthlyInvoiceData.length != 0) {
      var isIdExist: any = this.tempMonthlyInvoiceData.filter((item: { SupplierId: any }) => item.SupplierId == data.SupplierId);
      if (isIdExist.length != 0) {
        this.tempMonthlyInvoiceData.splice(isIdExist, 1);
      }
    }
    this.tempMonthlyInvoiceData.push(data);
  }

  //This method is for approving the suppliers usign SupplierId
  approveSelectedSupplier() {
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.approveSupplier(this.arrayOfID, this.monthToGetInvoice, this.yearToGetInvoice, this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          Swal.fire({
            title: 'Approved!',
            text: 'You just approved invoices.',
            icon: 'success',
            confirmButtonText: 'Ok',
          });
          this.showLoader = false;
          this.getInvoicesData(this.monthToGetInvoice, this.yearToGetInvoice);
        } else {
          Swal.fire({
            title: 'Error',
            text: 'Failed to approve invoices. Please try again later!',
            icon: 'error',
            confirmButtonText: 'Ok',
          });
          this.showLoader = false;
        }
      }
    )
  }

  //This method will return net amount
  getNetAmount(data: any) {
    return data.NetAmount = data.HairService + data.BeautyService + data.CustomHeader1 + data.CustomHeader2 + data.CustomHeader3 + data.CustomHeader4 + data.CustomHeader5;
  }

  //This method will return VAT amount
  getVATAmount(data: any) {
    return data.VATAmount = data.NetAmount * (this.globalVAT / 100);
  }

  //This method will return gross amount
  getGrossAmount(data: any) {
    return data.GrossAmount = data.NetAmount + data.VATAmount;
  }

  //This method will return balance due
  getBalanceDue(data: any) {
    return data.BalanceDue = data.GrossAmount - data.AdvancePay;
  }

  //This method will return total of net amount column
  totalNetAmount(data: any) {
    this.netTotal = 0;
    data.forEach((item: { NetAmount: number }) => {
      this.netTotal += item.NetAmount;
    });
    console.log(this.netTotal);
    return this.netTotal;
  }

  //This method will return total of VAT amount column
  totalVATAmount(data: any) {
    let total = 0;
    data.forEach((item: { VATAmount: number }) => {
      total += item.VATAmount;
    })
    return total;
  }

  //This method will return total of gross amount column
  totalGrossAmount(data: any) {
    let total = 0;
    data.forEach((item: { GrossAmount: number }) => {
      total += item.GrossAmount;
    })
    return total;
  }

  //This method will return total of advance payment amount column
  totalAdvancePayment(data: any) {
    let total = 0;
    data.forEach((item: { AdvancePay: number }) => {
      total += item.AdvancePay;
    })
    return total;
  }

  //This method will return total of balance due column
  totalBalanceDue(data: any) {
    let total = 0;
    data.forEach((item: { BalanceDue: number }) => {
      total += item.BalanceDue;
    })
    return total;
  }

  //this method will create pdf and will download it
  createPDF(string: string, fileName: string) {
    const source = `data:application/pdf;base64,${string}`;
    const link = document.createElement("a");
    link.href = source;
    link.download = `${fileName}.pdf`
    link.click();
  }

  //This method will return a base64 string
  downloadCombinePDF() {
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.downloadPDF(this.arrayOfID, this.monthToGetInvoice, this.yearToGetInvoice, this.token).subscribe(
      (response: any) => {
        console.log(response);
        if (response.ResponseStatus == 1) {
          this.createPDF(response.Result, response.Message);
          this.showLoader = false;
        } else {
          this.notification.create(
            'error',
            'Error',
            'You cannot download PDF as the data for supplier is not present.'
          );
          this.showLoader = false;
        }
      }
    )
  }

  //This method is for showing or hiding the menu
  isMenuCollapsed() {
    if (this.isCollapsed) {
      this.isCollapsed = false;
    } else {
      this.isCollapsed = true;
    }
  }

  logout() {
    sessionStorage.removeItem('userSessionToken');
    this.router.navigate(['/login']);
  }

  constructor(private apiService: ApiDataService, private router: Router, private fb: FormBuilder, public datepipe: DatePipe, private notification: NzNotificationService) { }

  ngOnInit(): void {
    if (!sessionStorage.getItem('userSessionToken')) {
      this.router.navigate(['/login']);
    }
    this.isDataSaved = false;
    this.isCollapsed = false;
  }
}
