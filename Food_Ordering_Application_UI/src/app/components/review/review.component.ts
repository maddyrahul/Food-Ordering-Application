import { Component, Input } from '@angular/core';
import { Review } from '../../models/Review';
import { RestaurantService } from '../../services/restaurant.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent {
  reviews: Review[] = [];
  responseText: string = '';
  @Input() restaurantId!: string;
  errorMessage: string = '';

  constructor(private restaurantService: RestaurantService) {}

  ngOnInit() {
    this.loadReviews();
  }

  loadReviews() {
    this.restaurantService.viewReviews(this.restaurantId).subscribe({
      next: (res) => this.reviews = res,
      error: (err) => this.errorMessage = err.message
    });
  }

  respondToReview(reviewId: string) {
    const reviewDto = { reviewId, response: this.responseText };
    this.restaurantService.respondToReview(reviewDto).subscribe({
      next: () => {
        alert("Response added successfully!");
        this.loadReviews();  // Reload reviews to show updated response
      },
      error: (err) => this.errorMessage = err.message
    });
  }
}
