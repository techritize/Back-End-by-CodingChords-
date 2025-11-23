import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService, Notification } from '../services/api.service';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notifications.html',
  styleUrls: ['./notifications.css']
})
export class Notifications {
  notifications: Notification[] = [];
  message = '';
  isLoading = true;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadNotifications();
  }

  loadNotifications() {
    this.api.getClientNotifications().subscribe({
      next: (res) => {
        this.notifications = res as Notification[];
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading notifications:', err);
        this.message = '❌ Failed to load notifications.';
        this.isLoading = false;
      }
    });
  }

  markAsRead(notificationId: number) {
    this.api.markNotificationRead(notificationId).subscribe({
      next: () => {
        const notif = this.notifications.find(n => n.id === notificationId);
        if (notif) notif.read = true;
      },
      error: (err) => {
        console.error('Error marking notification as read:', err);
      }
    });
  }
}
