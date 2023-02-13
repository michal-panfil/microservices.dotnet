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
import { OrderApiClient } from './services/orderr-api-client';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { LoginComponent } from './login/login.component';
import { AuthConfigModule } from './auth/auth-config.module';


@NgModule({
  declarations: [
    AppComponent,
    OrderListComponent,
    OrderCreatorComponent,
    OrderDetailsComponent,
    LoginComponent
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
        authority: 'https://localhost:5001',
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: 'myshopui',
        scope: 'openid orderapi warehouseapi',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug,
      },
    }),
    AuthConfigModule,
  ],
  providers: [SignalrService, OrderApiClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
