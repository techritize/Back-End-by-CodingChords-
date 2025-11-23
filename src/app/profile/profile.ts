import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiService, User } from '../services/api.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterLink],
  templateUrl: './profile.html',
  styleUrls: ['./profile.css']
})
export class Profile {
  user: User = {
    id: 0,
    name: '',
    companyName: '',
    country: '',
    email: '',
    profilePictureUrl: '',
    lastUpdated: ''
  };

  selectedFile: File | null = null;
  previewUrl: string | ArrayBuffer | null = null;
  canEdit = true;
  message = '';
  isError = false;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile() {
    this.api.getUserProfile().subscribe({
      next: (res: User) => {
        this.user = res;

        // Calculate months since last update
        const lastUpdate = new Date(res.lastUpdated);
        const now = new Date();
        const diffMonths =
          (now.getFullYear() - lastUpdate.getFullYear()) * 12 +
          (now.getMonth() - lastUpdate.getMonth());

        this.canEdit = diffMonths >= 3;
        this.message = '';
      },
    
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;

      const reader = new FileReader();
      reader.onload = () => (this.previewUrl = reader.result);
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if (!this.canEdit) {
      this.isError = true;
      this.message = 'You can only edit your profile after 3 months.';
      return;
    }

    const formData = new FormData();
    formData.append('name', this.user.name);
    formData.append('companyName', this.user.companyName);
    formData.append('country', this.user.country);
    formData.append('email', this.user.email);
    if (this.selectedFile) {
      formData.append('profilePicture', this.selectedFile);
    }

    this.api.updateUserProfile(formData).subscribe({
      next: () => {
        this.isError = false;
        this.message = '✅ Profile updated successfully!';
        this.getProfile();
      },
      error: () => {
        this.isError = true;
        this.message = 'Error updating profile.';
      }
    });
  }
}
