import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from './model/login-model';
import { Supplier } from './model/supplier';
import { Monthlyinvoice } from './model/monthlyinvoice';
import { Profile } from './model/profile';
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
    return this.http.post<LoginModel>(`http://apiteam1.azurewebsites.net/api/LogIn`, this.user, { responseType: 'json' })
  }

  getToken(email: string, password: string): Observable<LoginModel> {
    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('grant_type', "password");
    let header = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
    return this.http.post<LoginModel>(`http://apiteam1.azurewebsites.net/Token`, body, { headers: header });
  }

  addSupplier(token: string): Observable<Supplier> {
    let addSupplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token)
    return this.http.post<Supplier>(`http://apiteam1.azurewebsites.net/api/Suppliers`, this.supplier, { headers: addSupplierHeader });
  }

  editSupplier(id: number, token: string): Observable<Supplier> {
    let supplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token)
    return this.http.put<Supplier>(`http://apiteam1.azurewebsites.net/api/Suppliers` + id, this.supplier, { headers: supplierHeader });
  }

  getDataSuppliers(token: string) {
    let getSupplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    return this.http.get(`http://apiteam1.azurewebsites.net/api/Suppliers`, { headers: getSupplierHeader });
  }

  deleteSupplier(supplierId: number, token: string) {
    let deleteSupplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    return this.http.delete(`http://apiteam1.azurewebsites.net/api/Suppliers/` + supplierId, { headers: deleteSupplierHeader });
  }

  updateSupplierStatus(id: number, status: boolean, token: string) {
    let updateSupplierStatusHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    let url = `http://apiteam1.azurewebsites.net/api/Suppliers/?id=` + id + `&isActive=` + status;
    return this.http.patch(url, {}, { headers: updateSupplierStatusHeader });
  }

  getMonthlyInvoiceData(month: number, year: number, token: string) {
    let getMonthlyInvoiceDataHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    return this.http.get(`http://apiteam1.azurewebsites.net/api/MonthlyInvoices?month=` + month + `&year=` + year, { headers: getMonthlyInvoiceDataHeader })
  }

  addMonthlyInvoicesData(monthlyJsonObject: any, token: string): Observable<Monthlyinvoice> {
    console.log(monthlyJsonObject);
    let addmonthlyInvoiceDataHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    return this.http.post<Monthlyinvoice>(`http://apiteam1.azurewebsites.net/api/MonthlyInvoices`, monthlyJsonObject, { headers: addmonthlyInvoiceDataHeader });
  }

  approveSupplier(idArray: any, month: number, year: number, token: string) {
    let approveSupplierHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    let url = `http://apiteam1.azurewebsites.net/api/MonthlyInvoices?month=` + month + `&year=` + year;
    return this.http.patch(url, idArray, { headers: approveSupplierHeader });
  }

  emailSelectedSuppliers(arrayOfID: number, month: number, year: number, token: string) {
    let emailSuppliersHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    let url = `http://apiteam1.azurewebsites.net/api/Email?month=` + month + `&year=` + year;
    return this.http.post(url, arrayOfID, { headers: emailSuppliersHeader });
  }

  downloadPDF(IDArray: number, month: number, year: number, token: string) {
    let downloadPDFHeader = new HttpHeaders().set('Authorization', 'bearer ' + token);
    let url = `http://apiteam1.azurewebsites.net/api/PDF?month=` + month + `&year=` + year;
    return this.http.post(url, IDArray, { headers: downloadPDFHeader });
  }

  getProfileData(){
    return this.http.get(`http://apiteam1.azurewebsites.net/api/Profile`);
  }

  setProfileData(jsonObject:any): Observable<Profile>{
    let id = jsonObject.CId;
    return this.http.post<Profile>(`http://apiteam1.azurewebsites.net/api/Profile/`+ id, jsonObject);
  }
  constructor(private http: HttpClient) { }
} 
