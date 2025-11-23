import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-submit-content',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './submit-content.html',
  styleUrls: ['./submit-content.css']
})
export class SubmitContent {
  productName = '';
  description = '';
  keyPoint1 = '';
  keyPoint2 = '';
  keyPoint3 = '';
  selectedFiles: File[] = [];
  message = '';
  isSubmitting = false;

  constructor(private api: ApiService) {}

  onFileSelected(event: any) {
    this.selectedFiles = Array.from(event.target.files);
  }

  onSubmit() {
    if (!this.productName || !this.description) {
      this.message = 'Please fill in all required fields.';
      return;
    }

    if (this.description.length > 400) {
      this.message = 'Description cannot exceed 400 characters.';
      return;
    }

    const formData = new FormData();
    formData.append('productName', this.productName);
    formData.append('description', this.description);
    formData.append('keyPoint1', this.keyPoint1);
    formData.append('keyPoint2', this.keyPoint2);
    formData.append('keyPoint3', this.keyPoint3);

    this.selectedFiles.forEach((file, index) => {
      formData.append('files', file);
    });

    this.isSubmitting = true;
    this.message = 'Submitting...';

    this.api.submitClientContent(formData).subscribe({
      next: () => {
        this.message = '✅ Content submitted successfully!';
        this.isSubmitting = false;
        this.clearForm();
      },
      error: (err) => {
        console.error('Submit content error:', err);
        this.message = '❌ Error submitting content.';
        this.isSubmitting = false;
      }
    });
  }

  clearForm() {
    this.productName = '';
    this.description = '';
    this.keyPoint1 = '';
    this.keyPoint2 = '';
    this.keyPoint3 = '';
    this.selectedFiles = [];
  }
}
