import { NgModule }      from '@angular/core';
import { CommonModule }       from '@angular/common';
//import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

//import { AppComponent } from './app.component';
//import { FooterComponent } from './shared/footer/footer.component';
//import { LoginComponent } from './shared/login/login.component'
import { HomeComponent } from './home.component'
import { IndexComponent } from './index.component'

import { FooterComponent } from 'app/shared/footer/footer.component';
import { LoginComponent } from 'app/shared/login/login.component'
import { SearchComponent } from 'app/shared/search/search.component'

import { HomeService } from 'app/services/home.service'

import { HomeRoutingModule } from './home-routing.module';

@NgModule({    
    imports: [CommonModule, HomeRoutingModule],
    declarations: [IndexComponent, HomeComponent, LoginComponent, SearchComponent, FooterComponent],
    exports: [IndexComponent],
    providers: [HomeService],  
})
export class HomeModule { }
