import { Component, OnInit } from '@angular/core';

import { HomeService } from "app/services/home.service";
import {UtilityBuilding } from "app/business.entities/utility.building"

@Component({
    moduleId: module.id,
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit
{
    buildings: UtilityBuilding[] = []

    constructor(private homeService: HomeService) { }

    ngOnInit(): void {        
        console.log("Start");
        this.homeService.getTopOffers()
            .then((data) => {
                this.buildings = data;
                console.log("End of promise");
            });
        console.log("End");        
    }
}