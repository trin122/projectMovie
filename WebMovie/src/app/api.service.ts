import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  readonly baseUrl = "https://localhost:44317/api"; 
  readonly searchApiUrl = 'https://phimapi.com/v1/api/tim-kiem'; 

  constructor(private http: HttpClient) {}

  // tất cả người dùng
  Getuser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + '/User'); 
  }

  // lấy người dùng theo id
  getUser(userId: number): Observable<any> {
    const url = `${this.baseUrl}/User/${userId}`;
    return this.http.get(url);
  }

  // đăng nhập
  Login(val: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Login', val); 
  }

  // để đăng ký 
  Register(val: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Register', val);
  }
// doi mat khau
ChangePassword(changePasswordData: any): Observable<any> {
  return this.http.post(`${this.baseUrl}/ChangePassword`, changePasswordData);
}

}
