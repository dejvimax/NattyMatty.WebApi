import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    @Input() class: string;
	title: string;
	selectedProduct: Product;
    products: Product[];
    
    // constructor(private http: HttpClient,
    //     @Inject('BASE_URL') private baseUrl: string,
    //     private router: Router) {
    // }

//   constructor(http: HttpClient,
//         @Inject('BASE_URL') baseUrl: string) {
//         this.title = 'Latest products';
// 		var url = baseUrl + 'api/product/';
//         // this.baseUrl = baseUrl;
		
// 		http.get<Product[]>(url).subscribe(result => {
// 			this.products = result;
// 		}, error => console.error(error));
//     }

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
            private router: Router) {
    }
    
    ngOnInit() {
        console.log("ProductsComponent " +
            " instantiated with the following class: "
            + this.class);
            
            this.title = 'Latest products';
            
            //var url = "http://localhost:54895/product/";

            var url = this.baseUrl + "api/product/";

            console.log("URL:" + url);

            this.http.get<Product[]>(url).subscribe(result => {
                this.products = result;
            }, error => console.error(error));
    }
	
	onSelect(product: Product) {
        this.selectedProduct = product;
        console.log("product with Id "
            + this.selectedProduct.Id
            + " has been selected.");
        this.router.navigate(["product", this.selectedProduct.Id]);
    }
}
