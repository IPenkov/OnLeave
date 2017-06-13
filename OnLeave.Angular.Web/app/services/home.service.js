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
var HomeService = (function () {
    function HomeService(http) {
        this.http = http;
        this.homeServiceUrl = 'api/home/'; // URL to web api
        this.headers = new http_1.Headers({ 'Content-Type': 'application/json' });
    }
    HomeService.prototype.getTopOffers = function () {
        //let result = this.http.get(this.homeServiceUrl + "offers")
        //    .toPromise().then(response => response.json() as UtilityBuilding[]);
        var headers = new http_1.Headers({
            'Authorization': "Bearer " + sessionStorage.getItem('token')
        });
        var options = new http_1.RequestOptions({ headers: headers });
        var result = this.http.get("http://localhost/OnLeave.Services/api/home/offers", options)
            .toPromise().then(function (response) { return response.json(); }).catch(function (err) { return console.log(err); });
        return result;
    };
    HomeService.prototype.getCities = function () {
        return this.http.get(this.homeServiceUrl + 'cities')
            .toPromise().then(function (response) { return response.json(); });
    };
    HomeService.prototype.getUtilityBuildingTypes = function () {
        return this.http.get(this.homeServiceUrl + 'buildingtypes')
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    HomeService.prototype.getFacilityTypes = function () {
        return this.http.get(this.homeServiceUrl + 'facilitytypes')
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    HomeService.prototype.search = function (search) {
        return this.http.post(this.homeServiceUrl + 'search', search)
            .toPromise()
            .then(function (response) { return response.json(); });
    };
    return HomeService;
}());
HomeService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], HomeService);
exports.HomeService = HomeService;
//# sourceMappingURL=home.service.js.map