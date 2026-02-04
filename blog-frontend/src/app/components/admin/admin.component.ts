import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EntityDialogComponent } from './entity-dialog.component';
import { BlogService } from '../../services/blog.service';
import { Post, Category, Tag } from '../../models/post.model';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, MatTabsModule, MatTableModule, MatButtonModule, MatIconModule, MatDialogModule, MatCardModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  posts: Post[] = [];
  categories: Category[] = [];
  tags: Tag[] = [];

  constructor(private blogService: BlogService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.blogService.getAllPosts(1, 50).subscribe(r => { this.posts = r.items || []; });
    this.blogService.getAllCategories().subscribe(r => { this.categories = r.items || []; });
    this.blogService.getAllTags().subscribe(r => { this.tags = r.items || []; });
  }

  openDialog(entity: 'post' | 'category' | 'tag', mode: 'create' | 'edit', item?: any) {
    const ref = this.dialog.open(EntityDialogComponent, {
      width: '600px',
      data: { entity, mode, item }
    });

    ref.afterClosed().subscribe(result => {
      if (!result || result.action === 'cancel') return;

      const payload = result.payload;
      if (entity === 'post') {
        if (result.action === 'create') {
          this.blogService.createPost(payload).subscribe(() => this.loadAll());
        } else {
          this.blogService.updatePost(payload.id, payload).subscribe(() => this.loadAll());
        }
      }

      if (entity === 'category') {
        if (result.action === 'create') {
          this.blogService.createCategory(payload).subscribe(() => this.loadAll());
        } else {
          this.blogService.updateCategory(payload.id, payload).subscribe(() => this.loadAll());
        }
      }

      if (entity === 'tag') {
        if (result.action === 'create') {
          this.blogService.createTag(payload).subscribe(() => this.loadAll());
        } else {
          this.blogService.updateTag(payload.id, payload).subscribe(() => this.loadAll());
        }
      }
    });
  }

  deleteEntity(entity: 'post' | 'category' | 'tag', id: string) {
    if (!confirm('Are you sure?')) return;
    if (entity === 'post') this.blogService.deletePost(id).subscribe(() => this.loadAll());
    if (entity === 'category') this.blogService.deleteCategory(id).subscribe(() => this.loadAll());
    if (entity === 'tag') this.blogService.deleteTag(id).subscribe(() => this.loadAll());
  }
}
