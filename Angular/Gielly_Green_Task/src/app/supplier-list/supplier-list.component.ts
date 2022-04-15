import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faPen, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';
import { ApiDataService } from '../api-data.service';
import Swal from 'sweetalert2';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table'
@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  fileName = '';
  editIcon = faPen;
  plusIcon = faPlus;
  deleteIcon = faTrash;
  supplierListData:any;
  isCollapsed = false;
  isVisible = false;
  validateForm!:FormGroup;
  msg: any;
  fileData:any;
  
  showModal(): void {
    this.isVisible = true;
  }
  

  handleChange(info: NzUploadChangeParam): void {
    if (info.file.status !== 'uploading') {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === 'done') {
      this.msg.success(`${info.file.name} file uploaded successfully`);
    } else if (info.file.status === 'error') {
      this.msg.error(`${info.file.name} file upload failed.`);
    }
  }

  handleOk(): void {
    if (this.validateForm.valid) {
      alert('validated all fields');
      this.addSupplier();
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

  addSupplier(){
    
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
    console.log(this.apiService.supplier);
    // this.apiService.addSupplier().subscribe((response: any) => console.log(response));
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }

  getDataInTable(){
    this.apiService.getDataSuppliers().subscribe((data:any) => {
      this.supplierListData = data.Result;
      console.log(this.supplierListData);
    })
  }

  deleteSupplier(supplierId:number){
    alert(supplierId);
  }

  editSupplier(supplierId:number){
    alert(supplierId);
  }

  changeStatus(supplierData: any){
    this.apiService.updateSupplierStatus(supplierData.SupplierId,supplierData.Isactive).subscribe(
      (response:any) => {
        console.log(response);
        if(response.ResponseStatus == 1){
          this.notification.create(
            'success',
            'Success',
            'Status updated successfully!'
          );
        } else{
          this.notification.create(
            'error',
            'Error',
            'Failed to update the status'
          );
        }
       
      }
    );
  }
 
  onFileSelected(fileData:any){
    const file:File = fileData.target.files[0]; 
    if (file) {
        this.fileName = file.name;
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          this.fileData = reader.result;
          console.log(this.fileData);
      };
      
    }
  }

  constructor(private apiService: ApiDataService, private fb: FormBuilder, private notification: NzNotificationService) {
    
    this.validateForm = this.fb.group({
      supplierRefNo: [null, [Validators.required, Validators.pattern('^[a-zA-Z0-9][A-Za-z0-9]*$'), Validators.maxLength(15)]],
      supplierName: [null, [Validators.required, Validators.pattern('^[a-zA-Z][a-zA-Z ]*$')]],
      businessAddress: [null, Validators.maxLength(150)],
      emailAddress: [null, [Validators.required, Validators.email]],
      phoneNumber:[null, [Validators.pattern('^[0-9]*$'),Validators.maxLength(15)]],
      companyRegisteredNumber: [null, [Validators.pattern('^[0-9]*$'),Validators.maxLength(15)]],
      vatNumber: [null, [Validators.pattern('^[0-9]*$'),Validators.maxLength(15)]],
      taxReference:[null, Validators.pattern('^[A-Za-z0-9]*$')],
      companyAddress: [null, Validators.maxLength(150)],
      isSupplierActive:[false],
      invoiceLogo: [null]
    })
   }

  ngOnInit(): void {
    this.getDataInTable();
  }
}
