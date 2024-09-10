import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { Product } from '../models/product';
import { OrderService } from '../services/order.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  highlightedProduct: Product | undefined;

  constructor(protected productService: ProductService, protected orderService: OrderService) {}

  async ngOnInit(): Promise<void> {
    await this.getProducts();
  }

  async getProducts(): Promise<void> {
    await this.productService.getAll()
      .then((products: Product[]) => {
        this.highlightedProduct = products.find(p => p.name === 'Yamaha alt Yas 280');
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
