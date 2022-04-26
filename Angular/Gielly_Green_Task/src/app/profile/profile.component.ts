import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { faTruck, faFileInvoiceDollar, faEllipsisVertical, faUser, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { ApiDataService } from '../api-data.service';
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

  onSubmit() {
    this.apiService.setProfileData(this.createModel()).subscribe(
      (response: any) => console.log(response)
    )
  }

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

  constructor(private router: Router, private fb: FormBuilder, private apiService: ApiDataService) {
    this.profileForm = this.fb.group({
      companyName: '',
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
