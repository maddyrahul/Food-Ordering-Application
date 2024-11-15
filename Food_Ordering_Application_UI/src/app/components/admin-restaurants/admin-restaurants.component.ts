import { Component, OnInit } from '@angular/core';
import { Restaurant } from '../../models/Restaurant';
import { AdminService } from '../../services/admin.service';
import { RestaurantService } from '../../services/restaurant.service';

@Component({
  selector: 'app-admin-restaurants',
  templateUrl: './admin-restaurants.component.html',
  styleUrl: './admin-restaurants.component.css'
})
export class AdminRestaurantsComponent implements OnInit {
  restaurants: Restaurant[] = [];
  errorMessage: string = '';

  constructor(private adminService: AdminService, private restaurantService:RestaurantService) {}

  ngOnInit(): void {
    this.loadRestaurants();
  }

  // Load all restaurants
  loadRestaurants(): void {
    this.restaurantService.getAllRestaurants().subscribe({
      next: (data) => {
        this.restaurants = data;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load restaurants.';
      }
    });
  }

  // Toggle restaurant active status
  toggleRestaurantStatus(restaurant: Restaurant): void {
    const newStatus = !restaurant.isActive;
    this.adminService.manageRestaurant(restaurant.restaurantId, newStatus).subscribe({
      next: () => {
        restaurant.isActive = newStatus; // Update status in the UI
      },
      error: (err) => {
        this.errorMessage = 'Failed to update restaurant status.';
      }
    });
  }
}
