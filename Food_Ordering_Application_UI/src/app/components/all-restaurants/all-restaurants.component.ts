import { Component, OnInit } from '@angular/core';
import { Restaurant } from '../../models/Restaurant';
import { RestaurantService } from '../../services/restaurant.service';
import { Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { Menu } from '../../models/Menu';

@Component({
  selector: 'app-all-restaurants',
  templateUrl: './all-restaurants.component.html',
  styleUrls: ['./all-restaurants.component.css']
})
export class AllRestaurantsComponent implements OnInit {
  restaurants: Restaurant[] = [];
  filteredRestaurants: Restaurant[] = [];
  menuItems: Menu[] = [];
  selectedRestaurantId: string = "";
  errorMessage: string = '';
  searchTerm: string = '';

  constructor(private restaurantService: RestaurantService, private router: Router, private customerService: CustomerService) {}

  ngOnInit(): void {
    this.loadRestaurants();
  }

  loadRestaurants() {
    this.restaurantService.getAllRestaurants().subscribe({
      next: (res) => {
        // Filter for active restaurants only
        this.restaurants = res.filter(restaurant => restaurant.isActive);
        this.filteredRestaurants = this.restaurants; // Initialize filteredRestaurants with active restaurants
      },
      error: (err) => this.errorMessage = err.message
    });
  }

  searchRestaurants() {
    const term = this.searchTerm.toLowerCase();
    this.filteredRestaurants = this.restaurants.filter(
      restaurant => 
        restaurant.name.toLowerCase().includes(term) || 
        restaurant.location.toLowerCase().includes(term) || 
        restaurant.cuisine.toLowerCase().includes(term)
    );
  }

  addMenu(restaurantId: string) {
    this.router.navigate(['/add-menu', restaurantId]);
  }

  viewMenu(restaurantId: string) {
    this.selectedRestaurantId = restaurantId;
    this.customerService.getMenuByRestaurantId(restaurantId).subscribe({
      next: (res) => this.menuItems = res,
      error: (err) => this.errorMessage = "Error loading menu: " + err.message
    });
  }

  editMenu(menuId: string) {
    this.router.navigate(['/edit-menu', menuId]);
  }

  deleteMenu(menuId: string) {
    const confirmDelete = confirm("Are you sure you want to delete this menu item?");
    if (confirmDelete) {
      this.restaurantService.deleteMenuItem(menuId).subscribe({
        next: () => {
          this.menuItems = this.menuItems.filter(item => item.menuId !== menuId);
          alert('Menu item deleted successfully!');
          this.loadRestaurants(); // Reload restaurants to refresh menu items
        },
        error: (err) => alert("Error deleting menu item: " + err.message)
      });
    }
  }
}