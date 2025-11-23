import { Routes } from '@angular/router';
import { Profile } from './profile/profile';
import { SubmitContent } from './submit-content/submit-content';
import { ContactStaff } from './contact-staff/contact-staff';
import { Contents } from './contents/contents';
import { Logout } from './logout/logout';
import { Notifications } from './notifications/notifications';

export const routes: Routes = [

   { path: '', redirectTo: 'profile', pathMatch: 'full' },
  { path: 'profile', component: Profile },
  {path: 'submit-content', component: SubmitContent },

  {path: 'contact-staff', component: ContactStaff},

  {path: 'contents', component: Contents},

  {path: 'notifications', component: Notifications},

  {path: 'logout', component: Logout}

  
];
