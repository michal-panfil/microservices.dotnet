import { Component, Input, OnInit, DoCheck } from '@angular/core';
import { OrderDto } from '../models/orderDto';
import { OrderApiClient } from '../services/order-api-client';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit, DoCheck  {
  @Input() keyword:string | undefined;
  private keywordCopy:string | undefined;
  orders: OrderDto[] = [];

  constructor(private orderAPi: OrderApiClient) { }

  ngOnInit(): void {
    this.orderAPi.getAllOrders().subscribe( data => this.orders = data)
  }
  ngDoCheck(){
    if( this.keyword != undefined && this.keywordCopy != this.keyword){
      console.log("refreshing");
      this.keywordCopy = this.keyword;
      this.orderAPi.getAllOrders().subscribe( data => this.orders = data)
    }
  }
}
