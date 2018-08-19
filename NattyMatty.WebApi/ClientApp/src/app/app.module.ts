import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
// import { InjectionToken } from '@angular/core';
//export const BASE_URL = new InjectionToken<string>('BASE_URL');

import { AppComponent } from './app.component';
import { ProductComponent } from './components/product/product.component';
import { ProductsComponent } from './components/products/products.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule } from '@angular/material';

const appRoutes: Routes = [
  { path: 'app-products', component: ProductsComponent }  
];

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ProductsComponent,
    NavMenuComponent, 
    HomeComponent, NavBarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path: '', redirectTo: 'home', pathMatch:'full'},
      {path: 'home', component: HomeComponent},
      {path: 'product/:id', component: ProductComponent},
      {path: '**', redirectTo: 'home'}
    ]),
    //RouterModule.forRoot(appRoutes),
    // RouterModule.forRoot([
    //   {path: '', redirectTo: 'home', pathMatch:'full'},
    //   {path: 'home', component: HomeComponent},
    //   {path: '**', redirectTo: 'home'}
    // ]),
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule
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
