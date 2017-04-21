import { Component, OnInit } from '@angular/core'

import { HomeService } from 'app/services/home.service'
import { City } from 'app/business.entities/city'
import { UtilityBuildingType } from 'app/business.entities/entity.contracts'
import { UtilityBuildingFacilityType } from 'app/business.entities/entity.contracts'
import { FacilityTypes } from 'app/business.entities/constants'

import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-search',   
    templateUrl: './search.component.html'
})
export class SearchComponent implements OnInit
{
    cities: City[] = []

    buildingTypes: UtilityBuildingType[] = []

    facilityTypes: UtilityBuildingFacilityType[] = []

    topFacilityTypes: UtilityBuildingFacilityType[] = []

    additionalFacilityTypes: UtilityBuildingFacilityType[] = []

    facilities: number[] = []

    name: string = ''

    topFacilities: number[] = []

    cityId: number

    utilityBuildingTypeId: number

    rating: number

    minAmount: number

    maxAmount: number    

    constructor(private homeService: HomeService, private router: Router) { }

    ngOnInit(): void
    {
        this.homeService
            .getCities()
            .then((data) => this.cities = data);

        this.homeService
            .getUtilityBuildingTypes()
            .then((data) => this.buildingTypes = data);

        this.homeService
            .getFacilityTypes()
            .then((data) =>
            {
                this.facilityTypes = data;

                this.topFacilityTypes = data.filter((fType) =>
                    [FacilityTypes.BREAKFASET, FacilityTypes.SPA, FacilityTypes.SWIMMING_POOL, FacilityTypes.WI_FI].indexOf(fType.UtilityBuildingFacilityTypeId) != -1);

                this.additionalFacilityTypes = data.filter((fType) =>
                    [FacilityTypes.BREAKFASET, FacilityTypes.SPA, FacilityTypes.SWIMMING_POOL, FacilityTypes.WI_FI].indexOf(fType.UtilityBuildingFacilityTypeId) === -1);

            });
    }

    topOffersChange(obj: UtilityBuildingFacilityType, isChecked: boolean): void
    {
        console.log(obj);
        console.log(isChecked);
        if (isChecked)
            this.topFacilities.push(obj.UtilityBuildingFacilityTypeId);
        else
            this.topFacilities.splice(this.topFacilities.indexOf(obj.UtilityBuildingFacilityTypeId), 1);
    }

    additionalOffersChange(obj: UtilityBuildingFacilityType, isChecked: boolean): void
    {
        console.log(obj);
        console.log(isChecked);
        if (isChecked)
            this.topFacilities.push(obj.UtilityBuildingFacilityTypeId);
        else
            this.topFacilities.splice(this.topFacilities.indexOf(obj.UtilityBuildingFacilityTypeId), 1);
    }

    onSearch(): void {        

        //this.router.navigate(['/search', { d: data }]);
        this.router.navigate(['/search',
            {
                id: 1,
                Name: this.name,
                CityId: this.cityId,
                UtilityBuildingTypeId: this.utilityBuildingTypeId,
                TopFacilities: this.topFacilities,
                Rating: this.rating,
                MinAmount: this.minAmount,
                MaxAmount: this.maxAmount
            }]);
        
    }    
}
