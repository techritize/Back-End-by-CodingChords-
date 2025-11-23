import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, User } from '../services/api.service';

@Component({
  selector: 'app-contact-staff',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact-staff.html',
  styleUrls: ['./contact-staff.css']
})
export class ContactStaff {
  message = '';
  suggestions = '';
  isSending = false;
  feedback = '';

  constructor(private api: ApiService) {}

  onSubmit() {
    if (!this.message || !this.suggestions) {
      this.feedback = '⚠️ Please fill in both fields.';
      return;
    }

    const payload = {
      message: this.message,
      suggestions: this.suggestions
    };

    this.isSending = true;
    this.feedback = '📨 Sending message...';

    this.api.contactStaff(payload).subscribe({
      next: () => {
        this.feedback = '✅ Message sent successfully!';
        this.isSending = false;
        this.message = '';
        this.suggestions = '';
      },
      error: (err) => {
        console.error('Error sending message:', err);
        this.feedback = '❌ Failed to send message. Please try again.';
        this.isSending = false;
      }
    });
  }
}
