import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  // Add more routes as needed
  // { path: 'posts', component: PostListComponent },
  // { path: 'categories', component: CategoryListComponent },
  // { path: 'tags', component: TagListComponent },
];
