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
var home_service_1 = require("app/services/home.service");
//import { UtilityBuildingType } from 'app/business.entities/entity.contracts'
//import { UtilityBuildingFacilityType } from 'app/business.entities/entity.contracts'
//import { FacilityTypes } from 'app/business.entities/constants'
var SearchComponent = (function () {
    //buildingTypes: UtilityBuildingType[] = []
    //facilityTypes: UtilityBuildingFacilityType[] = []
    //topFacilityTypes: UtilityBuildingFacilityType[] = []
    //additionalFacilityTypes: UtilityBuildingFacilityType[] = []
    //buildings: number[] = []
    function SearchComponent(homeService) {
        this.homeService = homeService;
        this.buildings = [];
    }
    SearchComponent.prototype.ngOnInit = function () {
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
    };
    SearchComponent.prototype.showSelection = function (obj, isChecked) {
        console.log(obj);
        console.log(isChecked);
    };
    SearchComponent.prototype.getPageCount = function () {
        var pageNumber = [];
        pageNumber.length = Math.ceil(this.buildings.length / 10.0);
        return pageNumber;
    };
    return SearchComponent;
}());
SearchComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'app-search-result',
        templateUrl: './search.component.html'
    }),
    __metadata("design:paramtypes", [home_service_1.HomeService])
], SearchComponent);
exports.SearchComponent = SearchComponent;
//# sourceMappingURL=search.component.js.map