import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../../services/restaurant.service';
import { Menu } from '../../models/Menu';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-menu',
  templateUrl: './edit-menu.component.html',
  styleUrl: './edit-menu.component.css'
})
export class EditMenuComponent implements OnInit {
  menuItem: Menu = { restaurantId:'',name: '', price: 0, menuId: '' };
  menuId: string = '';

  constructor(
    private restaurantService: RestaurantService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.menuId = this.route.snapshot.paramMap.get('menuId')!;
    console.log("menu id",this.menuId)
    this.loadMenuItem();
  }

  loadMenuItem(): void {
    this.restaurantService.getMenuItemById(this.menuId).subscribe({
      next: (menu) => this.menuItem = menu,
      error: (err) => alert("Error loading getting menu item: " + err.message)
    });
  }

  updateMenuItem(): void {
    const updatedItem = { name: this.menuItem.name, price: this.menuItem.price };
    this.restaurantService.editMenuItem(this.menuId, updatedItem).subscribe({
      next: () => {
        alert("Menu item updated successfully!");
        this.router.navigate(['/all-restaurants']);
      },
      error: (err) => alert("Error updating menu item: " + err.message)
    });
  }
}
