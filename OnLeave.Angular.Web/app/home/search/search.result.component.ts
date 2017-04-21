import { Component, OnInit } from '@angular/core'
import { Router, ActivatedRoute, Params } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { HomeService } from 'app/services/home.service'
import { UtilityBuilding } from 'app/business.entities/utility.building'
//import { UtilityBuildingType } from 'app/business.entities/entity.contracts'
//import { UtilityBuildingFacilityType } from 'app/business.entities/entity.contracts'
//import { FacilityTypes } from 'app/business.entities/constants'

@Component({
    moduleId: module.id,
    selector: 'app-search-result',
    templateUrl: './search.result.component.html'
})
export class SearchResultComponent implements OnInit {
    buildings: UtilityBuilding[] = []

    //buildingTypes: UtilityBuildingType[] = []

    //facilityTypes: UtilityBuildingFacilityType[] = []

    //topFacilityTypes: UtilityBuildingFacilityType[] = []

    //additionalFacilityTypes: UtilityBuildingFacilityType[] = []

    //buildings: number[] = []

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private homeService: HomeService)
    {        
    }

    ngOnInit(): void {
        

        this.route.params
            // (+) converts string 'id' to a number
            .switchMap((params: Params) =>
                this.homeService.search(params))
            .subscribe(data => {
                this.buildings = data;
                console.log(data);
            });

        //let data = await this.homeService.search(this.route.params['value']);

        //this.buildings = data;
        //console.log(data);
    }

    showSelection(obj: any, isChecked: boolean): void {
        console.log(obj);
        console.log(isChecked);
    }

    getPageCount(): number[]
    {
        let pageNumber: number[] = [];
        pageNumber.length = Math.ceil(this.buildings.length / 10.0);
        return pageNumber;        
        
    }
}