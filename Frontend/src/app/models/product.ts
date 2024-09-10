export class Product {
    id: string;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    stock: number;

    constructor(id: string, name: string, description: string, price: number, imageUrl: string, stock: number) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.imageUrl = imageUrl;
        this.stock = stock;
    }
}
