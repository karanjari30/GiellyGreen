import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faTruck, faFileInvoiceDollar, faEllipsisVertical, faUser, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { ApiDataService } from '../api-data.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profileForm!: FormGroup;
  profileData: any;
  isCollapsed = false;
  profileIcon = faUser;
  menuIcon = faEllipsisVertical;
  logoutIcon = faRightFromBracket;
  supplierIcon = faTruck;
  invoiceIcon = faFileInvoiceDollar;

  //This method is for collapsing the Menu
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

  //This method is called whenever the submit button is clicked, to save the data
  onSubmit() {
    if (this.profileForm.valid) {
      this.apiService.setProfileData(this.createModel()).subscribe(
        (response: any) => {
          console.log(response)
          if (response.ResponseStatus == 1) {
            this.notification.create(
              'success',
              'Success',
              'Data saved successfully!'
            );
          } else {
            this.notification.create(
              'error',
              'Error',
              'Something went wrong. Please try again later!'
            );
          }
        }
      )
    } else {
      Object.values(this.profileForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  //This method will get the data from API in an object
  getProfileAPIData() {
    this.apiService.getProfileData().subscribe(
      (response: any) => {
        if (response.ResponseStatus == 1) {
          this.profileData = response.Result[0];
          this.seProfileAPIDataInForm();
        } else {
          alert('no data found');
        }
      }
    )
  }

  //This method will fill the form fields using the data we got from API in an object
  seProfileAPIDataInForm() {
    this.profileForm.patchValue({
      companyName: this.profileData.CompanyName,
      addressLine: this.profileData.AddressLine,
      city: this.profileData.City,
      zipCode: this.profileData.ZipCode,
      country: this.profileData.Country,
      defaultVat: this.profileData.DefaultVat,
    })
  }

  //This method will create a JSON type Object model to pass it in the API call
  createModel() {
    var body = {
      CId: this.profileData.CId,
      CompanyName: this.profileForm.value.companyName,
      AddressLine: this.profileForm.value.addressLine,
      City: this.profileForm.value.city,
      ZipCode: this.profileForm.value.zipCode,
      Country: this.profileForm.value.country,
      DefaultVat: this.profileForm.value.defaultVat,
    }
    return body;
  }

  constructor(private router: Router, private fb: FormBuilder, private apiService: ApiDataService, private notification: NzNotificationService) {
    this.profileForm = this.fb.group({
      companyName: [null, Validators.required],
      addressLine: '',
      city: '',
      zipCode: '',
      country: '',
      defaultVat: '',
    })
  }

  ngOnInit(): void {
    this.isCollapsed = false;
    this.getProfileAPIData();
  }
}
