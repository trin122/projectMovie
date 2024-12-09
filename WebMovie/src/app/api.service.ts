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

  // GET tất cả người dùng
  Getuser(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + '/User'); 
  }

  // GET lấy người dùng theo id
  getUserById(userId: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${userId}`);
  }

  // POST đăng nhập
  Login(val: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Login', val); 
  }

  // POST để đăng ký 
  Register(val: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Register', val);
  }

  // GET tìm kiếm phim 
  searchMovies(query: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.searchApiUrl}?search=${query}`);
  }

}
