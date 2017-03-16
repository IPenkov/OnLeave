import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { FooterComponent } from './shared/footer/footer.component';
import { LoginComponent } from './shared/login/login.component'
//import { HomeComponent } from './home/home.component'

//import { HomeService } from '../app/services/home.service'

/* Contact Imports */
import { HomeModule } from './home/home.module';

@NgModule({
    imports: [BrowserModule, HttpModule, HomeModule],
    declarations: [AppComponent, FooterComponent, LoginComponent],
    //providers: [HomeService],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
