import { Injectable } from '@angular/core';
import {HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import { finalize, tap } from "rxjs/operators";
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class LoaderInterceptor implements HttpInterceptor  {
  count = 0;

    constructor(private spinner: NgxSpinnerService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        this.spinner.show()

        this.count++;

        return next.handle(req)

            .pipe(tap(

                event => console.log(event),

                (error:any)=> console.log(error)

            ), finalize(() => {

                this.count--;

                if (this.count == 0) this.spinner.hide()
            })
            );
    }
}