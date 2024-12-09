import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-timkiem',
  templateUrl: './timkiem.component.html',
  styleUrls: ['./timkiem.component.css']
})
export class TimkiemComponent implements OnInit {
  keyword: string = '';
  movies: any[] = [];

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    // Lấy thông tin từ queryParams
    this.route.queryParams.subscribe(params => {
      this.keyword = params['keyword'];
      this.movies = JSON.parse(params['movies'] || '[]');
    });
  }
}
