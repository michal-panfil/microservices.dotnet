import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { OrderDto } from "../models/orderDto";

@Injectable()
export class OrderApiClient {
  constructor(private http: HttpClient,
    private oidcSecurityService: OidcSecurityService,) {}

  public getAllOrders() 
{
  
        return this.http.get<OrderDto[]>('http://localhost:5008/order/api/order',
        {headers:{"accept": "text/plain",
        }});
}
}