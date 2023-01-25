import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'myeshop';
  public keyword = "";
  public send(keyword: string){
    this.keyword = keyword;
  }
}
