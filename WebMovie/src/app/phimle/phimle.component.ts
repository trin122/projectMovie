import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-phimle',
  templateUrl: './phimle.component.html',
  styleUrls: ['./phimle.component.css']
})
export class PhimleComponent implements OnInit {

  movies: any[] = [];  
  loading: boolean = true;  

  private apiUrl = 'https://phimapi.com/v1/api/danh-sach/phim-le'; 

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
        console.error('Lỗi khi lấy dữ liệu phim lẻ:', error);
        this.loading = false;
      }
    );
  }

}
