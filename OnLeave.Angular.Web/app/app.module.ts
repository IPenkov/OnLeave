import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FooterComponent } from './shared/footer/footer.component';
import { LoginComponent } from './shared/login/login.component'
import { HomeComponent } from './home/home.component'

@NgModule({
    imports: [BrowserModule],
    declarations: [AppComponent, FooterComponent, LoginComponent, HomeComponent],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
