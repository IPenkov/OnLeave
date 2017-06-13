"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
require("rxjs/add/operator/toPromise");
require("rxjs/add/observable/of");
require("rxjs/add/operator/do");
require("rxjs/add/operator/delay");
var AuthService = (function () {
    function AuthService(http) {
        this.http = http;
        this.isLoggedIn = false;
        this.headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        this.http;
    }
    AuthService.prototype.login = function () {
        //return Observable.of(true).delay(1000).do(val => this.isLoggedIn = true);
        var loginData = {
            grant_type: 'password',
            username: 'alice@example.com',
            password: 'ivan'
        };
        var data = "username=a@a.com&password=123456&grant_type=password";
        var headers = new http_1.Headers({ 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.post("http://localhost/OnLeave.Services/Token", data, options)
            .toPromise()
            .then(function (response) {
            sessionStorage.setItem('token', response.json().access_token);
            console.log("error " + response);
        })
            .catch(function (err) { console.log("error " + err); });
        //$.ajax({
        //    type: 'POST',
        //    url: '/Token',
        //    data: loginData
        //}).done(function (data) {
        //    self.user(data.userName);
        //    // Cache the access token in session storage.
        //    sessionStorage.setItem(tokenKey, data.access_token);
        //}).fail(showError);
    };
    AuthService.prototype.logout = function () {
        this.isLoggedIn = false;
    };
    return AuthService;
}());
AuthService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], AuthService);
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map