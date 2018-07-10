import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http-service/http.service'
import { Constants } from '../constant'
import {CredentialService} from '../services/credentials-service/credentials.service';
import { User } from '../users/users.component';
import {Router} from '@angular/router'; 
@Component({
  selector: 'app-analytics',
  templateUrl: './analytics.component.html',
  styleUrls: ['./analytics.component.css']
})
export class AnalyticsComponent implements OnInit {

  mostTrendingHash:string;
  todayTweetsCount:number;
  mostTweetingUser:string;
  mostLikedTweet:any;
  constructor(private _http: HttpService,private _router: Router, private _credentialService: CredentialService ) { }

  ngOnInit() {
    this.islogin();
    this.GetMostTrendingHashtag();
    this.GetMostLikedTweet();
    this.GetTodayTweetsCount();
    this.GetMostTweetingUser();
  }
  islogin()
  {
    if(this._credentialService.isUserLoggedIn.getValue()==false)
    {
      this._router.navigate(['/loginregister']);
      return;
    }
  }
  GetMostTrendingHashtag(){
    this._http.GetRequest(Constants.AnalyticsConstants.Urls.GetMostTrendingHashtag)
      .subscribe((d: string) => {
        console.log(d)
        this.mostTrendingHash = d;
      });
  };
  GetMostTweetingUser(){
    this._http.GetRequest(Constants.AnalyticsConstants.Urls.GetMostTweetingUser)
      .subscribe((d: string) => {
        console.log(d)
        this.mostTweetingUser = d;
      });
  };
  GetMostLikedTweet(){
    this._http.GetRequest(Constants.AnalyticsConstants.Urls.GetMostLikedTweets)
      .subscribe((d: any) => {
        console.log(d)
        this.mostLikedTweet = d;
      });
  };
  GetTodayTweetsCount(){
    this._http.GetRequest(Constants.AnalyticsConstants.Urls.GetTodayTweetsCount)
    .subscribe((d:number)=>{
      console.log(d)
      this.todayTweetsCount=d;
    })
  }
}
