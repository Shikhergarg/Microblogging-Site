import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http-service/http.service';
import { Constants } from '../constant';
import {CredentialService} from '../services/credentials-service/credentials.service';
import { User } from '../users/users.component';
import {Router} from '@angular/router'; 
@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {
  followers: any
  followersCount:any
  constructor(private _http: HttpService,private _router: Router, private _credentialService: CredentialService ) { }

  ngOnInit() {
    this.islogin()
    this.getFollowers()
    //this.getFollowersCount()
  }
  islogin()
{
  if(this._credentialService.isUserLoggedIn.getValue()==false)
   {
    this._router.navigate(['/loginregister']);
    return;
   }
}
  getFollowers(){
    this._http.GetAllFollowers(Constants.FollowersConstants.Urls.GetAllFollowers,this._credentialService.GetUserId())
        .subscribe((d: any[]) => {
          d.forEach(x => {
            x.ImagePath = "http://localhost:51702/"+ x.ImagePath;
           // console.log(x.user.ImagePath);
            });
          this.followers = d;
        }
      );
  }

  // getFollowersCount(){
  //   this._http.GetAllFollowers(Constants.FollowersConstants.Urls.GetFollowerCount)
  //       .subscribe(
  //       data => {        
  //         this.followersCount = data
  //       },
  //       err => console.log(err),
  //     )
  // }
}
