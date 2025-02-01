import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ShopService } from './services/shop.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: false,
  providers: [ShopService]
})
export class AppComponent {
  title = 'myeshop';

  constructor(
  ) { }

  ngOnInit() {
  }
}
