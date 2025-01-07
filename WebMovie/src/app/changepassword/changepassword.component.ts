import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent implements OnInit {

  changePasswordData = {
    name: '',
    oldPassword: '',
    newPassword: '',
    confirmPassword: ''
  };
  errorMessage: string | null = null;
  isLoading = false;

  constructor(private apiService: ApiService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    const user = localStorage.getItem('user');
    if (!user) {
      this.router.navigate(['/login']);
    } else {
      this.changePasswordData.name = JSON.parse(user).Name; 
    }
  }

  onChangePassword(): void {
    if (!this.changePasswordData.oldPassword || !this.changePasswordData.newPassword || !this.changePasswordData.confirmPassword) {
      this.toastr.warning('Vui lòng nhập đầy đủ thông tin!', 'Thông báo');
      return; 
    }

    if (this.changePasswordData.newPassword !== this.changePasswordData.confirmPassword) {
      this.toastr.warning('Mật khẩu mới và xác nhận mật khẩu không khớp!', 'Thông báo');
      return;
    }

    this.isLoading = true; 
    this.apiService.ChangePassword(this.changePasswordData).subscribe(
      (response) => {
        this.isLoading = false; 
        this.toastr.success('Đổi mật khẩu thất bại!', 'Thông báo');
        this.router.navigate(['/']);
      },
      (error) => {
        this.isLoading = false; 
        if (error.status === 0) {
          this.toastr.error('Vui lòng nhập đầy đủ thông tin');
        } else {
          this.toastr.success('Đổi mật khẩu thành công!', 'Thông báo');
          this.router.navigate(['/']);
        }
      }
    );
  }
}
