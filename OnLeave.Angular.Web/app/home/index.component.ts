import { Component, OnInit } from '@angular/core';

@Component({    
  selector: 'my-app',  
  templateUrl: 'app/home/index.component.html'
})
export class IndexComponent implements OnInit  {
    name = 'Angular';

    ngOnInit(): void {
        console.log("Hello AppComponent");
    }
}
