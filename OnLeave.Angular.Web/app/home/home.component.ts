import { Component } from '@angular/core';

import {UtilityBuilding } from './UtilityBuilding'

@Component({
    moduleId: module.id,
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent
{
    buildings: UtilityBuilding[] = [new UtilityBuilding(-1, "Angular")]
}