import { Component } from '@angular/core';
import { LoginModel } from '../../models/LoginModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  credentials: LoginModel = { username: '', password: '' };
  jwtHelper = new JwtHelperService();
  errorMessage: string | null = null; // For storing backend error messages

  constructor(private authService: AuthService, private router: Router, private userService: UserService) {}

  login() {
    this.authService.login(this.credentials).subscribe({
      next: (result) => {
        const token = result?.token;
        if (!token) {
          this.errorMessage = 'Token not found in the response';
          return;
        }

        localStorage.setItem('token', token);

        const decodedToken = this.jwtHelper.decodeToken(token);
        const userId = decodedToken?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        const role = decodedToken?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        const username = decodedToken?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

        localStorage.setItem('userId', userId);
        this.authService.setUser(username);

        // Redirect based on role
        if (role === 'Customer') {
          this.router.navigate(['/list-restaurant']);
        } else if (role === 'RestaurantOwner') {
          this.router.navigate(['/all-restaurants']);
        } else if (role === 'Admin') {
          this.router.navigate(['/users']);
        }
      },
      error: (err) => {
        console.error('Login error:', err);
        this.errorMessage = err.error?.message || 'An error occurred. Please try again.'; // Set error message from backend
      }
    });
  }
}
