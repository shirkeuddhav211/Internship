import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class RemoveHeaderInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Remove a specific header
    const updatedRequest = request.clone({
      headers: request.headers.delete('Request Method')
    });

    return next.handle(updatedRequest);
  }
}
