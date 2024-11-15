import { Component, OnInit } from '@angular/core';
import { Restaurant } from '../../models/Restaurant';
import { Menu } from '../../models/Menu';
import { RestaurantService } from '../../services/restaurant.service';
import { Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-list-restaurant',
  templateUrl: './list-restaurant.component.html',
  styleUrl: './list-restaurant.component.css'
})
export class ListRestaurantComponent implements OnInit {
  restaurants: Restaurant[] = [];
  filteredRestaurants: Restaurant[] = [];
  menuItems: Menu[] = [];
  searchTerm: string = '';
  selectedRestaurantId: string | null = null;
  errorMessage: string = '';

  constructor(private restaurantService: RestaurantService, private router: Router, private customerService:CustomerService) {}

//   ngOnInit(): void {
//     this.loadRestaurants();
//   }

//   loadRestaurants() {
//     this.restaurantService.getAllRestaurants().subscribe({
//       next: (res) => this.restaurants = res,
//       error: (err) => this.errorMessage = err.message
//     });
//   }
// // Search restaurants based on the search term
// searchRestaurants() {
//   if (this.searchTerm.trim()) {  // Only search if search term is not empty
//     this.customerService.browseRestaurants(this.searchTerm).subscribe({
//       next: (res) => this.restaurants = res,
//       error: (err) => this.errorMessage = err.message
//     });
//   } else {
//     this.loadRestaurants();  // Load all restaurants if search term is empty
//   }
// }
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
 
  viewMenu(restaurantId: string) {
    this.router.navigate(['/view-menu', restaurantId]);
  }


   // Edit Menu Item
   editMenu(menuId: string) {
    
    this.router.navigate(['/edit-menu', menuId]);
    }
  

  // Delete Menu Item
  deleteMenu(menuId: string) {
    const confirmDelete = confirm("Are you sure you want to delete this menu item?");
    if (confirmDelete) {
      this.restaurantService.deleteMenuItem(menuId).subscribe({
        next: () => {
          alert('Menu item deleted successfully!');
          this.loadRestaurants(); // Call this after successful deletion
        },
        error: (err) => {
          alert("Error deleting menu item: " + err.message);
        }
      });
    }
  }
}