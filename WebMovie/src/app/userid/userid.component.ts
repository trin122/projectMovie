import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-userid',
  templateUrl: './userid.component.html',
  styleUrls: ['./userid.component.css']
})
export class UseridComponent implements OnInit {
  user: any = null;  // Dữ liệu người dùng sẽ được lưu ở đây
  errorMessage: string | null = null;  // Lỗi nếu không lấy được dữ liệu

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    // Lấy userId từ URL
    this.route.params.subscribe(params => {
      const userId = params['id'];  // Lấy userId từ params

      if (userId) {
        // Gọi API để lấy thông tin người dùng
        this.http.get(`https://localhost:44317/api/User/${userId}`).subscribe(
          (data: any) => {
            this.user = data;  // Lưu thông tin người dùng vào biến `user`
            this.errorMessage = null;  // Đặt lỗi thành null nếu thành công
          },
          (error) => {
            console.error(error);
            this.errorMessage = 'Không thể tải thông tin người dùng.';
          }
        );
      } else {
        this.errorMessage = 'Không có ID người dùng trong URL.';
      }
    });
  }
}
