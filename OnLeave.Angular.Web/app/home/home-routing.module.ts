import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
//import { HeroDetailComponent } from './hero-detail.component';
const heroesRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    //{ path: 'hero/:id', component: HeroDetailComponent }
];
@NgModule({
    imports: [
        RouterModule.forChild(heroesRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class HomeRoutingModule { }