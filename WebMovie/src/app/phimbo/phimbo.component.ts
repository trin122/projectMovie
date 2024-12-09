import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-phimbo',
  templateUrl: './phimbo.component.html',
  styleUrls: ['./phimbo.component.css']
})
export class PhimboComponent implements OnInit {

  movies: any[] = [];  
  loading: boolean = true;  

  private apiUrl = 'https://phimapi.com/v1/api/danh-sach/phim-bo'; 

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
        console.error('Lỗi khi lấy dữ liệu phim bộ:', error);
        this.loading = false;
      }
    );
  }
}
