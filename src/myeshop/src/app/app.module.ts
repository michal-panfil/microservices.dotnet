import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';

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
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';


@NgModule({ declarations: [
        AppComponent,
        OrderListComponent,
        OrderCreatorComponent,
        OrderDetailsComponent,
    ],
    imports: 
    [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        FormsModule,
        CardModule,
        TableModule,  
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
        })
    ], 
    providers: [SignalrService, OrderApiClient, provideHttpClient(withInterceptorsFromDi()),
        provideAnimationsAsync(),
        providePrimeNG({
            theme: {
                preset: Aura
            }
        })
    ],
    bootstrap: [AppComponent], 
 })
export class AppModule { }

