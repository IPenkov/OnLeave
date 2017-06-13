import { NgModule } from '@angular/core';
//import { CommonModule } from '@angular/common';
//import { FormsModule } from '@angular/forms';
import { FooterComponent } from 'app/shared/footer/footer.component';
import { LoginComponent } from 'app/shared/login/login.component'
@NgModule({
    imports: [],
    declarations: [LoginComponent, FooterComponent],
    exports: [LoginComponent, FooterComponent]
})
export class SharedModule { }