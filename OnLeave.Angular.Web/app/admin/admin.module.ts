import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { LeftComponent } from './left/left.component'
import { LoginComponent } from './login/login.component'

import { AuthGuard } from './auth-guard.service'
import { AuthService } from 'app/services/auth.service'

import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from 'app/shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        AdminRoutingModule,
        SharedModule        
    ],
    declarations: [
        AdminComponent,
        LeftComponent,
        LoginComponent        
    ],
    providers: [AuthGuard, AuthService]
})
export class AdminModule { }