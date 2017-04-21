import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index.component'
import { HomeComponent } from './home.component';
import { SearchResultComponent } from 'app/home/search/search.result.component'


//import { HeroDetailComponent } from './hero-detail.component';
const heroesRoutes: Routes = [
    //{ path: '', component: IndexComponent },
    {
        path: '', component: IndexComponent,
        children: [
            { path: '', component: HomeComponent },
            { path: 'search', component: SearchResultComponent }
        ]
    },        
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