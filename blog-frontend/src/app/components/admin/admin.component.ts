import { AfterViewInit, Component, computed, effect, inject, OnInit, signal, viewChild, ViewChild } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EntityDialogComponent } from './entity-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import {
  CategoryResponse,
  CategoryService,
  PostResponse,
  PostService,
  TagResponse,
  TagService,
} from '../../../client';
import { firstValueFrom } from 'rxjs';
import { parseApiErrors } from '../../utils/error.utils';
import { Router } from '@angular/router';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CdkTableModule } from "@angular/cdk/table";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    MatTabsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatCardModule,
    MatSnackBarModule,
    MatPaginatorModule,
    CdkTableModule
  ],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  private readonly snackbar = inject(MatSnackBar);
  private readonly router = inject(Router);

  posts = signal<PostResponse[]>([]);
  categories = signal<CategoryResponse[]>([]);
  tags = signal<TagResponse[]>([]);

  totalPost = computed(() => this.posts().length);
  totalCategory = computed(() => this.categories().length)
  totalTag = computed(() => this.tags().length)

  postDataSource = new MatTableDataSource<PostResponse>(this.posts());
  categoryDataSource = new MatTableDataSource<CategoryResponse>(this.categories());
  tagDataSource = new MatTableDataSource<TagResponse>(this.tags());

  postPaginator = viewChild<MatPaginator>('postPaginator');
  categoryPaginator = viewChild<MatPaginator>('categoryPaginator');
  tagPaginator = viewChild<MatPaginator>('tagPaginator');

  constructor(
    private dialog: MatDialog,
    private postService: PostService,
    private categoryService: CategoryService,
    private tagService: TagService,
  ) {
    effect(() => {
      this.postDataSource.data = this.posts();
      this.categoryDataSource.data = this.categories();
      this.tagDataSource.data = this.tags();
      
      if (this.postPaginator()) {
        this.postDataSource.paginator = this.postPaginator();
      }
      if (this.categoryPaginator()) {
        this.categoryDataSource.paginator = this.categoryPaginator();
      }
      if (this.tagPaginator()) {
        this.tagDataSource.paginator = this.tagPaginator();
      }
    });
  }

  ngOnInit(): void {
    this.loadAll();
  }

  async loadAll() {
    const [postRes, categoryRes, tagRes] = await Promise.all([
      firstValueFrom(this.postService.getAllPublishedPost(1, 50)),
      firstValueFrom(this.categoryService.getCategories(1, 50)),
      firstValueFrom(this.tagService.getAllTags(1, 50)),
    ]);

    this.posts.set(postRes.items || []);
    this.categories.set(categoryRes.items || []);
    this.tags.set(tagRes.items || []);
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
            complete: async () => await this.loadAll(),
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
              complete: async () => await this.loadAll(),
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
            complete: async () => await this.loadAll(),
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
            complete: async () => await this.loadAll(),
          });
        }
      }
    });
  }

  deleteEntity(entity: 'post' | 'category' | 'tag', id: string) {
    if (!confirm('Are you sure?')) return;
    if (entity === 'post')
      this.postService.deletePost(id).subscribe({
        next: () => {
          this.snackbar.open('Post deleted', 'Close', { duration: 3000 });
        },
        error: (err) => {
          let errorMsg = parseApiErrors(err);
          this.snackbar.open(`Failed to delete post: ${errorMsg}`, 'Close', {
            duration: 3000,
          });
        },
        complete: async () => await this.loadAll(),
      });
    if (entity === 'category')
      this.categoryService.deleteCategory(id).subscribe({
        next: () => {
          this.snackbar.open('Category deleted', 'Close', { duration: 3000 });
        },
        error: (err) => {
          let errorMsg = parseApiErrors(err);
          this.snackbar.open(
            `Failed to delete category: ${errorMsg}`,
            'Close',
            {
              duration: 3000,
            },
          );
        },
        complete: async () => await this.loadAll(),
      });
    if (entity === 'tag')
      this.tagService.deleteTag(id).subscribe({
        next: () => {
          this.snackbar.open('Tag deleted', 'Close', { duration: 3000 });
        },
        error: (err) => {
          let errorMsg = parseApiErrors(err);
          this.snackbar.open(`Failed to delete tag: ${errorMsg}`, 'Close', {
            duration: 3000,
          });
        },
        complete: async () => await this.loadAll(),
      });
  }
}
