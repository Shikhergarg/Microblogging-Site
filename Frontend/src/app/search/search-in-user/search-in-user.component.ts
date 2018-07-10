import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../services/http-service/http.service'
import { Constants } from '../../constant';
import {User} from '../../users/users.component';
import { Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CredentialService } from '../../services/credentials-service/credentials.service';

@Component({
 selector: 'app-search-in-user',
 templateUrl: './search-in-user.component.html',
 styleUrls: ['./search-in-user.component.css']
})
export class SearchInUSerComponent implements OnInit {
 searchedUsers: SearchedUser[];
 searchTxt:string;
 userFollowing :UserFollowing=new UserFollowing();
 constructor(private _http: HttpService, private route: ActivatedRoute,private credentialSerice: CredentialService) { }

 ngOnInit() {
   this.route.params.subscribe(params => {
     this.GetAllSearchedUsers(params['text']);
   });
 }
 GetAllSearchedUsers(searchText:string)
 {
   this.searchTxt=searchText;
   this._http.GetRequest(Constants.SearchConstants.Urls.GetAllSearchUsers + searchText)
   .subscribe((event: SearchedUser[]) => {
    //console.log(event);
    event.forEach(x => {
      x.UserDTO.ImagePath = "http://localhost:51702/" + x.UserDTO.ImagePath;
     // console.log(x.user.ImagePath);
      })
     this.searchedUsers = event;
     console.log(event);
   })
 }
 Follow(following) {
  this.userFollowing.userId = this.credentialSerice.GetUserId();
  this.userFollowing.followingId = following.UserDTO.UserId;
  this._http.Follow(this.userFollowing)
      .subscribe(
      data => {
        this.GetAllSearchedUsers(this.searchTxt);
      },
      err => console.log(err))
}
 UnFollow(following) {
  this.userFollowing.userId = this.credentialSerice.GetUserId();
  this.userFollowing.followingId = following.UserDTO.UserId;
  this._http.unfollowUser(this.userFollowing)
      .subscribe(
      data => {
        this.GetAllSearchedUsers(this.searchTxt);
      },
      err => console.log(err))
}
}

export interface SearchedUser {
 UserDetails: User;
 AlreadyFollowing: boolean;
}

class UserFollowing {
 userId: number;
 followingId: number;
}

