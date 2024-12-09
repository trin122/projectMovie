import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface Movie {
  _id: string;
  name: string;
  origin_name: string;
  year: number;
  poster_url: string;
  thumb_url: string;
  slug: string; 
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movies: Movie[] = []; // Biến để chứa danh sách phim
  errorMessage: string = ''; // Biến để lưu thông báo lỗi
  currentPage: number = 1; // Biến trang hiện tại
  totalMovies: number = 0; // Tổng số bộ phim

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadMovies(); 
  }

  
  loadMovies(): void {
    const apiUrl = `https://phimapi.com/danh-sach/phim-moi-cap-nhat?page=${this.currentPage}`;

    this.http.get<any>(apiUrl).subscribe(
      response => {
        if (response && response.items) {
          
          this.movies = [...this.movies, ...response.items];
          this.totalMovies = this.movies.length;
          
         
          if (this.totalMovies < 21) {
            this.currentPage++;
            this.loadMovies();
          }
        } else {
          this.errorMessage = 'Không có dữ liệu phim!';
        }
      },
      error => {
        this.errorMessage = 'Lỗi khi lấy danh sách phim: ' + error.message;
      }
    );
  }
}
