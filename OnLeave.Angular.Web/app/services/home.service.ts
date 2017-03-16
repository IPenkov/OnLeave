import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class HomeService {

    private homeServiceUrl = 'api/home/offers';  // URL to web api

    constructor(private http: Http) { }

    getTopOffers(): Promise<object[]> {
        let result = this.http.get(this.homeServiceUrl)
            .toPromise().then(response => response.json() as object[]);
        
        return result;
    }
}