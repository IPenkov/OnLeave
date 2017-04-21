"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms"); // <--- JavaScript import from Angular
var app_routing_module_1 = require("./app-routing.module");
//import { IndexComponent } from 'app/home/index.component';
var app_component_1 = require("./app.component");
//import { HomeComponent } from 'app/home/home.component'
//import { FooterComponent } from './shared/footer/footer.component';
//import { LoginComponent } from './shared/login/login.component'
//import { SearchComponent } from 'app/shared/search/search.component'
//import { HomeComponent } from './home/home.component'
//import { HomeService } from '../app/services/home.service'
//const appRoutes: Routes = [
//    { path: 'home', component: IndexComponent },    
//    { path: '', redirectTo: '/home', pathMatch: 'full' }    
//];
/* Contact Imports */
var home_module_1 = require("./home/home.module");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, http_1.HttpModule, home_module_1.HomeModule, forms_1.FormsModule, app_routing_module_1.AppRoutingModule],
        declarations: [app_component_1.AppComponent],
        //providers: [HomeService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map