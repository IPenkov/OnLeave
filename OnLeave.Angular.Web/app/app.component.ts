import { Component, OnInit } from '@angular/core';

@Component({    
  selector: 'my-app',  
    //templateUrl: 'app/app.component.html'
  template: '<router-outlet></router-outlet>'
})
export class AppComponent implements OnInit  {
    name = 'Angular';

    ngOnInit(): void {
        console.log("Hello AppComponent");
    }
}
