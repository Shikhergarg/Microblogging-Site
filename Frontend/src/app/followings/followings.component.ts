import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http-service/http.service'
import { Constants } from '../constant';
import { CredentialService } from '../services/credentials-service/credentials.service';
import { User } from '../users/users.component';
import {Router} from '@angular/router'; 
@Component({
  selector: 'app-followings',
  templateUrl: './followings.component.html',
  styleUrls: ['./followings.component.css']
})
export class FollowingsComponent implements OnInit {

  followings:any[];
  userFollowing: UserFollowing = new UserFollowing();

  constructor(private _http: HttpService,private _router: Router, private _credentialService: CredentialService) { }

  ngOnInit() {
    this.islogin();
    this.getfollowings();
  }
  islogin()
{
  if(this._credentialService.isUserLoggedIn.getValue()==false)
   {
    this._router.navigate(['/loginregister']);
    return;
   }
}
  getfollowings()
  {
    this._http.GetFollowing(Constants.FollowingsConstants.Urls.GetAllFollowings, this._credentialService.GetUserId())
      .subscribe((d: any[]) => {
        
        d.forEach(x => {
          x.ImagePath = "http://localhost:51702/"+ x.ImagePath;
         // console.log(x.user.ImagePath);
          })
          console.log(d)
        this.followings = d;
      });
  }
  unfollow(following) {
    this.userFollowing.userId=this._credentialService.GetUserId();
    this.userFollowing.followingId=following.UserId;
    this._http.unfollowUser(this.userFollowing)
        .subscribe(
        data => {
        },
        err => console.log(err))
  }
}
export class UserFollowing{
  userId:number
  followingId:number
}