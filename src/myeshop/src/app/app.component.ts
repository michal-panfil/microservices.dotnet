import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
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
