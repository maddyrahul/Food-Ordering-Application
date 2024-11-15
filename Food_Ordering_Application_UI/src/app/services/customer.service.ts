import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Menu } from '../models/Menu';
import { Observable } from 'rxjs/internal/Observable';
import { Review } from '../models/Review';
import { Restaurant } from '../models/Restaurant';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) {}
  private apiUrl='https://localhost:7090/api/Customer';

  getMenuByRestaurantId(restaurantId: string): Observable<Menu[]> {
    return this.http.get<Menu[]>(`${this.apiUrl}/viewMenu/${restaurantId}`);
  }
  // Method to get order history by orderId
  getOrderHistory(orderId: string): Observable<any> {
    const url = `${this.apiUrl}/GetOrderHistory/${orderId}`;  // Adjust according to your backend API
    return this.http.get<any>(url);
  }
  // Browse restaurants
  browseRestaurants(search: string): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(`${this.apiUrl}/browseRestaurants?search=${search}`);
  }

  // View menu for a specific restaurant
  viewMenu(restaurantId: string): Observable<Menu[]> {
    return this.http.get<Menu[]>(`${this.apiUrl}/viewMenu/${restaurantId}`);
  }

  // Service
addToCart(cartDto: { customerId: string; menuId: string; quantity: number }): Observable<any> {
  return this.http.post<any>(`${this.apiUrl}/addToCart`, cartDto);
}


  // Place an order
  placeOrder(customerId: string): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/placeOrder`, { customerId });
  }

  // Track an order
  trackOrder(orderId: string): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/trackOrder/${orderId}`);
  }

  // Get order history for a customer
  orderHistory(customerId: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orderHistory/${customerId}`);
  }

  // Add a review to an order
  addReview(orderId: string, review: Review): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/${orderId}/AddReview`, review);
  }
}
