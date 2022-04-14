import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from './model/login-model';
@Injectable({
  providedIn: 'root'
})
export class ApiDataService {

  user: LoginModel =
    {
      "Email": "sample string 1",
      "Password": "sample string 2"
    }
  constructor(private http: HttpClient) { }

  loginUser():Observable<LoginModel> {
    return this.http.post<LoginModel>(`https://f2e7-106-201-236-89.ngrok.io/api/LogIn`, this.user,  {responseType:'json'})
  }

  getToken(email:string, password:string):Observable<LoginModel> {
    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('grant_type', "password");
    let header = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
    return this.http.post<LoginModel>(`https://f2e7-106-201-236-89.ngrok.io/Token`, body, { headers: header });
  }

  getDataSuppliers(){
    return this.http.get(`https://f2e7-106-201-236-89.ngrok.io/api/Suppliers`);
  }
}
