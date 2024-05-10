import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HideRequestMethodInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Add a custom header to simulate a different request method
    const updatedRequest = request.clone({
      setHeaders: {
        'X-Http-Method-Override': request.method
      }
    });

    return next.handle(updatedRequest);
  }
}
