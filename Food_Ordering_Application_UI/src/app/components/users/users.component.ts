import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  role: string | null = null; 
  currentUserId: string | null = null;
  constructor(private userService: UserService,private router: Router,private authService:AuthService) {}

  ngOnInit(): void {
    this.currentUserId = this.authService.getUserId();
    this.role = this.authService.getRole();
    console.log("role",this.role)
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe((users) => {
      this.users = users;
    });
  }

  editUser(userId: string): void {
    if (userId === this.currentUserId && this.role==='Admin') {
      alert("You cannot edit your own account.");
      return;
    }
    this.router.navigate(['/edit-user', userId]);
  }

  deleteUser(userId: string): void {
    if (userId === this.currentUserId && this.role==='Admin')  {
      alert("You cannot delete your own account.");
      return;
    }

    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.deleteUser(userId).subscribe(() => {
        this.loadUsers();  // Reload the user list after deletion
      });
    }
  }
}
