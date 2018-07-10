import { Component, OnInit } from '@angular/core';
import { HttpService } from './services/http-service/http.service'
import { Constants } from './constant'
import { CredentialService } from './services/credentials-service/credentials.service';
import {Router} from '@angular/router'; 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoggedIn$: BehaviorSubject<boolean>;
  followerCount: number = 0;
  followingCount: number = 0;
  
  items: any[];

  constructor(private _http: HttpService,private _router: Router ,private _credentialService :CredentialService) { 

    this.FollowerCount(); this.FollowingCount();
    this.isLoggedIn$ = this._credentialService.isUserLoggedIn;
  }

  ngOnInit() {
    this.items = [
      { label: 'Playground', routerLink: ['/PlayGround'] },
      { label: `Followers(0)`, routerLink: ['/Follower'] },
      { label: `Following(0)`, routerLink: ['/Following'] },
      { label: 'Search', routerLink: ['/Search'] },
      { label: 'Analytics', routerLink: ['/Analytics'] }
    ];
    this.FollowerCount();
    this.FollowingCount();
  }


  FollowerCount() {
    this._http.GetFollowerCount(Constants.FollowersConstants.Urls.GetFollowerCount,this._credentialService.GetUserId())
      .subscribe((d: number) => {
        this.items[1].label = `Followers(${d})`;
      });
  }

  FollowingCount() {
    this._http.GetFollowerCount(Constants.FollowingsConstants.Urls.GetFollowingCount,this._credentialService.GetUserId())
      .subscribe((d: number) => {
        this.items[2].label = `Following(${d})`;
      });
  }
  Logout(){
    this._credentialService.ClearCredentials()
    this._router.navigate(['/loginregister']);
  }
}
