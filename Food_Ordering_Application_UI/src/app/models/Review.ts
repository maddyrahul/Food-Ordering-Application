export interface Review {
    reviewId: string;
    customerId: string;
    restaurantId: string;
    rating: number;
    comment?: string;
    response?: string;
    datePosted: Date;
}