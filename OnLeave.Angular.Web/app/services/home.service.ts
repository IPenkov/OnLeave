import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { UtilityBuilding } from 'app/business.entities/utility.building'
import { City } from 'app/business.entities/city'
import { UtilityBuildingType } from 'app/business.entities/entity.contracts'
import { UtilityBuildingFacilityType } from 'app/business.entities/entity.contracts'

@Injectable()
export class HomeService {

    private homeServiceUrl = 'api/home/';  // URL to web api

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    getTopOffers(): Promise<UtilityBuilding[]> {
        //let result = this.http.get(this.homeServiceUrl + "offers")
        //    .toPromise().then(response => response.json() as UtilityBuilding[]);

        let headers = new Headers({
            'Authorization': `Bearer ${sessionStorage.getItem('token')}` });
        let options = new RequestOptions({ headers: headers });

        let result = this.http.get("http://localhost/OnLeave.Services/api/home/offers", options)
            .toPromise().then(response => response.json() as UtilityBuilding[]).catch(err => console.log(err));
        
        return result;
    }

    getCities(): Promise<City[]>
    {
        return this.http.get(this.homeServiceUrl + 'cities')
            .toPromise().then(response => response.json() as City[]);
    }

    getUtilityBuildingTypes(): Promise<UtilityBuildingType[]>
    {
        return this.http.get(this.homeServiceUrl + 'buildingtypes')
            .toPromise()
            .then(response => response.json() as UtilityBuildingType[]);
    }

    getFacilityTypes(): Promise<UtilityBuildingFacilityType[]>
    {
        return this.http.get(this.homeServiceUrl + 'facilitytypes')
            .toPromise()
            .then(response => response.json() as UtilityBuildingFacilityType[]);
    }

    search(search: any): Promise<UtilityBuilding[]>
    {
        return this.http.post(this.homeServiceUrl + 'search', search)
            .toPromise()
            .then(response => response.json() as UtilityBuilding[]);
    }
}