import { Component } from '@angular/core';
import { Restaurant } from '../../models/Restaurant';
import { Menu } from '../../models/Menu';
import { RestaurantService } from '../../services/restaurant.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrl: './restaurant.component.css'
})
export class RestaurantComponent {
  restaurantForm: FormGroup;
  errorMessage: string = '';
  ownerId: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private restaurantService: RestaurantService,
    private router: Router
  ) {
    this.ownerId = localStorage.getItem('userId') || '';

    // Initialize the form with validation
    this.restaurantForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      location: ['', [Validators.required, Validators.minLength(3)]],
      cuisine: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  registerRestaurant() {
    if (this.restaurantForm.invalid) {
      return; // stop if form is invalid
    }

    const restaurantDto = {
      ownerId: this.ownerId,
      ...this.restaurantForm.value
    };

    this.restaurantService.registerRestaurant(restaurantDto).subscribe({
      next: () => {
        alert("Restaurant registered successfully!");
        this.router.navigate(['/all-restaurants']);
      },
      error: (err) => (this.errorMessage = err.message)
    });
  }

  // Convenience getter for easy access to form fields in the template
  get f() {
    return this.restaurantForm.controls;
  }
}