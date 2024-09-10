import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product';
import { APIClient } from './api-http.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends APIClient {

  constructor(protected override http: HttpClient) {
    super(http);
    this.apiRoot = `${environment.apiBaseUrl}/products/products`;
  }

  public async getAll(): Promise<Product[]> {
    return this.get<Product[]>();
  }
}
