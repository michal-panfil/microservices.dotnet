import { Component, Input, OnInit, DoCheck } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { OrderDto } from '../models/orderDto';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit, DoCheck  {
  @Input() keyword:string | undefined;
  private keywordCopy:string | undefined;
  orders: OrderDto[] = [];
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.callWebApiForOrders();
  }
  ngDoCheck(){
    if( this.keyword != undefined && this.keywordCopy != this.keyword){
      console.log("refreshing");
      this.keywordCopy = this.keyword;
      this.callWebApiForOrders();
    }
  }
 callWebApiForOrders() {
  console.log("lets call api");

    this.http.get<OrderDto[]>('http://localhost:5001/api/order',{headers:{"accept": "text/plain"}}).subscribe(result => {
      console.log(result);
      this.orders = result;
    }, error => console.error(error));
  }
}
