import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order } from '../models/order';
import { APIClient } from './api-http.service';


@Injectable({
  providedIn: 'root'
})
export class OrderService extends APIClient {

  constructor(protected override http: HttpClient) {
    super(http);
    this.apiRoot = `${environment.apiBaseUrl}/orders/orders`;
  }

  public async create(order: Order): Promise<Order> {
    return this.post<Order>('', order);
  }
}
