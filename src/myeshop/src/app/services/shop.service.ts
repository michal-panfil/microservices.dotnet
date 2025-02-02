import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable()
export class ShopService {

    public createdOrderIds: Observable<number>;
    private _createdOrdersIds = new BehaviorSubject<number>(1);

    constructor() {
        this.createdOrderIds = this._createdOrdersIds.asObservable();
    }

    setNewOrderId(id: number) {
        this._createdOrdersIds.next(id);
    }

}
