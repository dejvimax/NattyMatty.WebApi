import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { Product } from './models/product.model'; 

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly _productsUrl: string = "api/product/";
  products: Product[];

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  getProducts() : Observable<Product[]> {
    console.log("Inside getProducts ProductService");
    var url = this.baseUrl + this._productsUrl;

    console.log("URL in product service:" + url);

    return this.http.get<Product[]>(url);

    // this.http.get<Product[]>(url).subscribe(result => {
    //   this.products = result;
    // }, error => console.error(error));

    //return this.products;
  }
}
