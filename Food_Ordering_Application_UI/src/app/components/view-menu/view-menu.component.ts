import { Component } from '@angular/core';
import { Menu } from '../../models/Menu';
import { RestaurantService } from '../../services/restaurant.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-view-menu',
  templateUrl: './view-menu.component.html',
  styleUrl: './view-menu.component.css'
})
export class ViewMenuComponent {
  errorMessage: string = '';
  menuItems: Menu[] = [];
  restaurantId: string;
  customerId: string = ''; // Assuming a sample customer ID

  selectedRestaurantId: string | null = null;
  constructor(private restaurantService: RestaurantService,  private router: Router,
    private route: ActivatedRoute,
     private customerService:CustomerService) {
      this.customerId = localStorage.getItem('userId') || '';
      console.log("user id ",this.customerId)
      this.restaurantId = this.route.snapshot.paramMap.get('restaurantId')!;
       
this.customerService.getMenuByRestaurantId(this.restaurantId).subscribe({
  next: (res) => {
    console.log("menu ",res); // Logs the response data to the console
    this.menuItems = res;
  },
  error: (err) => this.errorMessage = "Error loading menu: " + err.message
});

     }
     addToCart(menuId: string) {
      const cartDto = { customerId: this.customerId, menuId, quantity: 1 };
      this.customerService.addToCart(cartDto).subscribe({
        next: (res) => {
          console.log("Response from API:", res);
          alert('Item added to cart successfully!');
          this.router.navigate(['/cart']);
        },
        error: (err) => {
          console.error("Error adding item to cart:", err);
          alert("Error adding item to cart: " + err.message);
        }
      });
    }
    
  
}
