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
var admin_component_1 = require("./admin.component");
var left_component_1 = require("./left/left.component");
var login_component_1 = require("./login/login.component");
var auth_guard_service_1 = require("./auth-guard.service");
var auth_service_1 = require("app/services/auth.service");
var admin_routing_module_1 = require("./admin-routing.module");
var shared_module_1 = require("app/shared/shared.module");
var AdminModule = (function () {
    function AdminModule() {
    }
    return AdminModule;
}());
AdminModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            admin_routing_module_1.AdminRoutingModule,
            shared_module_1.SharedModule
        ],
        declarations: [
            admin_component_1.AdminComponent,
            left_component_1.LeftComponent,
            login_component_1.LoginComponent
        ],
        providers: [auth_guard_service_1.AuthGuard, auth_service_1.AuthService]
    })
], AdminModule);
exports.AdminModule = AdminModule;
//# sourceMappingURL=admin.module.js.map