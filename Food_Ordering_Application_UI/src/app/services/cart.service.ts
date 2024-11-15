import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { CartItem } from '../models/CartItem';
import { CartResponse } from '../models/CartResponse';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private apiUrl = 'https://localhost:7090/api/Cart';

  constructor(private http: HttpClient) { }


  getCartItems(customerId: string): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.apiUrl}/getCartItems/${customerId}`);
  }

  updateCartItem(updateDto: { cartId: string; quantity: number }): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/updateCartItem`, updateDto);
  }

  removeCartItem(cartId: string): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/removeCartItem/${cartId}`);
  }
}
