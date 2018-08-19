import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

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
        @Inject('BASE_URL') private baseUrl: string) {

        // create an empty object from the Product interface
        this.product = <Product>{};

        var id = +this.activatedRoute.snapshot.params["id"];
        console.log(id);
        if (id) {
            var url = this.baseUrl + "api/product/" + id;
            //var url = "http://localhost:54895/product/" + id;

            this.http.get<Product>(url).subscribe(result => {
                this.product = result;
            }, error => console.error(error));
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
