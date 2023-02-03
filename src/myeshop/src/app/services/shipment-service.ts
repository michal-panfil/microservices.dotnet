import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ShipmentUpdate } from './ShipmentUpdate';
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
                              .withUrl('https://localhost:5001/shipmentHub')
                              .build();
    }
  
  public startConnection = () => {
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
   
    public addShipmentStatusListener = () => {
      this.hubConnection.on('shipmentstatus', (data) => {
        this.shipmentUpdate = data;
        this.$shipmentUpdatedState.next(this.shipmentUpdate);
      })
    }
}


