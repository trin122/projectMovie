import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { UserComponent } from './user/user.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';

// Import ToastrModule và BrowserAnimationsModule
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ChitietphimComponent } from './chitietphim/chitietphim.component';

import { PhimboComponent } from './phimbo/phimbo.component';
import { PhimleComponent } from './phimle/phimle.component';
import { UseridComponent } from './userid/userid.component';
import { TimkiemComponent } from './timkiem/timkiem.component';
import { AboutComponent } from './about/about.component';
import { HoathinhComponent } from './hoathinh/hoathinh.component';
import { MovieComponent } from './movie/movie.component';
import { SafeUrlPipe } from './safe-url.pipe';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    UserComponent,
    RegisterComponent,
    ChitietphimComponent,
    PhimboComponent,
    PhimleComponent,
    UseridComponent,
    TimkiemComponent,
    AboutComponent,
    HoathinhComponent,
    MovieComponent,
    SafeUrlPipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,  // Import BrowserAnimationsModule
    ToastrModule.forRoot()     // Cấu hình ToastrModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
