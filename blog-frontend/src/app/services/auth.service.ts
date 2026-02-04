import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AccessTokenResponse, IdentityService, LoginRequest, RegisterRequest } from '../../client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private identityService: IdentityService) {
    const savedUser = localStorage.getItem('currentUser');
    if (savedUser) {
      this.currentUserSubject.next(JSON.parse(savedUser));
    }
  }

  login(loginRequest: LoginRequest): Observable<AccessTokenResponse> {
    return this.identityService.apiIdentityLoginPost(loginRequest).pipe(
      tap((response: AccessTokenResponse) => {
        if (response && response.accessToken) {
          localStorage.setItem('token', response.accessToken);
        }
      })
    );
  }

  register(user: RegisterRequest): Observable<any> {
    return this.identityService.apiIdentityRegisterPost(user);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  getCurrentUser(): any {
    return this.currentUserSubject.value;
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
