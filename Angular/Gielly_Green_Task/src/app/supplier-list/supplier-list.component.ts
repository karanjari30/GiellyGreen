import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faPen, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ApiDataService } from '../api-data.service';
// import Swal from 'sweetalert2';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  //Variables
  fileName:any;
  searchSupplier = '';
  editIcon = faPen;
  tempData: any;
  isEmailDuplicate: boolean = false;
  token: any;
  tempLogoData: any;
  plusIcon = faPlus;
  deleteIcon = faTrash;
  supplierListData: any;
  sortNameFn = (a: ApiDataService["supplier"], b: ApiDataService["supplier"]) => a.SupplierName.localeCompare(b.SupplierName);
  sortReferenceNoFn = (a: ApiDataService["supplier"], b: ApiDataService["supplier"]) => a.SupplierReference.localeCompare(b.SupplierReference);
  tempSupplierList: any;
  isCollapsed = false;
  isVisible = false;
  validateForm!: FormGroup;
  msg: any;
  img = new Image();
  fileData: any;

  //this method is for showing modal at the time adding/updating the data
  showModal($event: any): void {
    this.isVisible = true;
    if (!$event) {
      this.tempData = null;
      this.validateForm.reset();
    } else {
      this.tempData = $event;
      this.tempLogoData = "data:image/jpeg;base64," + this.tempData.logo;
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

  }

  //This method is called for calling for supplier adding/updating method
  handleOk(): void {
    this.toastr.info('Init method called');
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
    const formControl = this.validateForm.value;
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

    this.apiService.addSupplier().subscribe(
      (response: any) => {
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
        }
        else {
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
          }
          else {
            this.notification.create(
              'error',
              'Error',
              'Something went wrong. Please refresh the page'
            );
          }
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
    this.apiService.getDataSuppliers().subscribe((data: any) => {
      this.supplierListData = data.Result;
    })
  }

  //This method is for searching a record in the table by using SupplierName
  searchBySupplierOrReference() {
    let searchBySupplier = this.supplierListData.filter(
      (item: any) => item.SupplierName.indexOf(this.searchSupplier) !== -1
    );

    if (this.searchSupplier.length <= 0) {
      searchBySupplier = null;
      if (searchBySupplier == null) {
        this.getDataInTable();
      }
    }
    else {
      this.supplierListData = searchBySupplier;
    }
  }

  //This method will delete the supplier from database
  deleteSupplier(supplierId: number) {
    this.supplierListData = this.supplierListData.filter((d: any) => d.SupplierId !== supplierId);
    this.apiService.deleteSupplier(supplierId, this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Record Deleted successfully!'
          );
        } else {
          this.notification.create(
            'error',
            'Error',
            'Failed to delete the recordz!'
          );
        }
      }
    )
  }

  //This method is for editing the Supplier record
  editSupplier(supplierId: number) {
    const supplierFormFields = this.validateForm.controls;
    const supplierToEdit = this.apiService.supplier;

    supplierToEdit.SupplierName = supplierFormFields["supplierName"].value;
    supplierToEdit.SupplierReference = supplierFormFields["supplierRefNo"].value;
    supplierToEdit.BusinessAddress = supplierFormFields["businessAddress"].value;
    supplierToEdit.EmailAddress = supplierFormFields["emailAddress"].value;
    supplierToEdit.PhoneNumber = supplierFormFields["phoneNumber"].value;
    supplierToEdit.CompanyRegisterNumber = supplierFormFields["companyRegisteredNumber"].value;
    supplierToEdit.VATNumber = supplierFormFields["vatNumber"].value;
    supplierToEdit.TaxReference = supplierFormFields["taxReference"].value;
    supplierToEdit.CompanyRegisterAddress = supplierFormFields["companyAddress"].value;
    supplierToEdit.Isactive = supplierFormFields["isSupplierActive"].value;
    supplierToEdit.logo = this.fileData;

    this.token = sessionStorage.getItem('userSessionToken');
    this.apiService.editSupplier(supplierId, this.token).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Record updated successfully!'
          );
          this.handleCancel();
        } else {
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
              'Failed to update record!'
            );
          }
          // this.handleCancel();
        }
      }
    );

  }

  //This method is for changing the Supplier Status from the table
  changeStatus(supplierData: any) {
    this.apiService.updateSupplierStatus(supplierData.SupplierId, supplierData.Isactive).subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.notification.create(
            'success',
            'Success',
            'Status updated successfully!'
          );
        } else {
          this.notification.create(
            'error',
            'Error',
            'Failed to update the status'
          );
        }
      }
    );
  }

  //This method is used for converting the file into Base64 string
  onFileSelected(filedata: any) {
    const file: File = filedata.target.files[0];

    if (file) {
      this.fileName = file.name;
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.tempLogoData = reader.result
        this.fileData = this.tempLogoData.split(',')[1];
      };
      
    }
  }

  constructor(private apiService: ApiDataService, private fb: FormBuilder, private notification: NzNotificationService, private router: Router, private toastr: ToastrService) {
    this.validateForm = this.fb.group({
      supplierRefNo: [null, [Validators.required, Validators.pattern('^[a-zA-Z0-9]*$'), Validators.maxLength(15)]],
      supplierName: [null, [Validators.required, Validators.pattern('^[a-zA-Z ]*$')]],
      businessAddress: [null, Validators.maxLength(150)],
      emailAddress: [null, [Validators.required, Validators.email]],
      phoneNumber: [null, [Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]],
      companyRegisteredNumber: [null, [Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]],
      vatNumber: [null, [Validators.pattern('^[a-zA-Z0-9][A-Za-z0-9]*$'), Validators.maxLength(15)]],
      taxReference: [null, Validators.pattern('^[A-Za-z0-9]*$')],
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
    this.getDataInTable();
    
  }
}

