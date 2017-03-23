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
var constants_1 = require("app/business.entities/constants");
var SearchComponent = (function () {
    function SearchComponent(homeService) {
        this.homeService = homeService;
        this.cities = [];
        this.buildingTypes = [];
        this.facilityTypes = [];
        this.topFacilityTypes = [];
        this.additionalFacilityTypes = [];
    }
    SearchComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.homeService
            .getCities()
            .then(function (data) { return _this.cities = data; });
        this.homeService
            .getUtilityBuildingTypes()
            .then(function (data) { return _this.buildingTypes = data; });
        this.homeService
            .getFacilityTypes()
            .then(function (data) {
            _this.facilityTypes = data;
            _this.topFacilityTypes = data.filter(function (fType) {
                return [constants_1.FacilityTypes.BREAKFASET, constants_1.FacilityTypes.SPA, constants_1.FacilityTypes.SWIMMING_POOL, constants_1.FacilityTypes.WI_FI].indexOf(fType.UtilityBuildingFacilityTypeId) != -1;
            });
            _this.additionalFacilityTypes = data.filter(function (fType) {
                return [constants_1.FacilityTypes.BREAKFASET, constants_1.FacilityTypes.SPA, constants_1.FacilityTypes.SWIMMING_POOL, constants_1.FacilityTypes.WI_FI].indexOf(fType.UtilityBuildingFacilityTypeId) === -1;
            });
        });
    };
    return SearchComponent;
}());
SearchComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'app-search',
        templateUrl: './search.component.html'
    }),
    __metadata("design:paramtypes", [home_service_1.HomeService])
], SearchComponent);
exports.SearchComponent = SearchComponent;
//# sourceMappingURL=search.component.js.map