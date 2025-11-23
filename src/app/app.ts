import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HttpClientModule  // ✅ Add this line to enable API calls
  ],
  template: `<router-outlet></router-outlet>`,
  styleUrls: ['./app.css']
})
export class App {}
