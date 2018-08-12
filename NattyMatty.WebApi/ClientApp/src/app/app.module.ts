import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { RouterModule } from '@angular/router';
// import { InjectionToken } from '@angular/core';
//export const BASE_URL = new InjectionToken<string>('BASE_URL');

import { AppComponent } from './app.component';
import { ProductComponent } from './components/product/product.component';
import { ProductListComponent } from './components/product-list/product-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ProductListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule
  ],

  providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl }],
  //providers: [  { provide: BASE_URL , useValue: "http://localhost:/api" }],
  //providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return "http://localhost:54895/";
  //return document.getElementsByTagName('base')[0].href;
}
