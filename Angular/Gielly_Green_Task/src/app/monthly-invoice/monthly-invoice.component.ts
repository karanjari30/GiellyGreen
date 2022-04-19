import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import Swal from 'sweetalert2';
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
  customHeader1: any;
  customHeader2: any;
  customHeader3: any;
  customHeader4: any;
  customHeader5: any;
  invoiceReferenceNumber: any;
  isVisible = false;
  dateAndInvoiceForm!: FormGroup;
  checked = false;
  indeterminate = false;
  listOfCurrentPageData: readonly ItemData[] = [];
  listOfData: readonly ItemData[] = [];
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

  hairServices: any;
  beautyServices: any;
  customServices1: any;
  customServices2: any;
  customServices3: any;
  customServices4: any;
  customServices5: any;
  netTotal: any;
  advancePaid: any;

  onChange(date: any) {
    this.isVisible = true;
    date = new Date();
    (console.log(date.getMonth()))
    this.invoiceDate = this.datepipe.transform(date, 'yyyy-MM-dd');
    this.invoiceReferenceNumber = String(date.getMonth() + 1) + String(date.getFullYear()) + String(this.counter);
    this.counter++;
  }

  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
      console.log(this.setOfCheckedId);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onItemChecked(id: number, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listOfCurrentPageData.forEach(item => this.updateCheckedSet(item.id, value));
    this.refreshCheckedStatus();
  }

  onCurrentPageDataChange($event: readonly ItemData[]): void {
    this.listOfCurrentPageData = $event;
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listOfCurrentPageData.every(item => this.setOfCheckedId.has(item.id));
    this.indeterminate = this.listOfCurrentPageData.some(item => this.setOfCheckedId.has(item.id)) && !this.checked;
  }

  sendEmail() {
    Swal.fire({
      title: 'Mailed Successfully!',
      text: 'You just sent mail to suppliers',
      icon: 'success',
      confirmButtonText: 'Ok',
    });
  }

  constructor(private router: Router, private fb: FormBuilder, public datepipe: DatePipe) { }

  ngOnInit(): void {
    if (!sessionStorage.getItem('userSessionToken')) {
      this.router.navigate(['/login']);
    }
    this.listOfData = new Array(20).fill(0).map((_, index) => ({
      id: index,
      name: `Edward King ${index}`,
      age: 32,
      address: `London, Park Lane no. ${index}`
    }));
  }
}
