import { Component, OnInit } from '@angular/core'

import { HomeService } from 'app/services/home.service'
import { UtilityBuilding } from 'app/business.entities/utility.building'
//import { UtilityBuildingType } from 'app/business.entities/entity.contracts'
//import { UtilityBuildingFacilityType } from 'app/business.entities/entity.contracts'
//import { FacilityTypes } from 'app/business.entities/constants'

@Component({
    moduleId: module.id,
    selector: 'app-search-result',
    templateUrl: './search.component.html'
})
export class SearchComponent implements OnInit {
    buildings: UtilityBuilding[] = []

    //buildingTypes: UtilityBuildingType[] = []

    //facilityTypes: UtilityBuildingFacilityType[] = []

    //topFacilityTypes: UtilityBuildingFacilityType[] = []

    //additionalFacilityTypes: UtilityBuildingFacilityType[] = []

    //buildings: number[] = []

    constructor(private homeService: HomeService) { }

    ngOnInit(): void {
        //this.homeService
        //    .getCities()
        //    .then((data) => this.cities = data);

        //this.homeService
        //    .getUtilityBuildingTypes()
        //    .then((data) => this.buildingTypes = data);

        //this.homeService
        //    .getFacilityTypes()
        //    .then((data) => {
        //        this.facilityTypes = data;

        //        this.topFacilityTypes = data.filter((fType) =>
        //            [FacilityTypes.BREAKFASET, FacilityTypes.SPA, FacilityTypes.SWIMMING_POOL, FacilityTypes.WI_FI].indexOf(fType.UtilityBuildingFacilityTypeId) != -1);

        //        this.additionalFacilityTypes = data.filter((fType) =>
        //            [FacilityTypes.BREAKFASET, FacilityTypes.SPA, FacilityTypes.SWIMMING_POOL, FacilityTypes.WI_FI].indexOf(fType.UtilityBuildingFacilityTypeId) === -1);

        //    });
    }

    showSelection(obj: object, isChecked: boolean): void {
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