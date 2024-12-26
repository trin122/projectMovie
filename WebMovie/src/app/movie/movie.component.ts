import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  movie: any;
  episodes: any[] = [];
  selectedEpisode: any;
  safeUrl: SafeResourceUrl | null = null;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit(): void {
    const slug = this.route.snapshot.params['slug'];
    this.getMovieDetails(slug);
  }

  getMovieDetails(slug: string): void {
    const url = `https://phimapi.com/phim/${slug}`;
    this.http.get(url).pipe(
      catchError(error => {
        console.error('Error fetching movie details', error);
        return of(null);
      })
    ).subscribe((response: any) => {
      if (response && response.status) {
        this.movie = response.movie;
        this.episodes = response.episodes;

        // Chọn tập đầu tiên nếu có
        if (this.episodes.length > 0) {
          this.selectEpisode(this.episodes[0].server_data[0]); // Chọn server đầu tiên của tập đầu tiên
        }
      } else {
        console.error('Failed to load movie details');
      }
    });
  }

  selectEpisode(server: any): void {
    this.selectedEpisode = server;
    this.safeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(server.link_embed); // Cập nhật link video
  }
}
