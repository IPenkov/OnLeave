import { NgModule }      from '@angular/core';
import { CommonModule }       from '@angular/common';
//import { HttpModule } from '@angular/http';

//import { AppComponent } from './app.component';
//import { FooterComponent } from './shared/footer/footer.component';
//import { LoginComponent } from './shared/login/login.component'
import { HomeComponent } from './home.component'

import { HomeService } from 'app/services/home.service'

@NgModule({    
    imports: [CommonModule],
    declarations: [HomeComponent],
    exports: [HomeComponent],
    providers: [HomeService],  
})
export class HomeModule { }
