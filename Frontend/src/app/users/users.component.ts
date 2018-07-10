import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http-service/http.service'
import { Constants } from '../constant';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: any[];

  constructor(private _http: HttpService) { }

  ngOnInit() {
   
    this._http.GetRequest(Constants.UserConstants.Urls.GetAllUsers)
      .subscribe((d: User[]) => {
        this.users = d;
      });
  }

}

export interface User {
  UserId: number;
  FirstName: string;
  LastName: string;
  EmailId: string;
  ImagePath: string;
  // user:UsersComponent
}
