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

  
  loginUser(): Observable<LoginModel> {
    return this.http.post<LoginModel>(`http://cf9c-106-201-236-89.ngrok.io/api/LogIn`, this.user, { responseType: 'json' })
  }

  getToken(email: string, password: string): Observable<LoginModel> {
    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('grant_type', "password");
    let header = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
    return this.http.post<LoginModel>(`http://cf9c-106-201-236-89.ngrok.io/Token`, body, { headers: header });
  }

  addSupplier(): Observable<Supplier> {
    console.log(this.supplier);
    return this.http.post<Supplier>(`http://cf9c-106-201-236-89.ngrok.io/api/Suppliers`, this.supplier);
  }

  editSupplier(id: number, token: string): Observable<Supplier> {
    let supplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token)
    return this.http.put<Supplier>(`http://cf9c-106-201-236-89.ngrok.io/api/Suppliers/` + id, this.supplier, { headers: supplierHeader });
  }

  getDataSuppliers() {
    return this.http.get(`http://cf9c-106-201-236-89.ngrok.io/api/Suppliers`);
  }

  deleteSupplier(supplierId: number, token: string) {
    let deleteSupplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    return this.http.delete(`http://cf9c-106-201-236-89.ngrok.io/api/Suppliers/` + supplierId, { headers: deleteSupplierHeader });
  }

  updateSupplierStatus(id: number, status: boolean) {
    let url = `http://cf9c-106-201-236-89.ngrok.io/api/Suppliers/?id=` + id + `&isActive=` + status;
    return this.http.patch(url, this.supplier);
  }

  getMonthlyInvoiceData(month: number, year: number) {
    return this.http.get(`http://cf9c-106-201-236-89.ngrok.io/api/MonthlyInvoices?month=` + month + `&year=` + year)
  }

  addMonthlyInvoicesData(monthlyJsonObject: any): Observable<Monthlyinvoice>{
    return this.http.post<Monthlyinvoice>(`http://cf9c-106-201-236-89.ngrok.io/api/MonthlyInvoices`, monthlyJsonObject);
  }

  approveSupplier(idArray: any, month: number, year: number){
    let url = `http://cf9c-106-201-236-89.ngrok.io/api/MonthlyInvoices?month=`+month+`&year=`+year;
    return this.http.patch(url, idArray);
  }

  emailSelectedSuppliers(arrayOfID: number, month: number, year: number) {
    let url = `http://cf9c-106-201-236-89.ngrok.io/api/Email?month=`+month+`&year=`+year;
    return this.http.post(url, arrayOfID);
  }

  downloadPDF(IDArray:number, month:number, year:number){
    let url = `http://cf9c-106-201-236-89.ngrok.io/api/PDF?month=`+month+`&year=`+year;
    return this.http.post(url, IDArray);
  }
  constructor(private http: HttpClient) { }
} 
