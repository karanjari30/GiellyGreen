import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from './model/login-model';
import { Supplier } from './model/supplier';
@Injectable({
  providedIn: 'root'
})
export class ApiDataService {

  user: LoginModel =
    {
      "Email": "sample string 1",
      "Password": "sample string 2"
    }

  supplier: Supplier =
    {
      "SupplierId": 1,
      "SupplierName": "sample string 2",
      "SupplierReference": "sample string 3",
      "BusinessAddress": "sample string 4",
      "EmailAddress": "sample string 5",
      "PhoneNumber": "sample string 6",
      "CompanyRegisterNumber": "sample string 7",
      "VATNumber": "sample string 8",
      "TaxReference": "sample string 9",
      "CompanyRegisterAddress": "sample string 10",
      "logo": "QEA=",
      "Isactive": true
    }

  constructor(private http: HttpClient) { }

  loginUser(): Observable<LoginModel> {
    return this.http.post<LoginModel>(`https://8147-106-201-236-89.ngrok.io/api/LogIn`, this.user, { responseType: 'json' })
  }

  getToken(email: string, password: string): Observable<LoginModel> {
    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('grant_type', "password");
    let header = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
    return this.http.post<LoginModel>(`https://8147-106-201-236-89.ngrok.io/Token`, body, { headers: header });
  }

  addSupplier(): Observable<Supplier> {
    console.log(this.supplier); 
    return this.http.post<Supplier>(`https://8147-106-201-236-89.ngrok.io/api/Suppliers`, this.supplier);
  }

  getDataSuppliers() {
    return this.http.get(`https://8147-106-201-236-89.ngrok.io/api/Suppliers`);
  }

  updateSupplierStatus(id: number, status: boolean){
    let url = `https://8147-106-201-236-89.ngrok.io/api/Suppliers/?id=`+id+`&isActive=`+status;
    return this.http.patch(url, this.supplier);
  }
}
