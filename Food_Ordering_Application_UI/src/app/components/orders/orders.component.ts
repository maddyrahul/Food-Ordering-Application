import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/Order';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent implements OnInit {
  orders: Order[] = [];
  customerId: string = ''; // Assuming you have the customer ID stored somehow
  orderDetailsArray: any[] = [];
  constructor(private orderService: OrderService,private customerService:CustomerService) {
    this.customerId = localStorage.getItem('userId') || '';
  }

  ngOnInit(): void {
    this.loadOrderHistory();
  }

  // Load order history for the customer
  loadOrderHistory() {
    this.orderService.getOrderHistory(this.customerId).subscribe({
      next: (orders) => {
        this.orders = orders;
          console.log("orders",this.orders)
        // Extract orderId from each order and fetch individual order history
        const orderIds = this.orders.map(order => order.orderId); // Collect orderId from each order
        console.log("orderId",orderIds)
        // Fetch details for each orderId
        orderIds.forEach(orderId => {
          this.customerService.getOrderHistory(orderId).subscribe({
            next: (orderDetails) => {
              this.orderDetailsArray.push(orderDetails); // Store each order's details
            },
            error: (err) => console.error(`Error loading order details for orderId ${orderId}:`, err)
          });
        });
      },
      error: (err) => console.error("Error loading order history:", err)
    });
  }
}
