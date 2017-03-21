import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { UtilityBuilding } from 'app/business.entities/utility.building'

@Injectable()
export class HomeService {

    private homeServiceUrl = 'api/home/offers';  // URL to web api

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    getTopOffers(): Promise<UtilityBuilding[]> {
        let result = this.http.get(this.homeServiceUrl)
            .toPromise().then(response => response.json() as UtilityBuilding[]);
        
        return result;
    }
}