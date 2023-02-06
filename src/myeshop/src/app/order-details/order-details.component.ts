import { Component, OnInit } from '@angular/core';
import { SignalrService } from '../services/shipment-service';
import { ShipmentUpdate } from '../services/shipmentUpdate';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  public ordarDetails: ShipmentUpdate[] = [];


  constructor(private signalrService: SignalrService) {
  }
  ngOnInit(): void {
    this.signalrService.startConnection();
    this.signalrService.addShipmentStatusListener();
    this.signalrService.$shipmentUpdatedState.subscribe((data) => {
      var editedOrder = this.ordarDetails.find(order => {
        return order.shipmentId == data.shipmentId;
      });
      if (editedOrder) {
        editedOrder.currentLocation = data.currentLocation;
        editedOrder.remainingKm = data.remainingKm;
      } else {
        this.ordarDetails.push(data);
      }
    });
  }
}