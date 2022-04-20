import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from './model/login-model';
import { Supplier } from './model/supplier';
import { Monthlyinvoice } from './model/monthlyinvoice';
@Injectable({
  providedIn: 'root'
})
export class ApiDataService {

  token: any;
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

  monthlyInvoiceObject: Monthlyinvoice =
    {
      "InvoiceId": 1,
      "Custom1": "sample string 2",
      "Custom2": "sample string 3",
      "Custom3": "sample string 4",
      "Custom4": "sample string 5",
      "Custom5": "sample string 6",
      "InvoiceReferenceId": "sample string 7",
      "InvoiceYear": 1,
      "InvoiceMonth": 1,
      "InvoiceDate": "2022-04-20T13:26:02.6467167+05:30",
      "InvoiceViewList": [
        {
          "MontlyInvoiceId": 1,
          "HairService": 1.0,
          "BeautyService": 1.0,
          "CustomHeader1": 1.0,
          "CustomHeader2": 1.0,
          "CustomHeader3": 1.0,
          "CustomHeader4": 1.0,
          "CustomHeader5": 1.0,
          "NetAmount": 1.0,
          "VATAmount": 1.0,
          "GrossAmount": 1.0,
          "AdvancePay": 1.0,
          "BalanceDue": 1.0,
          "IsApprove": true,
          "SupplierId": 1,
          "InvoiceId": 1
        },
      ]
    }
 

  loginUser(): Observable<LoginModel> {
    return this.http.post<LoginModel>(`http://a467-106-201-236-89.ngrok.io/api/LogIn`, this.user, { responseType: 'json' })
  }

  getToken(email: string, password: string): Observable<LoginModel> {
    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('grant_type', "password");
    let header = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
    return this.http.post<LoginModel>(`http://a467-106-201-236-89.ngrok.io/Token`, body, { headers: header });
  }

  addSupplier(): Observable<Supplier> {
    console.log(this.supplier);
    return this.http.post<Supplier>(`http://a467-106-201-236-89.ngrok.io/api/Suppliers`, this.supplier);
  }

  editSupplier(id: number, token: string): Observable<Supplier> {
    let supplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token)
    return this.http.put<Supplier>(`http://a467-106-201-236-89.ngrok.io/api/Suppliers/` + id, this.supplier, { headers: supplierHeader });
  }

  getDataSuppliers() {
    return this.http.get(`http://a467-106-201-236-89.ngrok.io/api/Suppliers`);
  }

  deleteSupplier(supplierId: number, token: string) {
    let deleteSupplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    return this.http.delete(`http://a467-106-201-236-89.ngrok.io/api/Suppliers/` + supplierId, { headers: deleteSupplierHeader });
  }

  updateSupplierStatus(id: number, status: boolean) {
    let url = `http://a467-106-201-236-89.ngrok.io/api/Suppliers/?id=` + id + `&isActive=` + status;
    return this.http.patch(url, this.supplier);
  }

  getMonthlyInvoiceData(month: number, year: number) {
    return this.http.get(`http://a467-106-201-236-89.ngrok.io/api/MonthlyInvoices?month=` + month + `&year=` + year)
  }

  addMonthlyInvoicesData(): Observable<Monthlyinvoice>{
    return this.http.post<Monthlyinvoice>(`http://a467-106-201-236-89.ngrok.io/api/MonthlyInvoices`, this.monthlyInvoiceObject);
  }

  constructor(private http: HttpClient) { }
} 
