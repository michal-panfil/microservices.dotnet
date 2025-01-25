import { Component, Input, OnInit, DoCheck } from '@angular/core';
import { OrderDto } from '../models/orderDto';
import { OrderApiClient } from '../services/order-api-client';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { tap } from 'rxjs/operators';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss'],
  standalone: false
})
export class OrderListComponent implements OnInit, DoCheck {
  @Input() keyword: string | undefined;
  private keywordCopy: string | undefined;
  orders: OrderDto[] = [{ id: 1, clientName: "xxxx", clientAddress: "ddddd", quantity: 5, productId: 101, productName: "dsacvsd", status: "New" }];

  constructor(private orderAPi: OrderApiClient) { }

  ngOnInit(): void {
    this.orderAPi.getAllOrders().pipe(takeUntilDestroyed(), tap((data) => this.orders = data)).subscribe();
  }
  ngDoCheck() {
    if (this.keyword != undefined && this.keywordCopy != this.keyword) {
      this.keywordCopy = this.keyword;
      this.orderAPi.getAllOrders().pipe(takeUntilDestroyed(), tap((data) => this.orders = data)).subscribe();
    }
  }
}
