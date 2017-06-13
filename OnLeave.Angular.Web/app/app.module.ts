import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms'; // <--- JavaScript import from Angular
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
//import { IndexComponent } from 'app/home/index.component';
import { AppComponent } from './app.component'


//import { FooterComponent } from 'app/shared/footer/footer.component';
//import { LoginComponent } from 'app/shared/login/login.component'

//import { HomeComponent } from 'app/home/home.component'
//import { FooterComponent } from './shared/footer/footer.component';
//import { LoginComponent } from './shared/login/login.component'
//import { SearchComponent } from 'app/shared/search/search.component'

//import { HomeComponent } from './home/home.component'

//import { HomeService } from '../app/services/home.service'

//const appRoutes: Routes = [
//    { path: 'home', component: IndexComponent },    
//    { path: '', redirectTo: '/home', pathMatch: 'full' }    
//];

/* Contact Imports */
import { HomeModule } from './home/home.module';
import { AdminModule } from './admin/admin.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
    imports: [BrowserModule, HttpModule, HomeModule, AdminModule, SharedModule, FormsModule, AppRoutingModule],
    declarations: [AppComponent ],
    exports: [],
    //providers: [HomeService],
    bootstrap: [AppComponent ]
})
export class AppModule { }
