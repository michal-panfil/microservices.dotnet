import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-order-creator',
  templateUrl: './order-creator.component.html',
  styleUrls: ['./order-creator.component.scss']
})
export class OrderCreatorComponent implements OnInit {
  products: Product[] = [];
  public clientName: string = "";
  public clientAddress: string = "";
  public quantity: number = 0;
  public product: Product = {id: 0, name: ''};
    constructor(private http: HttpClient) {
      this.products.push({id: 1, name: 'Product 1'});
      this.products.push({id: 2, name: 'Product 2'});
      this.products.push({id: 3, name: 'Product 3'});
     }
    ngOnInit(): void {
    }
    public submitOrder() {
      const newOrder: NewOrder = {
        clientName: this.clientName,
        clientAddress: this.clientAddress,
        quantity: this.quantity,
        productId: this.product.id
      };
      this.http.post('http://localhost:5001/api/order', newOrder).subscribe();

    }
}

interface Product {
  id: number;
  name: string;
}

interface NewOrder{
  clientName: string;
  clientAddress: string;
  quantity: number;
  productId: number;
}