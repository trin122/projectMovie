import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-userid',
  templateUrl: './userid.component.html',
  styleUrls: ['./userid.component.css']
})
export class UseridComponent implements OnInit {
  userData: any = {}; 
  isLoading = false;

  constructor(private apiService: ApiService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.isLoading = true;
    const user = localStorage.getItem('user');
    if (user) {
      const userId = JSON.parse(user).Id; 
      this.apiService.getUser(userId).subscribe(
        (response) => {
          this.userData = response;
          this.isLoading = false;
        },
        (error) => {
          this.isLoading = false;
          this.toastr.error('Lỗi khi lấy thông tin người dùng', 'Lỗi!');
          console.error(error); 
        }
      );
    } else {
      this.isLoading = false;
      this.toastr.error('Người dùng chưa đăng nhập!', 'Lỗi!');
    }
  }
}
