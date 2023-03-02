import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrderListComponent } from './order-list/order-list.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrderCreatorComponent } from './order-creator/order-creator.component';
import { FormsModule } from '@angular/forms'; 
import { ReactiveFormsModule } from '@angular/forms';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { SignalrService } from './services/shipment-service';
import { OrderApiClient } from './services/order-api-client';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';


@NgModule({
  declarations: [
    AppComponent,
    OrderListComponent,
    OrderCreatorComponent,
    OrderDetailsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    AuthModule.forRoot({
      config: {
        clientId: 'myshopui',
        authority: 'http://localhost:5009',
        responseType: 'code',
        redirectUrl: "http://localhost:5002",
        postLogoutRedirectUri: "http://localhost:5002",
        scope: 'openid OrderApi WarehouseApi',
        logLevel: LogLevel.Debug,
      },
    }),
  ],
  providers: [SignalrService, OrderApiClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
