"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var index_component_1 = require("./index.component");
var home_component_1 = require("./home.component");
var search_result_component_1 = require("app/home/search/search.result.component");
//import { HeroDetailComponent } from './hero-detail.component';
var heroesRoutes = [
    //{ path: '', component: IndexComponent },
    {
        path: '', component: index_component_1.IndexComponent,
        children: [
            { path: '', component: home_component_1.HomeComponent },
            { path: 'search', component: search_result_component_1.SearchResultComponent }
        ]
    },
];
var HomeRoutingModule = (function () {
    function HomeRoutingModule() {
    }
    return HomeRoutingModule;
}());
HomeRoutingModule = __decorate([
    core_1.NgModule({
        imports: [
            router_1.RouterModule.forChild(heroesRoutes)
        ],
        exports: [
            router_1.RouterModule
        ]
    })
], HomeRoutingModule);
exports.HomeRoutingModule = HomeRoutingModule;
//# sourceMappingURL=home-routing.module.js.map