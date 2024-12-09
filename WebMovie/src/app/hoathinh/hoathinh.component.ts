import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-hoathinh',
  templateUrl: './hoathinh.component.html',
  styleUrls: ['./hoathinh.component.css']
})
export class HoathinhComponent implements OnInit {

  movies: any[] = [];  
  loading: boolean = true;  

  private apiUrl = 'https://phimapi.com/v1/api/danh-sach/hoat-hinh'; 

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getMovies();  
  }

  
  getMovies(): void {
    this.http.get<any>(this.apiUrl).subscribe(
      (data) => {
        this.movies = data.data.items;  
        this.loading = false;  
      },
      (error) => {
        console.error('Lỗi khi lấy dữ liệu phim hoat hình:', error);
        this.loading = false;
      }
    );
  }
}
