import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FooterComponent } from './shared/footer/footer.component';

@NgModule({
  imports: [BrowserModule],
  declarations: [AppComponent, FooterComponent],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
