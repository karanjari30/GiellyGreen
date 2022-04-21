import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
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
  month = null;
  invoiceDate: any;
  monthlyInvoiceData: any;
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
    this.monthToGetInvoice = date.getMonth() + 1;
    this.yearToGetInvoice = date.getFullYear();
    date = new Date();
    this.invoiceDate = this.datepipe.transform(date, 'yyyy-MM-dd');
    this.monthForInvoice = this.datepipe.transform(date, 'MM/yyyy');
    this.counter++;
    this.getInvoicesData(this.monthToGetInvoice, this.yearToGetInvoice)

  }

  //this method is called whenever a row is selected
  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
      console.log(this.setOfCheckedId);
      this.arrayOfID = Array.from(this.setOfCheckedId);

    } else {
      this.setOfCheckedId.delete(id);
      this.arrayOfID = Array.from(this.setOfCheckedId);
    }
  }

  //This method will 
  onItemChecked(id: number, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.monthlyInvoiceData.forEach((item: any) => this.updateCheckedSet(item.SupplierId, value));
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
    Swal.fire({
      title: 'Mailed Successfully!',
      text: 'You just sent mail to suppliers',
      icon: 'success',
      confirmButtonText: 'Ok',
    });
  }

  //This method is for printing the monthly invoice table 
  printReport() {
    let printContents: any = document.getElementById('printableDiv');
    let getTableToPrint = printContents.outerHTML;
    var a: any = window.open('', '', 'height=1000, width=1000');
    setTimeout(function () {
      a.document.write('<html><head><link rel="stylesheet" type="text/css" href="styles.css" /></head>');
      a.document.write('<body>');
      a.document.write(getTableToPrint);
      a.document.write('</body></html>');
      // a.close();
      window.print();
    }, 2000);

  }

  //This method is for getting all the invoice data from API
  getInvoicesData(month: number, year: number) {
    this.apiService.getMonthlyInvoiceData(month, year).subscribe((response: any) => {
      console.log(response.Result);
      this.monthlyInvoiceData = response.Result;
      this.customHeader1 = this.monthlyInvoiceData[0].Custom1;
      this.customHeader2 = this.monthlyInvoiceData[0].Custom2;
      this.customHeader3 = this.monthlyInvoiceData[0].Custom3;
      this.customHeader4 = this.monthlyInvoiceData[0].Custom4;
      this.customHeader5 = this.monthlyInvoiceData[0].Custom5;
      this.onCurrentPageDataChange(response.Result);
      this.invoiceReferenceNumber = this.monthlyInvoiceData[0].InvoiceReferenceId;
    })
  }

  //This method will save the table into database using API
  saveData() {
    var monthlyJSONObject: any = this.setMonthlyInvoiceObjectAPIData();
    console.log("In Save data method: ", monthlyJSONObject);
    this.apiService.addMonthlyInvoicesData(monthlyJSONObject).subscribe((response: any) => console.log(response));
  }

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
      InvoiceViewList: this.monthlyInvoiceData
    }
    return body;
  }

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
    this.apiService.approveSupplier(this.arrayOfID, this.monthToGetInvoice, this.yearToGetInvoice).subscribe(
      (response: any) => {
        // console.log(response)
        if(response.ResponseStatus == 1){
          this.notification.create(
            'success',
            'Success',
            'Supplier(s) successfully approved!'
          );
        } else{
          this.notification.create(
            'error',
            'Error',
            'Failed to approve supplier(s)'
          );
        }
      }
    )
  }

  constructor(private apiService: ApiDataService, private router: Router, private fb: FormBuilder, public datepipe: DatePipe, private notification: NzNotificationService) { }

  ngOnInit(): void {
    if (!sessionStorage.getItem('userSessionToken')) {
      this.router.navigate(['/login']);
    }
  }
}
