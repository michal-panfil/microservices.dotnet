import { Component, OnInit } from '@angular/core';
import { OrderDto } from '../models/orderDto';
import { OrderApiClient } from '../services/order-api-client';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap, tap } from 'rxjs/operators';
import { ShopService } from '../services/shop.service';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss'],
  standalone: false
})
export class OrderListComponent implements OnInit {
  orders: OrderDto[] = [{ id: 1, clientName: "xxxx", clientAddress: "ddddd", quantity: 5, productId: 101, productName: "dsacvsd", status: "New" }];

  constructor(private orderAPi: OrderApiClient, private shopService: ShopService) { }

  ngOnInit(): void {
    this.orderAPi.getAllOrders().pipe(takeUntilDestroyed(), tap((data) => this.orders = data)).subscribe();

    this.shopService.createdOrderIds.pipe(takeUntilDestroyed(), switchMap(x =>
      this.orderAPi.getAllOrders().pipe(takeUntilDestroyed(), tap((data) => this.orders = data)))).subscribe()
  }
}
