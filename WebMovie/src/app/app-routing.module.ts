import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ChitietphimComponent } from './chitietphim/chitietphim.component';

import { PhimboComponent } from './phimbo/phimbo.component';
import { UserComponent } from './user/user.component';
import { PhimleComponent } from './phimle/phimle.component';
import { UseridComponent } from './userid/userid.component';
import { TimkiemComponent } from './timkiem/timkiem.component';
import { AboutComponent } from './about/about.component';
import { HoathinhComponent } from './hoathinh/hoathinh.component';
import { MovieComponent } from './movie/movie.component';
import { XemtatcaComponent } from './xemtatca/xemtatca.component';
import { ChangepasswordComponent } from './changepassword/changepassword.component';
// import { XemphimComponent } from './xemphim/xemphim.component';

const routes: Routes = [
  { path: '', component: HomeComponent },  // Trang mặc định sẽ là HomeComponent
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'chitietphim/:slug', component: ChitietphimComponent },
  { path: 'xemphim/:slug', component: MovieComponent },
  { path: 'phimbo', component: PhimboComponent },
  { path: 'users', component: UserComponent },
  { path: 'phimle', component: PhimleComponent },
  { path: 'user/:id', component: UseridComponent },
  { path: 'timkiem', component: TimkiemComponent },
  { path: 'about', component: AboutComponent },
  { path: 'hoathinh', component: HoathinhComponent },
  { path: 'xemtatca', component: XemtatcaComponent },
  { path: 'doimatkhau', component: ChangepasswordComponent },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
