import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ApiDataService } from '../api-data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  validateForm!: FormGroup;
  passwordVisible = false;
  password?: string;
  constructor(private fb: FormBuilder, private router: Router, private apiService: ApiDataService) { }

  ngOnInit(): void {
    if(sessionStorage.getItem('userSessionToken')){
      this.router.navigate(['/suppliers-list']);
    } else{
      this.router.navigate(['/login']);
    }
    this.validateForm = this.fb.group({
      email: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
  }

  submitForm() {
    if (this.validateForm.valid) {
      this.apiService.user.Email = this.validateForm.controls['email'].value;
      this.apiService.user.Password = this.validateForm.controls['password'].value;
      this.apiService.loginUser().subscribe(
        (response: any) => {
          console.log(response)
          if(response.ResponseStatus == 0){
            Swal.fire({
              title: 'Error!',
              text: 'Email/Password is incorrect!',
              icon: 'error',
              confirmButtonText: 'Ok',
            });
          } else{
            this.apiService.getToken(this.validateForm.controls['email'].value, this.validateForm.controls['password'].value).subscribe((response: any) => {
              sessionStorage.setItem('userSessionToken', response.access_token)
              this.router.navigate(['/suppliers-list']);
            })
          }
        },
        
      );
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

}
