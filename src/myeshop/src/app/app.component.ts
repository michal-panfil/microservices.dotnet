import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    standalone: false
})
export class AppComponent {
  title = 'myeshop';
  public keyword = "";

  constructor(
    ) { }

  ngOnInit() {
  }


  public send(keyword: string){
    this.keyword = keyword;
  }
}
