import { Component, inject, OnInit, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import {
  CategoryService,
  PagedListDtoOfCategoryResponse,
  PagedListDtoOfPostResponse,
  PagedListDtoOfTagResponse,
  PostService,
  TagService,
} from '../../../client';
import { firstValueFrom } from 'rxjs';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { parseApiErrors } from '../../utils/error.utils';
import { EntityDialogComponent } from '../admin/entity-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  private readonly tagService = inject(TagService);
  private readonly router = inject(Router);
  private readonly dialog = inject(MatDialog);
  private readonly snackbar = inject(MatSnackBar);

  posts = signal<PagedListDtoOfPostResponse>({ items: [], totalCount: 0 });
  allCategories = signal<PagedListDtoOfCategoryResponse>({
    items: [],
    totalCount: 0,
  });
  allTags = signal<PagedListDtoOfTagResponse>({ items: [], totalCount: 0 });
  stats: any = {
    totalPosts: 0,
    publishedPosts: 0,
    draftPosts: 0,
    totalCategories: 0,
    totalTags: 0,
  };
  loading = true;

  constructor() {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  async loadDashboardData() {
    this.loading = true;
    try {
      const [postRes, categoryRes, tagRes] = await Promise.all([
        firstValueFrom(this.postService.getAllPublishedPost(1, 100)),
        firstValueFrom(this.categoryService.getCategories(1, 100)),
        firstValueFrom(this.tagService.getAllTags(1, 100)),
      ]);

      this.posts.set(postRes);
      this.allCategories.set(categoryRes);
      this.allTags.set(tagRes);
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
      totalTags: this.allTags().totalCount || 0,
    };
  }

  openDialog(
    entity: 'post' | 'category' | 'tag',
    mode: 'create' | 'edit',
    item?: any,
  ) {
    if (entity == 'post' && mode == 'create') {
      this.router.navigate(['/post/create'], {
        replaceUrl: true,
      });
      return;
    }

    if (entity == 'post' && mode == 'edit') {
      this.router.navigate(['/post/update/' + item.postId], {
        replaceUrl: true,
      });
      return;
    }

    const ref = this.dialog.open(EntityDialogComponent, {
      width: '600px',
      data: { entity, mode, item },
    });

    ref.afterClosed().subscribe((result) => {
      if (!result || result.action === 'cancel') return;

      const payload = result.payload;
      if (entity === 'category') {
        if (result.action === 'create') {
          this.categoryService.createCategory(payload).subscribe({
            next: () => {
              this.snackbar.open('Category created', 'Close', {
                duration: 3000,
              });
            },
            error: (err) => {
              let errorMsg = parseApiErrors(err);
              this.snackbar.open(
                `Failed to create category: ${errorMsg}`,
                'Close',
                {
                  duration: 3000,
                },
              );
            },
            complete: async () => await this.loadDashboardData(),
          });
        } else {
          this.categoryService
            .updateCategory(payload.categoryId, payload)
            .subscribe({
              next: () => {
                this.snackbar.open('Category updated', 'Close', {
                  duration: 3000,
                });
              },
              error: (err) => {
                let errorMsg = parseApiErrors(err);
                this.snackbar.open(
                  `Failed to update category: ${errorMsg}`,
                  'Close',
                  {
                    duration: 3000,
                  },
                );
              },
              complete: async () => await this.loadDashboardData(),
            });
        }
      }

      if (entity === 'tag') {
        if (result.action === 'create') {
          this.tagService.createTag(payload).subscribe({
            next: () => {
              this.snackbar.open('Tag created', 'Close', { duration: 3000 });
            },
            error: (err) => {
              let errorMsg = parseApiErrors(err);
              this.snackbar.open(`Failed to create tag: ${errorMsg}`, 'Close', {
                duration: 3000,
              });
            },
            complete: async () => await this.loadDashboardData(),
          });
        } else {
          this.tagService.updateTag(payload.tagId, payload).subscribe({
            next: () => {
              this.snackbar.open('Tag updated', 'Close', { duration: 3000 });
            },
            error: (err) => {
              let errorMsg = parseApiErrors(err);
              this.snackbar.open(`Failed to update tag: ${errorMsg}`, 'Close', {
                duration: 3000,
              });
            },
            complete: async () => await this.loadDashboardData(),
          });
        }
      }
    });
  }
}
