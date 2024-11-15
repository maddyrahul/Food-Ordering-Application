import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../models/CartItem';
import { CustomerService } from '../../services/customer.service';
import { CartService } from '../../services/cart.service';
import { RestaurantService } from '../../services/restaurant.service';
import { Router } from '@angular/router';
import { Menu } from '../../models/Menu';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  menucartItems:any[]=[];
  menuItems: Menu[] = [];
  totalAmount: number = 0;
  customerId: string = ''; // Assuming you have the customer ID stored somehow




  constructor(private customerService: CustomerService, 
    private router: Router,
    private cartService:CartService,
    private restaurantService:RestaurantService, private orderService: OrderService
  ) {
    this.customerId = localStorage.getItem('userId') || '';
  }
  
  ngOnInit(): void {
    this.loadCartItems();
  }

  // Load cart items from the API
  loadCartItems() {
    this.cartService.getCartItems(this.customerId).subscribe({
      next: (cartItems) => {
        this.cartItems = cartItems;
       // this.menucartItems=cartItems;
        console.log("all carts", this.cartItems)
        this.loadMenuDetails();
      },
      error: (err) => console.error("Error loading cart items:", err)
    });
  }

  // Load menu details for each cart item (like name and price)
  loadMenuDetails() {
    this.menuItems = [];
    this.totalAmount = 0;

    for (const cartItem of this.cartItems) {
      this.restaurantService.getMenuItemById(cartItem.menuId).subscribe({
        next: (menu) => {
          this.menuItems.push(menu);
          this.calculateTotal();
        },
        error: (err) => console.error("Error loading menu item details:", err)
      });
    }
  }

  // Calculate the total amount in the cart
  calculateTotal() {
    this.totalAmount = 0;
    for (const cartItem of this.cartItems) {
      const menuItem = this.menuItems.find(item => item.menuId === cartItem.menuId);
      if (menuItem) {
        this.totalAmount += menuItem.price * cartItem.quantity;
      }
    }
  }

// Method to handle the removal of an item from the cart
removeFromCart(cartId: string) {
  this.cartService.removeCartItem(cartId).subscribe({
    next: (result) => {
      alert("Item removed from cart successfully!"); // Success message from server
      this.loadCartItems(); // Reload cart items after removal
    },
    error: (err) => console.error("Error removing item from cart:", err)
  });
}

 // Update the quantity of a single item in the cart
updateQuantity(cartId: string, quantity: number) {
  const updateDto = { cartId, quantity };
  
  this.cartService.updateCartItem(updateDto).subscribe({
    next: () => {
      alert('Quantity updated successfully');
      
      // Update the quantity locally in cartItems array
      const cartItem = this.cartItems.find(item => item.cartId === cartId);
      if (cartItem) {
        cartItem.quantity = quantity; // Update quantity locally
      }
      
      this.calculateTotal(); // Recalculate total amount without reloading all items
    },
    error: (err) => console.error("Error updating item quantity:", err)
  });
}


// Place an order
placeOrder() {
  this.orderService.placeOrder(this.customerId).subscribe({
    next: (message) => {
      alert('Orderd Successfully'); // Show a success message
      this.router.navigate(['/orders']); // Redirect to orders page
    },
    error: (err) => console.error("Error placing order:", err)
  });
}

}
