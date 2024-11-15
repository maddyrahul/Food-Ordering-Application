import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { AllRestaurantsComponent } from './components/all-restaurants/all-restaurants.component';
import { AddMenuComponent } from './components/add-menu/add-menu.component';
import { EditMenuComponent } from './components/edit-menu/edit-menu.component';
import { ViewMenuComponent } from './components/view-menu/view-menu.component';
import { ListRestaurantComponent } from './components/list-restaurant/list-restaurant.component';
import { CartComponent } from './components/cart/cart.component';
import { OrdersComponent } from './components/orders/orders.component';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';
import { ResOwnerGuard } from './guards/res-owner.guard';
import { AdminRestaurantsComponent } from './components/admin-restaurants/admin-restaurants.component';

const routes: Routes = [
  { path: 'register-restaurant', component: RestaurantComponent,canActivate: [ResOwnerGuard] },
  { path: 'all-restaurants', component: AllRestaurantsComponent,canActivate: [ResOwnerGuard] },
  { path: 'orders', component: OrdersComponent ,canActivate: [AuthGuard]},
  { path: 'edit-menu/:menuId', component: EditMenuComponent ,canActivate: [ResOwnerGuard]},
  { path: 'edit-user/:userId',component:EditUserComponent,canActivate: [AdminGuard]},
  { path: 'add-menu/:restaurantId', component: AddMenuComponent ,canActivate: [ResOwnerGuard]},
  { path: 'view-menu/:restaurantId',component:ViewMenuComponent,canActivate: [AuthGuard]},
  { path: 'list-restaurant',component:ListRestaurantComponent,canActivate: [AuthGuard]},
  { path: 'login',component:LoginComponent},
  { path: 'admin-restaurants',component:AdminRestaurantsComponent,canActivate: [AdminGuard]},
  { path: 'users',component:UsersComponent,canActivate: [AdminGuard]},
  { path: 'cart',component:CartComponent,canActivate: [AuthGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
