import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';  // Nếu cần thông báo lỗi

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: any[] = [];  // Mảng chứa dữ liệu người dùng

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    const apiUrl = 'https://localhost:44317/api/User';
    this.http.get<any[]>(apiUrl).subscribe(
      (data) => {
        this.users = data;  // Lưu dữ liệu vào mảng users
      },
      (error) => {
        console.error('Có lỗi khi tải danh sách người dùng:', error);
        this.toastr.error('Không thể tải danh sách người dùng', 'Lỗi');
      }
    );
  }
}
