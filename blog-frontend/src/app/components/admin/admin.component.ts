import { Component, OnInit } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EntityDialogComponent } from './entity-dialog.component';
import { MatCardModule } from '@angular/material/card';
import {
  CategoryResponse,
  CategoryService,
  PostResponse,
  PostService,
  TagResponse,
  TagService,
} from '../../../client';

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
  ],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  posts: PostResponse[] = [];
  categories: CategoryResponse[] = [];
  tags: TagResponse[] = [];

  constructor(
    private dialog: MatDialog,
    private postService: PostService,
    private categoryService: CategoryService,
    private tagService: TagService,
  ) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.postService.getAllPublishedPost(1, 50).subscribe((r) => {
      this.posts = r.items || [];
    });
    this.categoryService.getCategories(1, 50).subscribe((r) => {
      this.categories = r.items || [];
    });
    this.tagService.getAllTags(1, 50).subscribe((r) => {
      this.tags = r.items || [];
    });
  }

  openDialog(
    entity: 'post' | 'category' | 'tag',
    mode: 'create' | 'edit',
    item?: any,
  ) {
    const ref = this.dialog.open(EntityDialogComponent, {
      width: '600px',
      data: { entity, mode, item },
    });

    ref.afterClosed().subscribe((result) => {
      if (!result || result.action === 'cancel') return;

      const payload = result.payload;
      if (entity === 'category') {
        if (result.action === 'create') {
          this.categoryService
            .createCategory(payload)
            .subscribe(() => this.loadAll());
        } else {
          this.categoryService
            .updateCategory(payload.id, payload)
            .subscribe(() => this.loadAll());
        }
      }

      if (entity === 'tag') {
        if (result.action === 'create') {
          this.tagService.createTag(payload).subscribe(() => this.loadAll());
        } else {
          this.tagService
            .updateTag(payload.id, payload)
            .subscribe(() => this.loadAll());
        }
      }
    });
  }

  deleteEntity(entity: 'post' | 'category' | 'tag', id: string) {
    if (!confirm('Are you sure?')) return;
    if (entity === 'post')
      this.postService.deletePost(id).subscribe(() => this.loadAll());
    if (entity === 'category')
      this.categoryService.deleteCategory(id).subscribe(() => this.loadAll());
    if (entity === 'tag')
      this.tagService.deleteTag(id).subscribe(() => this.loadAll());
  }
}
