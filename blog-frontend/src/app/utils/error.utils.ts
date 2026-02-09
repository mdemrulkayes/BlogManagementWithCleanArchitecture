import { HttpErrorResponse } from '@angular/common/http';
import { HttpValidationProblemDetails } from '../../client';

export function parseApiErrors(error: unknown): string {
  // 1. Handle Angular's HttpErrorResponse
  if (error instanceof HttpErrorResponse) {
    const problem = error.error as HttpValidationProblemDetails;

    // Safety check: sometimes the error body is just a blob or string
    if (!problem || typeof problem !== 'object') {
      return error.message || 'An unexpected server error occurred.';
    }

    // Case A: Your specific format (Array of objects with 'message')
    if (Array.isArray(problem.errors)) {
      return problem.errors
        .map((e) => e.message)
        .filter(Boolean)
        .join('\n');
    }

    // Case B: Standard RFC 7807 (Object with field names as keys)
    // e.g. { "email": ["Invalid email"], "password": ["Too short"] }
    if (problem.errors && typeof problem.errors === 'object') {
      return Object.values(problem.errors).flat().join('\n');
    }

    // Case C: Fallback to Title or Detail
    return (
      problem.detail || problem.title || error.message || 'Unknown API Error'
    );
  }

  // 2. Handle standard JavaScript Errors (e.g. exceptions thrown in code)
  if (error instanceof Error) {
    return error.message;
  }

  // 3. Fallback for strings or unknown types
  return typeof error === 'string' ? error : 'An unexpected error occurred.';
}
