import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { Product } from '../models/product';
import { OrderService } from '../services/order.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  products: Product[] | undefined;

  constructor(protected productService: ProductService, protected orderService: OrderService) {}

  async ngOnInit(): Promise<void> {
    await this.getProducts();
  }

  async getProducts(): Promise<void> {
    await this.productService.getAll()
      .then((products: Product[]) => {
        this.products = products;
      });
  }

  async orderProduct(product: Product): Promise<void> {
    if(confirm(`Weet je zeker dat je ${product.name} wil bestellen?`)) {
      await this.orderService.create(new Order(product?.id));
      alert(`Bestelling voor ${product.name} geplaatst!`);
      
      await this.getProducts();
    }
  }
}
