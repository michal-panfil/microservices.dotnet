import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductDto } from '../models/productDto';

@Component({
    selector: 'app-order-creator',
    templateUrl: './order-creator.component.html',
    styleUrls: ['./order-creator.component.scss'],
    standalone: false
})
export class OrderCreatorComponent implements OnInit {
  @Output() emitter:EventEmitter<string>
  = new EventEmitter<string>();
  products: Product[] = [];
  public clientName: string = "";
  public clientAddress: string = "";
  public quantity: number = 0;
  public product: Product = { id: 0, name: '' };
  constructor(private http: HttpClient) {
  }
  ngOnInit(): void {
    this.callWebApiForProducts();
  }
  public submitOrder() {
    const newOrder: NewOrder = {
      clientName: this.clientName,
      clientAddress: this.clientAddress,
      quantity: this.quantity,
      productId: this.product.id
    };
    this.http.post('http://localhost:5008/order/api/order', newOrder)
    .subscribe(
      (val) => {
        console.log("POST call successful value returned in body", val);
        this.emitter.emit("Order created:" + Date.now());
      }
    );
    this.clearForm();
  }

  callWebApiForProducts() {
    console.log("lets call api");

    this.http.get<ProductDto[]>('http://localhost:5008/order/api/products', { headers: { "accept": "text/plain" } }).subscribe(result => {
      console.log(result);
      this.products = result;
    }, error => console.error(error));
  }

  clearForm() {
    this.clientName = "";
    this.clientAddress = "";
    this.quantity = 0;
    this.product = { id: 0, name: '' };
  }
}

interface Product {
  id: number;
  name: string;
}

interface NewOrder {
  clientName: string;
  clientAddress: string;
  quantity: number;
  productId: number;
}