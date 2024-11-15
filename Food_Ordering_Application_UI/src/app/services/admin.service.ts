import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private api = 'https://localhost:7090/api/Admin'; 
  constructor(private http: HttpClient) {}

  // Manage restaurant status
  manageRestaurant(restaurantId: string, isActive: boolean): Observable<any> {
    const params = new HttpParams().set('restaurantId', restaurantId).set('isActive', isActive.toString());
    return this.http.post(`${this.api}/manageRestaurant`, null, { params });
  }
}
