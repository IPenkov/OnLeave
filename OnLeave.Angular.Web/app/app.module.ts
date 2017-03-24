import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms'; // <--- JavaScript import from Angular

import { AppComponent } from './app.component';
import { FooterComponent } from './shared/footer/footer.component';
import { LoginComponent } from './shared/login/login.component'
import { SearchComponent } from 'app/shared/search/search.component'

//import { HomeComponent } from './home/home.component'

//import { HomeService } from '../app/services/home.service'

/* Contact Imports */
import { HomeModule } from './home/home.module';

@NgModule({
    imports: [BrowserModule, HttpModule, HomeModule, FormsModule],
    declarations: [AppComponent, FooterComponent, LoginComponent, SearchComponent],
    //providers: [HomeService],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
