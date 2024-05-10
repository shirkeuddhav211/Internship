import { CustomError } from './custom-error.model';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';


export abstract class BaseService {
    constructor() { }
    protected handleError(response: any) {
        const customError = new CustomError();
        customError.responseStatus = response.status;

        // if (response.length > 0) {
        //     // error string
        //     if (response.error && typeof response.error === 'string') {
        //         customError.message += response.error;
        //     }
        //     return throwError(() => customError);
        // }
       

        if (response.status === 404) {
            customError.message += 'Requested entity not found.';
            return throwError(() => customError);
        }
       

        if (response.status === 401 || response.status === 403) {
            customError.message += 'You are not authorized to access requested resource.';
            return throwError(() => customError);
        }

        if (response.status === 400) {
            // error string
            if (response.error && typeof response.error === 'string') {
                customError.message += response.error;
                return throwError(() => customError);
            }
             
            // modelstate errors
            if (response.error && typeof response.error === 'object') {
                if (response.error.errors) {
                    for (const key in response.error.errors) {
                        if (response.error.errors[key]) {
                            const errorCode = response.error.errors[key];
                            customError.message = errorCode.reduce(
                                (accumulator: any, currentValue: any) =>
                                    accumulator + currentValue + ' <br />',
                                customError.message
                            );
                        }
                    }
                }
                else if (response.error && typeof response.error.Errors.login_failure === 'object') {
                    customError.message += response.error.Errors.login_failure;
                    return throwError(() => customError);
                }
                else if (response.error.Errors && typeof response.error.Errors === 'object') {
                    for (const key in response.error.Errors.ERROR) {
                        if (response.error.Errors.ERROR[key]) {
                            const errorCode = response.error.Errors.ERROR[key];
                            if (typeof errorCode == 'string') {
                                customError.message = errorCode;
                            }
                            else {
                                customError.message = errorCode.reduce(
                                    (accumulator: any, currentValue: any) =>
                                        accumulator + currentValue + ' <br />',
                                    customError.message
                                );
                            }

                        }
                        else {

                        }
                    }
                }                    
                return throwError(() => customError);
            }
        }
        if (response.length > 0)
        {
            customError.message += response
            return throwError(() => customError);
        }
        return throwError(() => customError);
    }
}
