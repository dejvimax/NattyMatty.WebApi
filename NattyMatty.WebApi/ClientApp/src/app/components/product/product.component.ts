import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

import { ProductService } from '../../product.service';
import { Product } from '../../models/product.model'; 

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent {
    product: Product;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private productService: ProductService) {

        // create an empty object from the Product interface
        this.product = <Product>{};

        var id = +this.activatedRoute.snapshot.params["id"];
        console.log(id);
        if (id) {

            console.log("Calling Product Service");
            this.productService.getProduct(id)
                .subscribe(product => {this.product = product;},
                error => console.error(error));
            
            console.log("After Calling Product Service");            
        }
        else {
            console.log("Invalid id: routing back to product...");
            this.router.navigate(["product"]);
        }
    }
}

  /*
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
	
  constructor() { }

  ngOnInit() {
  }

}*/
