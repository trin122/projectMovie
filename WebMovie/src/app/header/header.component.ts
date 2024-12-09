import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  userName: string | null = null;
  userId: number | null = null;  
  searchKeyword: string = ''; 

  constructor(private router: Router, private http: HttpClient) {}

  ngOnInit(): void {
    const user = localStorage.getItem('user');
    if (user) {
      const parsedUser = JSON.parse(user);
      this.userName = parsedUser.Name;
      this.userId = parsedUser.Id;
    }
  }

  logout(): void {
    localStorage.removeItem('user');
    this.userName = null;
    this.userId = null; 
    this.router.navigate(['']);
   
    window.location.reload();
  }

  search(): void {
    if (this.searchKeyword) {
      const url = `https://phimapi.com/v1/api/tim-kiem?keyword=${this.searchKeyword}&limit=10`;
      
      
      this.http.get(url).subscribe((response: any) => {
       
        this.router.navigate(['/timkiem'], {
          queryParams: { keyword: this.searchKeyword, movies: JSON.stringify(response.data.items) }
        });
      });
    }
  }
}