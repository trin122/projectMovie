import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginData = {
    name: '',
    password: ''
  };
  errorMessage: string | null = null;
  isLoading = false;

  constructor(private apiService: ApiService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    const user = localStorage.getItem('user');
    if (user) {
      this.router.navigate(['/']);
    }
  }

  onLogin(): void {
    
    if (!this.loginData.name || !this.loginData.password) {
      this.toastr.warning('Vui lòng nhập tên và mật khẩu!', 'Thông báo');
      return; 
    }

    this.isLoading = true; 
    this.apiService.Login(this.loginData).subscribe(
      (response) => {
        this.isLoading = false; 

        
        const user = {
          Id: response.Id,
          Name: response.Name,
          Email: response.Email,
          Password: this.loginData.password 
        };
        localStorage.setItem('user', JSON.stringify(user));

        this.toastr.success('Đăng nhập thành công!', 'Chào mừng bạn!');
        this.router.navigate(['/']).then(() => {
          setTimeout(() => {
            
            window.location.reload();
          }, 1000);
        });
      },
      (error) => {
        this.isLoading = false; // Dừng loading
        if (error.status === 0) {
          this.toastr.error('Vui lòng nhập đầy đủ thông tin');
        } else {
          this.toastr.error('Tên đăng nhập hoặc mật khẩu không đúng', 'Đăng nhập thất bại!');
        }
      }
    );
  }
}
