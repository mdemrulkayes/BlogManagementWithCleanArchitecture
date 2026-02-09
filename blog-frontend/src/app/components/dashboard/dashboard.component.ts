import { Component, inject, OnInit, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import {
  CategoryService,
  PagedListDtoOfCategoryResponse,
  PagedListDtoOfPostResponse,
  PostService,
} from '../../../client';
import { firstValueFrom } from 'rxjs';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule,
    DatePipe,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  private readonly postService = inject(PostService);
  private readonly categoryService = inject(CategoryService);
  private readonly router = inject(Router);

  posts = signal<PagedListDtoOfPostResponse>({ items: [], totalCount: 0 });
  allCategories = signal<PagedListDtoOfCategoryResponse>({
    items: [],
    totalCount: 0,
  });

  stats: any = {
    totalPosts: 0,
    publishedPosts: 0,
    draftPosts: 0,
    totalCategories: 0,
  };
  loading = true;

  constructor() {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  async loadDashboardData() {
    this.loading = true;
    try {
      const [postRes, categoryRes] = await Promise.all([
        firstValueFrom(this.postService.getAllPublishedPost(1, 100)),
        firstValueFrom(this.categoryService.getCategories(1, 100)),
      ]);

      this.posts.set(postRes);
      this.allCategories.set(categoryRes);
    } catch (error) {
      console.error('Failed to load data', error);
    } finally {
      this.loading = false;
    }

    this.stats = {
      totalPosts: this.posts().totalCount || 0,
      publishedPosts: 95,
      draftPosts: 25,
      totalCategories: this.allCategories().totalCount || 0,
    };
  }

  navigateToCreatePost() {
    this.router.navigate(['/post/create'], { replaceUrl: true });
  }
}
