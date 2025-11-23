import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-logout',
  standalone: true,
  template: `<p>Logging out...</p>`
})
export class Logout {
  constructor(private api: ApiService, private router: Router) {
    this.logoutUser();
  }

  logoutUser() {
    // Call API if needed
    this.api.logout().subscribe({
      next: () => this.clearAndRedirect(),
      error: () => this.clearAndRedirect() // still redirect even if error
    });
  }

  clearAndRedirect() {
    localStorage.removeItem('token'); // clear JWT or session
    this.router.navigate(['/login']);  // redirect to login page
  }
}
