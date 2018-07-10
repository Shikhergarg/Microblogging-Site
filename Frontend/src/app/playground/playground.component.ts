import { Component, OnInit } from '@angular/core';
import { Constants } from '../constant';
import { HttpService } from '../services/http-service/http.service';
import {fintweet} from './fintweet';
import {CredentialService} from '../services/credentials-service/credentials.service';
import { User } from '../users/users.component';
import {Router} from '@angular/router'; 
@Component({
 selector: 'app-playground',
 templateUrl: './playground.component.html',
 styleUrls: ['./playground.component.css']
})
export class PlaygroundComponent implements OnInit {
  display1: boolean = false;
display: boolean = false; 
  tweetId:number;
 tweets: any
 tweetContent: string
 tweetObj:any
 userTweetDetails:UserTweetDetails= new UserTweetDetails();
 tweet:Tweet= new Tweet();
 userId: number;
 likeDislikeTweet: LikeDislikeTweet=new LikeDislikeTweet();
 editObject:fintweet=new fintweet();
 constructor(private _http: HttpService,private _router: Router,private _credentialService: CredentialService) {
   this.userId=this._credentialService.GetUserId()
  }

 ngOnInit() {
   this.getAllTweets()
 }

 composeNewTweet(tweetContent:string)
 {
  this.userTweetDetails.Content = tweetContent;
  this.userTweetDetails.UserId = this._credentialService.GetUserId();
  this._http.addNewTweet(this.userTweetDetails)
  .subscribe((event)=>{
  this.getAllTweets()
  })
 }
 
showDialog() {
this.display = true;
} 


showDialog1(tweet:any) {
 // console.log(tweet.TweetId)
this.tweetContent = tweet.Content;
this.tweetId=tweet.TweetId;
this.display1 = true;
}  
 getAllTweets(){
   if(this._credentialService.isUserLoggedIn.getValue()==false)
   {
    this._router.navigate(['/loginregister']);
    return;
   }
   this._http.getAllTweets(this._credentialService.GetUserId())
     .subscribe((event:any[])=>{
      console.log(event)
      this.tweets = event;
      event.forEach(x=>{
          x.user.ImagePath="http://localhost:51702/"+x.user.ImagePath;
        })
    })
     console.log(this.tweets);
 }

 likeTweet(tweet: any){

   tweet.IsTweetLikedByUser = true
   tweet.IsTweetDislikedByUser = false
   console.log(tweet.TweetId);
   this.likeDislikeTweet.UserId=this._credentialService.GetUserId()
   this.likeDislikeTweet.TweetId=tweet.TweetId
   this._http.likeTweet(this.likeDislikeTweet)
     .subscribe((data) => {})
 }

 dislikeTweet(tweet: any){
  console.log(tweet.TweetId);
  tweet.IsTweetLikedByUser = false;
  tweet.IsTweetDislikedByUser = true;
  this.likeDislikeTweet.UserId=this._credentialService.GetUserId()
  this.likeDislikeTweet.TweetId=tweet.TweetId;
   this._http.dislikeTweet(this.likeDislikeTweet)
     .subscribe((data) => {})
 }

editTweet(editTweetContent:string){
  console.log("edit tweet "+editTweetContent);
  this.tweet.TweetId = this.tweetId;
  this.tweet.Content = editTweetContent;
  
  this._http.editTweet(this.tweet)
  .subscribe((data) => {
  this.getAllTweets()
  })
  }
 deleteTweet(tweet: any){
   console.log(tweet.TweetId);
   this._http.deleteTweet( tweet.TweetId)
   .subscribe((data) =>{this.getAllTweets();
  )})
 }
}

export class Tweet{
  Content: string 
  TweetId: number 
  }
  
  export class UserTweetDetails{
  Content: string
  UserId: number
  }
  
  class LikeDislikeTweet{
  TweetId: number
  UserId: number
  } 

