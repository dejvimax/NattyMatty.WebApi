import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { ProductComponent } from './components/product/product.component';
import { ProductsComponent } from './components/products/products.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';

import { ProductService } from './product.service';

const appRoutes: Routes = [
  { path: 'app-products', component: ProductsComponent }  
];

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ProductsComponent,
    NavMenuComponent, 
    HomeComponent, 
    NavBarComponent    
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
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule
  ],

  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl },
    ProductService 
  ],  
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return "http://localhost:54895/";
  //return document.getElementsByTagName('base')[0].href;
}
