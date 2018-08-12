import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {
	title: string;
	selectedProduct: Product;
	products: Product[];

  constructor(http: HttpClient,
        @Inject('BASE_URL') baseUrl: string) {
        this.title = 'Latest products';
		var url = baseUrl + 'api/product/';
        // this.baseUrl = baseUrl;
		
		http.get<Product[]>(url).subscribe(result => {
			this.products = result;
		}, error => console.error(error));
    }	
	
	onSelect(product: Product) {
        this.selectedProduct = product;
        console.log("product with Id "
            + this.selectedProduct.Id
            + " has been selected.");
    }

  

}
