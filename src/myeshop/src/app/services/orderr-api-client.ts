import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { OrderDto } from "../models/orderDto";

@Injectable()
export class OrderApiClient {
  constructor(private http: HttpClient) {}

  public getAllOrders() 
{
    let orders : OrderDto[] = [];
    this.http.get<OrderDto[]>('http://localhost:5008/order/api/order',{headers:{"accept": "text/plain"}}).subscribe(result => {
        orders = result;
      }, error => console.error(error));
      
      return orders;
}
}

