import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  stats: any = {
    totalPosts: 0,
    publishedPosts: 0,
    draftPosts: 0,
    totalCategories: 0
  };

  recentPosts: any[] = [];
  loading = true;

  constructor(private blogService: BlogService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.blogService.getDashboardStats().subscribe(
      data => {
        this.stats = data;
        this.loading = false;
      },
      error => {
        console.error('Error loading dashboard stats:', error);
        this.loading = false;
      }
    );

    this.blogService.getAllPosts(1, 5).subscribe(
      data => {
        this.recentPosts = data.items;
      },
      error => {
        console.error('Error loading recent posts:', error);
      }
    );
  }
}
