import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.callWebApiForOrders();
  }
 callWebApiForOrders() {
  console.log("lets call api");

    this.http.get<Order[]>('http://localhost:5001/api/order').subscribe(result => {
      console.log(result);
      this.orders = result;
    }, error => console.error(error));
  }
}
interface Order {
  id: number;
  customer : Customer;
  items: OrderItem[];

}

interface Customer {
  id: number;
  name: string;
  email: string;
}
interface OrderItem {
  id: number;
  quantity: number;
  product: Product;
}

interface Product {
  id: number;
  name: string;
  price: number;
}