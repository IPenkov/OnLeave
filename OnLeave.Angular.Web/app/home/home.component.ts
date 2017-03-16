import { Component, OnInit } from '@angular/core';

import { HomeService } from "../services/home.service";

@Component({
    moduleId: module.id,
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit
{
    //buildings: UtilityBuilding[] = [new UtilityBuilding(-1, "Angular")]

    constructor(private homeService: HomeService) { }

    ngOnInit(): void {
        //let offers = this.homeService.getTopOffers();
        console.log("Start");
        this.homeService.getTopOffers().then((data) => console.log(data));        
        console.log("End");        
    }
}