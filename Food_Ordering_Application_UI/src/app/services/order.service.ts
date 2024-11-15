import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Order } from '../models/Order';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = 'https://localhost:7090/api/Customer';

  constructor(private http: HttpClient) {}

  // Place a new order
  placeOrder(customerId: string): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/placeOrder?customerId=${customerId}`, {});
  }

  // Track an order
  trackOrder(orderId: string): Observable<string> {
    return this.http.get<string>(`${this.apiUrl}/trackOrder/${orderId}`);
  }

  // Get order history for a customer
  getOrderHistory(customerId: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orderHistory/${customerId}`);
  }
}
