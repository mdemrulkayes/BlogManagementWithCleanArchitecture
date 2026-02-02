import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post, Category, Tag, PaginatedResponse } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  private apiUrl = 'https://localhost:7290/api';

  constructor(private http: HttpClient) { }

  // Posts
  getAllPosts(pageNumber: number = 1, pageSize: number = 10): Observable<PaginatedResponse<Post>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);
    return this.http.get<PaginatedResponse<Post>>(`${this.apiUrl}/posts`, { params });
  }

  getPostById(id: string): Observable<Post> {
    return this.http.get<Post>(`${this.apiUrl}/posts/${id}`);
  }

  createPost(post: Post): Observable<Post> {
    return this.http.post<Post>(`${this.apiUrl}/posts`, post);
  }

  updatePost(id: string, post: Post): Observable<Post> {
    return this.http.put<Post>(`${this.apiUrl}/posts/${id}`, post);
  }

  deletePost(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/posts/${id}`);
  }

  // Categories
  getAllCategories(): Observable<PaginatedResponse<Category>> {
    return this.http.get<PaginatedResponse<Category>>(`${this.apiUrl}/categories`);
  }

  createCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(`${this.apiUrl}/categories`, category);
  }

  updateCategory(id: string, category: Category): Observable<Category> {
    return this.http.put<Category>(`${this.apiUrl}/categories/${id}`, category);
  }

  deleteCategory(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/categories/${id}`);
  }

  // Tags
  getAllTags(): Observable<PaginatedResponse<Tag>> {
    return this.http.get<PaginatedResponse<Tag>>(`${this.apiUrl}/tags`);
  }

  createTag(tag: Tag): Observable<Tag> {
    return this.http.post<Tag>(`${this.apiUrl}/tags`, tag);
  }

  updateTag(id: string, tag: Tag): Observable<Tag> {
    return this.http.put<Tag>(`${this.apiUrl}/tags/${id}`, tag);
  }

  deleteTag(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/tags/${id}`);
  }

  // Dashboard stats
  getDashboardStats(): Observable<any> {
    return this.http.get(`${this.apiUrl}/dashboard/stats`);
  }
}
