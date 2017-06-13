import { Injectable } from '@angular/core';
import { Headers, RequestOptions, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';

@Injectable()
export class AuthService {
    isLoggedIn: boolean = false;

    // store the URL so we can redirect after logging in
    redirectUrl: string;

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http)
    {
        this.http
    }

    login(): Promise<void> {
        //return Observable.of(true).delay(1000).do(val => this.isLoggedIn = true);

        let loginData = {
            grant_type: 'password',
            username: 'alice@example.com',
            password: 'ivan'
        };

        let data = `username=a@a.com&password=123456&grant_type=password`;

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post("http://localhost/OnLeave.Services/Token", data, options)
            .toPromise()
            .then(response => {
                sessionStorage.setItem('token', response.json().access_token);
                console.log(`error ${response}`);
            })
            .catch(err => { console.log(`error ${err}`); })
       
            

        //$.ajax({
        //    type: 'POST',
        //    url: '/Token',
        //    data: loginData
        //}).done(function (data) {
        //    self.user(data.userName);
        //    // Cache the access token in session storage.
        //    sessionStorage.setItem(tokenKey, data.access_token);
        //}).fail(showError);
    }

    logout(): void {
        this.isLoggedIn = false;
    }
}