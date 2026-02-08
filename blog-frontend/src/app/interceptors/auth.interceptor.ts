import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const token = authService.getToken();

  // 1. Attach Token to Outgoing Request
  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  // 2. Handle Responses & Errors
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        // Clear token (Using localStorage directly as requested,
        // or call authService.logout() if you have that method)
        localStorage.removeItem('token');

        // Redirect to Login
        router.navigate(['/login']);
      }

      // Propagate the error so the component can handle it if needed
      return throwError(() => error);
    }),
  );
};
