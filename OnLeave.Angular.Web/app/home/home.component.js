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
var HomeComponent = (function () {
    //buildings: UtilityBuilding[] = [new UtilityBuilding(-1, "Angular")]
    function HomeComponent(homeService) {
        this.homeService = homeService;
    }
    HomeComponent.prototype.ngOnInit = function () {
        //let offers = this.homeService.getTopOffers();
        console.log("Start");
        this.homeService.getTopOffers().then(function (data) { return console.log(data); });
        console.log("End");
    };
    return HomeComponent;
}());
HomeComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'app-home',
        templateUrl: './home.component.html'
    }),
    __metadata("design:paramtypes", [home_service_1.HomeService])
], HomeComponent);
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map