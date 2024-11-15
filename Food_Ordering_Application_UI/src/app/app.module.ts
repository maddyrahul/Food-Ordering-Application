import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ReviewComponent } from './components/review/review.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AddMenuComponent } from './components/add-menu/add-menu.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { AllRestaurantsComponent } from './components/all-restaurants/all-restaurants.component';
import { EditMenuComponent } from './components/edit-menu/edit-menu.component';
import { ListRestaurantComponent } from './components/list-restaurant/list-restaurant.component';
import { ViewMenuComponent } from './components/view-menu/view-menu.component';
import { CartComponent } from './components/cart/cart.component';
import { OrdersComponent } from './components/orders/orders.component';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';
import { JwtModule } from '@auth0/angular-jwt';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AdminRestaurantsComponent } from './components/admin-restaurants/admin-restaurants.component';

 

@NgModule({
  declarations: [
    AppComponent,
    RestaurantComponent,
    ReviewComponent,
    AddMenuComponent,
    HeaderComponent,
    FooterComponent,
    AllRestaurantsComponent,
    EditMenuComponent,
    ListRestaurantComponent,
    ViewMenuComponent,
    CartComponent,
    OrdersComponent,
    LoginComponent,
    UsersComponent,
    EditUserComponent,
    AdminRestaurantsComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('token');
        },
        allowedDomains: ['localhost:7090'], 
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    provideAnimationsAsync(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
