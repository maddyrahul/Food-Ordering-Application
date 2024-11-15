import { Component } from '@angular/core';
import { Menu } from '../../models/Menu';
import { RestaurantService } from '../../services/restaurant.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-menu',
  templateUrl: './add-menu.component.html',
  styleUrl: './add-menu.component.css'
})
export class AddMenuComponent {
  menu: Menu = {} as Menu;
  restaurantId: string;

  constructor(
    private restaurantService: RestaurantService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.restaurantId = this.route.snapshot.paramMap.get('restaurantId')!;
    console.log(this.restaurantId)
  }

  // AddMenuComponent
addMenuItem() {
  const menuItemDto = {
    restaurantId: this.restaurantId,
    name: this.menu.name,
    price: this.menu.price
  };

  this.restaurantService.addMenuItem(menuItemDto).subscribe({
    next: () => {
      alert("Menu item added successfully!");
      this.router.navigate(['/all-restaurants']);
    },
    error: (err) => alert("Error adding menu item: " + err.message)
  });
}

}

