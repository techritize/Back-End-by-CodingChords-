import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface User {
  id: number;
  name: string;
  companyName: string;
  country: string;
  email: string;
  profilePictureUrl: string;
  lastUpdated: string;
}

export interface Notification {
  id: number;
  message: string;
  date: string;      // or Date if you convert it
  read: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:7049/api'; // change to your backend URL

  constructor(private http: HttpClient) {}

  getUserProfile(): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/users/profile`);
  }

  updateUserProfile(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/profile`, formData);
  }


  submitClientContent(formData: FormData) {
  return this.http.post(`${this.apiUrl}/api/submit-content`, formData);
}

contactStaff(payload: any) {
  return this.http.post('https://localhost:7141/api/contact-staff', payload);
}

getClientContents() {
  return this.http.get('https://localhost:7141/api/client/contents');
}

getClientNotifications() {
  return this.http.get<Notification[]>(`${this.apiUrl}/api/notifications`);
}

markNotificationRead(notificationId: number) {
  return this.http.post(`${this.apiUrl}/api/notifications/${notificationId}/read`, {});
}

logout() {
  return this.http.post(`${this.apiUrl}/api/auth/logout`, {});
}
}
