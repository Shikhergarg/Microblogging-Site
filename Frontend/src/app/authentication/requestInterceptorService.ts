import { Injectable } from '@angular/core';
import {
    HttpSentEvent, HttpInterceptor, HttpHeaderResponse,
    HttpRequest, HttpErrorResponse, HttpProgressEvent,
    HttpHandler, HttpResponse, HttpUserEvent
} from '@angular/common/http';
import { CredentialService } from '../services/credentials-service/credentials.service';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable()
export class RequestInterceptorService implements HttpInterceptor {

    constructor(private _credentialService: CredentialService) { }

    addCredentialsToRequest(req: HttpRequest<any>, credential: string): HttpRequest<any> {
        return req.clone({ setHeaders: { Authorization: 'Basic ' + credential } })
    }

intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent
 | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
        try {
            let credential = this._credentialService.GetCredentials();
            let request = req;
            if (credential) {
                request = this.addCredentialsToRequest(req, credential);
            }
            return next.handle(request);
        }
        catch (error) {
            debugger;
            return Observable.throw(error);
        }
    }

}