"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
//import { AppComponent } from './app.component';
//import { FooterComponent } from './shared/footer/footer.component';
//import { LoginComponent } from './shared/login/login.component'
var home_component_1 = require("./home.component");
var index_component_1 = require("./index.component");
var search_result_component_1 = require("app/home/search/search.result.component");
var footer_component_1 = require("app/shared/footer/footer.component");
var login_component_1 = require("app/shared/login/login.component");
var search_component_1 = require("app/shared/search/search.component");
var home_service_1 = require("app/services/home.service");
var home_routing_module_1 = require("./home-routing.module");
var HomeModule = (function () {
    function HomeModule() {
    }
    return HomeModule;
}());
HomeModule = __decorate([
    core_1.NgModule({
        imports: [common_1.CommonModule, home_routing_module_1.HomeRoutingModule],
        declarations: [index_component_1.IndexComponent, home_component_1.HomeComponent, search_result_component_1.SearchResultComponent, login_component_1.LoginComponent, search_component_1.SearchComponent, footer_component_1.FooterComponent],
        exports: [index_component_1.IndexComponent],
        providers: [home_service_1.HomeService],
    })
], HomeModule);
exports.HomeModule = HomeModule;
//# sourceMappingURL=home.module.js.map