import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-chitietphim',
  templateUrl: './chitietphim.component.html',
  styleUrls: ['./chitietphim.component.css']
})
export class ChitietphimComponent implements OnInit {
  movie: any;
  slug: string = '';

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router 
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.slug = params['slug'];
      this.getMovieDetails(this.slug);
    });
  }

  getMovieDetails(slug: string): void {
    const url = `https://phimapi.com/phim/${slug}`;
    this.http.get(url).subscribe((response: any) => {
      if (response.status) {
        this.movie = response.movie;
      } else {
        console.error('Failed to load movie details');
      }
    });
  }

  goToWatchPage(): void {
    this.router.navigate(['/xemphim', this.movie.slug]);
  }
}
