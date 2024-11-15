import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Restaurant } from '../models/Restaurant';
import { Observable } from 'rxjs/internal/Observable';
import { Menu } from '../models/Menu';
import { Review } from '../models/Review';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  
  private apiUrl = 'https://localhost:7090/api/RestaurantOwner'; 
  private api='https://localhost:7090/api/Admin';
  constructor(private http: HttpClient) {}

  registerRestaurant(restaurantDto: {ownerId: string, name: string, location: string, cuisine: string}): Observable<Restaurant> {
    return this.http.post<Restaurant>(`${this.apiUrl}/registerRestaurant`, restaurantDto);
  }
  getAllRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(`${this.api}/getAllRestaurants`);
  }
  // addMenuItem(restaurantId: number, name: string, price: number): Observable<Menu> {
  //   return this.http.post<Menu>(`${this.apiUrl}/addMenuItem`, { restaurantId, name, price });
  // }
  addMenuItem(menuItemDto: { restaurantId: string; name: string; price: number }): Observable<Menu> {
    return this.http.post<Menu>(`${this.apiUrl}/addMenuItem`, menuItemDto);
  }
  
  // editMenuItem(menuId: number, name: string, price: number): Observable<Menu> {
  //   return this.http.put<Menu>(`${this.apiUrl}/editMenuItem/${menuId}`, { name, price });
  // }


  // Edit an existing menu item
  editMenuItem(menuId: string, menuItemDto: { name: string; price: number }): Observable<Menu> {
    return this.http.put<Menu>(`${this.apiUrl}/editMenuItem/${menuId}`, menuItemDto);
  }
  deleteMenuItem(menuId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteMenuItem/${menuId}`);
  }
 
  viewReviews(restaurantId: string): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/viewReviews/${restaurantId}`);
  }
  getMenuItemById(menuId: string): Observable<Menu> {
    return this.http.get<Menu>(`${this.apiUrl}/menuItem/${menuId}`);
  }
  // respondToReview(reviewId: number, response: string): Observable<void> {
  //   return this.http.post<void>(`${this.apiUrl}/respondToReview`, { reviewId, response });
  // }

  // Respond to a specific review
  respondToReview(reviewDto: { reviewId: string; response: string }): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/respondToReview`, reviewDto);
  }
}
