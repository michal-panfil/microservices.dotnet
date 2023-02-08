import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { Subject } from 'rxjs/internal/Subject';
import { ShipmentUpdate } from './shipmentUpdate';
@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public shipmentId: number = 0;
  public $shipmentUpdatedState: Subject<ShipmentUpdate> = new Subject<ShipmentUpdate>();
  public shipmentUpdate: ShipmentUpdate = { shipmentId: 0, remainingKm: 0, currentLocation: '' };
  private hubConnection: signalR.HubConnection;
    
    constructor() { 
        this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl('http://localhost:5008/warehouse/shipmentHub')
                              .build();
    }
  
  public startConnection = () => {
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
   
    public addShipmentStatusListener = () => {
      this.hubConnection.on('newshipmentlocation', (data) => {
        console.log("receiving websocket data");
        console.log(data);
        this.shipmentUpdate = data;
        this.$shipmentUpdatedState.next(this.shipmentUpdate);
      })
    }
}


