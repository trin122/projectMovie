import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ToastrService } from 'ngx-toastr'; // Import ToastrService

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // Dữ liệu đăng ký
  user = {
    email: '',
    name: '',
    password: '',
    confirmPassword: ''
  };

  errorMessage: string | null = null;

  constructor(private apiService: ApiService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  
  onRegister(): void {
    
    if (!this.user.email || !this.user.name || !this.user.password || !this.user.confirmPassword) {
      this.errorMessage = "Vui lòng nhập đầy đủ thông tin.";
      this.toastr.warning('Vui lòng nhập đầy đủ thông tin.');
      return;
    }

    
    if (this.user.password !== this.user.confirmPassword) {
      this.errorMessage = "Passwords do not match!";
      this.toastr.error('Mật khẩu và xác nhận mật khẩu không khớp.');
      return;
    }

    
    this.apiService.Register(this.user).subscribe(
      (response) => {
        
        localStorage.setItem('user', JSON.stringify(response));
        
        
        this.toastr.success('Đăng ký thành công!', 'Chào mừng bạn!');

        
        this.router.navigate(['/']).then(() => {
          setTimeout(() => {
            window.location.reload(); 
          }, 1000); 
        });
      },
      (error) => {
        
        this.errorMessage = 'Registration failed. Please try again.';
        this.toastr.error('Đăng ký thất bại!', 'Vui lòng thử lại.');
      }
    );
  }
}
