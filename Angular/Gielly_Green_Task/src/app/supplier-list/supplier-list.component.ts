import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faPen, faPlus, faTrash, faArrowUpFromBracket, faXmark, faTruck, faFileInvoiceDollar, faEllipsisVertical, faUser, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { ApiDataService } from '../api-data.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Router } from '@angular/router';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  //Variables/Properties
  fileName: any;
  searchSupplier = '';
  modalTitle: any;
  tempData: any;
  isEmailDuplicate: boolean = false;
  token: any;
  tempLogoData: any;
  tempListData: any;
  editIcon = faPen;
  uploadIcon = faArrowUpFromBracket;
  plusIcon = faPlus;
  showLoader = false;
  deleteIcon = faTrash;
  iconDelete = faXmark;
  profileIcon = faUser;
  menuIcon = faEllipsisVertical;
  logoutIcon = faRightFromBracket;
  supplierIcon = faTruck;
  invoiceIcon = faFileInvoiceDollar;
  supplierListData: any;
  sortNameFn = (a: ApiDataService["supplier"], b: ApiDataService["supplier"]) => a.SupplierName.localeCompare(b.SupplierName);
  sortReferenceNoFn = (a: ApiDataService["supplier"], b: ApiDataService["supplier"]) => a.SupplierReference.localeCompare(b.SupplierReference);
  tempSupplierList: any;
  isCollapsed = false;
  isVisible = false;
  validateForm!: FormGroup;
  fileData: any;
  @ViewChild('logoUpload') logoUpload: any;

  //this method is for showing modal at the time adding/updating the data
  showModal($event: any): void {
    this.isVisible = true;
    if (!$event) {
      this.modalTitle = "Add New Supplier";
      this.tempData = null;
      this.validateForm.reset();
    } else {
      this.modalTitle = "Edit Supplier";
      this.tempData = $event;
      if (this.tempData.logo == null || this.tempData.logo == '') {
        this.tempLogoData = null;
        this.getPatchValueForEdit();
      } else {
        this.tempLogoData = "data:image/jpeg;base64," + this.tempData.logo;
        this.getPatchValueForEdit();
      }
    }

  }

  //This method is called for calling for supplier adding/updating method
  handleOk(): void {
    if (this.validateForm.valid) {
      if (this.tempData) {
        this.editSupplier(this.tempData.SupplierId);
      } else {
        this.addSupplier();
      }
    } else {
      //to fire validations on invalid fields
      Object.values(this.validateForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  //This method will add suupplier to the database
  addSupplier() {
    this.setDataOfAPIModel();
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.addSupplier(this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Data added successfully!'
          );
          this.validateForm.reset();
          this.fileName = null;
          this.tempLogoData = null;
          this.isVisible = false;
          this.getDataInTable();
          this.showLoader = false;
        }
        else {
          this.showStatusNotification(response);
        }
      }
    );

  }

  //This method is for hiding the modal
  handleCancel(): void {
    this.validateForm.reset();
    this.fileName = null;
    this.tempLogoData = null;
    this.isVisible = false;
  }

  //This method will fetch the data from the API into the table
  getDataInTable() {
    this.token= sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.getDataSuppliers(this.token).subscribe((data: any) => {
      if(data.ResponseStatus == 1){
        this.supplierListData = data.Result;
        this.tempListData = data.Result;
        this.showLoader = false;
      }
      else{
        this.showLoader = false;
      }
    })
  }

  //This is a common method for setting form values in the API model
  setDataOfAPIModel() {
    this.apiService.supplier.SupplierReference = this.validateForm.value.supplierRefNo;
    this.apiService.supplier.SupplierName = this.validateForm.value.supplierName;
    this.apiService.supplier.BusinessAddress = this.validateForm.value.businessAddress;
    this.apiService.supplier.EmailAddress = this.validateForm.value.emailAddress;
    this.apiService.supplier.PhoneNumber = this.validateForm.value.phoneNumber;
    this.apiService.supplier.CompanyRegisterNumber = this.validateForm.value.companyRegisteredNumber;
    this.apiService.supplier.VATNumber = this.validateForm.value.vatNumber;
    this.apiService.supplier.TaxReference = this.validateForm.value.taxReference;
    this.apiService.supplier.CompanyRegisterAddress = this.validateForm.value.companyAddress;
    this.apiService.supplier.Isactive = this.validateForm.value.isSupplierActive;
    this.apiService.supplier.logo = this.fileData;
  }

  //This method is for searching a record in the table by using SupplierName
  searchBySupplierOrReference() {
    this.supplierListData = this.tempListData.filter(
      (item: any) => item.SupplierName.toLowerCase().indexOf(this.searchSupplier.toLowerCase()) !== -1
    );
    
  }

  //This method will delete the supplier from database
  deleteSupplier(supplierId: number) {
    this.apiService.deleteSupplier(supplierId, this.token).subscribe(
      (response: any) => {
        console.log(response);
        if (response.ResponseStatus == 1) {
          if (response.Result == 2) {
            this.notification.create(
              'error',
              'Error',
              'Invoice already exists for this supplier!'
            );
          } else {
            this.supplierListData = this.supplierListData.filter((d: any) => d.SupplierId !== supplierId);
            this.notification.create(
              'success',
              'Success',
              'Record Deleted successfully!'
            );
          }

        } else {
          this.notification.create(
            'error',
            'Error',
            'Failed to delete the record!'
          );
        }
      }
    )
  }

  //This method will fill up all the data in modal form whenever we click on edit button
  getPatchValueForEdit(){
    this.validateForm.patchValue({
      supplierName: this.tempData.SupplierName,
      supplierRefNo: this.tempData.SupplierReference,
      businessAddress: this.tempData.BusinessAddress,
      emailAddress: this.tempData.EmailAddress,
      phoneNumber: this.tempData.PhoneNumber,
      companyRegisteredNumber: this.tempData.CompanyRegisterNumber,
      vatNumber: this.tempData.VATNumber,
      taxReference: this.tempData.TaxReference,
      companyAddress: this.tempData.CompanyRegisterAddress,
      isSupplierActive: this.tempData.Isactive,
      invoiceLogo: this.tempLogoData
    })
  }

  //This method is for editing the Supplier record
  editSupplier(supplierId: number) {
    this.setDataOfAPIModel();
    this.showLoader = true;
    this.token = sessionStorage.getItem('userSessionToken');
    this.apiService.editSupplier(supplierId, this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Record updated successfully!'
          );
          this.getDataInTable();
          this.handleCancel();
          this.showLoader = false;
        } else {
          this.showLoader = false;
          this.showStatusNotification(response);
        }
      }
    );

  }

  //This method will help to show notifications for Unique VATNumber, Supplier Reference, Tax Reference and Email
  showStatusNotification(response: any) {
    if (response.Result.includes('SP_EmailAddress')) {
      this.notification.create(
        'error',
        'Error',
        'Email already exists!'
      );
    }
    else if (response.Result.includes('SP_SupplierReference')) {
      this.notification.create(
        'error',
        'Error',
        'Supplier Reference already exists!'
      );
    } else if (response.Result.includes('SP_TaxReferenceIndex')) {
      this.notification.create(
        'error',
        'Error',
        'Tax reference number already exists!'
      );
    }
    else if (response.Result.includes('SP_VATNumberIndex')) {
      this.notification.create(
        'error',
        'Error',
        'VAT number already exists!'
      );
    } else {
      this.notification.create(
        'error',
        'Error',
        'Something went wrong. Please refresh the page'
      );
    }
  }

  //This method is for changing the Supplier Status from the table
  changeStatus(supplierData: any) {
    this.token = sessionStorage.getItem('userSessionToken');
    this.showLoader = true;
    this.apiService.updateSupplierStatus(supplierData.SupplierId, supplierData.Isactive, this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Status updated successfully!'
          );
          this.showLoader = false;
        } else {
          this.notification.create(
            'error',
            'Error',
            'Failed to update the status'
          );
          this.showLoader = false;
        }
      }
    );
  }

  //This method is used for converting the file into Base64 string
  onFileSelected(filedata: any) {
    const file: File = filedata.target.files[0];
    if (file) {
      if ((file.type == 'image/png') || (file.type == 'image/jpeg')) {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          this.tempLogoData = reader.result
          this.fileData = this.tempLogoData.split(',')[1];
        };
      } else {
        this.notification.create(
          'error',
          'Error',
          'Only .jpeg or .png are allowed!'
        );
        // alert('Only .jpeg or .png are allowed');
      }
    }
  }

  //This method will remove the uploaded image on button click when the user the is adding/editing the supplier
  removeImage() {
    this.tempLogoData = '';
    this.fileData = null;
    this.logoUpload.nativeElement.value = "";
    // console.log(this .tempLogoData);
    // this.fileData = this.tempLogoData;
  }
  
  //This method is for collapsing the menu
  isMenuCollapsed(){
    if(this.isCollapsed){
      this.isCollapsed = false;
    } else {
      this.isCollapsed = true;
    }
  }

  
  logout(){
    sessionStorage.removeItem('userSessionToken');
    this.router.navigate(['/login']);
  }

  constructor(private apiService: ApiDataService, private fb: FormBuilder, private notification: NzNotificationService, private router: Router) {
    this.validateForm = this.fb.group({
      supplierRefNo: [null, [Validators.required, Validators.pattern('^[a-zA-Z0-9]*$'), Validators.maxLength(15)]],
      supplierName: [null, [Validators.required, Validators.pattern('^[a-zA-Z ]*$'), Validators.maxLength(30)]],
      businessAddress: [null, Validators.maxLength(150)],
      emailAddress: [null, [Validators.required, Validators.email]],
      phoneNumber: [null, [Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]],
      companyRegisteredNumber: [null, [Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]],
      vatNumber: [null, [Validators.pattern('^[a-zA-Z0-9][A-Za-z0-9]*$'), Validators.maxLength(15)]],
      taxReference: [null, [Validators.pattern('^[A-Za-z0-9]*$'), Validators.maxLength(15)]],
      companyAddress: [null, Validators.maxLength(150)],
      isSupplierActive: [false],
      invoiceLogo: [null]
    })
  }

  //Initialization Method
  ngOnInit(): void {
    if (!sessionStorage.getItem('userSessionToken')) {
      this.router.navigate(['/login']);
    }
    this.tempLogoData = null;
    this.isCollapsed = false;
    this.getDataInTable();
  }
}