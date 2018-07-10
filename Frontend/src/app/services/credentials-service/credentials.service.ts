import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'Rxjs'

@Injectable({
  providedIn: 'root'
})
export class CredentialService {

  isUserLoggedIn : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  SaveCredentials = (emailId: string, password: string, userId: number): void => {
    sessionStorage["credentials"] = window.btoa(emailId + ":" + password);
    sessionStorage["userid"] = userId;
    this.isUserLoggedIn.next(true);
  }

  GetCredentials = (): string => {
    return window.atob(sessionStorage["credentials"] ? sessionStorage["credentials"] : '');
  }

  GetUserId = (): number => {
    return sessionStorage["userid"];
  }

  ClearCredentials = (): void => {
    sessionStorage.removeItem("credentials");
    this.isUserLoggedIn.next(false);
  }
}