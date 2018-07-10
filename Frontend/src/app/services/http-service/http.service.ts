import { Injectable } from '@angular/core';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import {fintweet} from '../../playground/fintweet';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  baseUrl = "http://localhost:51702/api/";
  // get 
  GetRequest (url: string)  {
    url = this.baseUrl + url;
    return this.http.get(url);
  }

  GetAllSearchedTweets(url:string,searchText:string)
  {
    // const options={ params: new HttpParams().set('searchText', searchText) };
    return this.http.get(this.baseUrl + url+"?searchText="+searchText)
  }

  GetAllSearchedUsers(url:string,searchText:string)
  {
    return this.http.get(this.baseUrl+url+"?searchText="+searchText)
  }

  GetAllFollowers(url:string,userId:Number)
  {
    return this.http.get(this.baseUrl+url+"?userId="+ userId )
  }

  GetFollowerCount(url:string,userId:Number){
    return this.http.get(this.baseUrl+url+"?userId="+ userId)
  }

  GetFollowing(url:string,userId:Number)
  {
    return this.http.get(this.baseUrl+url+"?userId="+ userId)
  }
  
  GetFollowingCount(url:string,userId:Number)
  {
    return this.http.get(this.baseUrl+url+"?userId="+ userId)
  }

  Follow(userFollowing:any)
  {
    return this.http.post("http://localhost:51702/api/follow/FollowUser", userFollowing ,httpOptions)
    
  }
  unfollowUser(userFollowing:any){
    //const body = JSON.stringify(foll);
  
    return this.http.post("http://localhost:51702/api/follow/UnFollowUser",userFollowing, httpOptions)
    }

  GetMostTrendingHashTag(url:string){
    return this.http.get(this.baseUrl+url) 
  }
 
  GetTodayTotalTweet(url:string){
     return this.http.get(this.baseUrl+url)
  }
 
  GetUserWithMaxTweets(url:string){
     return this.http.get(this.baseUrl+url)
  }
 
  GetMostLikedTweet(url:string){
     return this.http.get(this.baseUrl+url)
  }
  likeTweet(userTweet:any){
    let body = JSON.stringify(userTweet);
    return this.http.post("http://localhost:51702/api/tweet/liketweet", body, httpOptions)
  }
 
  dislikeTweet(userTweet:any){
    console.log(userTweet);
    let body = JSON.stringify(userTweet);
    return this.http.post("http://localhost:51702/api/tweet/disliketweet", body, httpOptions)
  }
  editTweet(editTweetObject:any)
  {
    return this.http.post("http://localhost:51702/api/tweet/edittweet",editTweetObject,httpOptions)
  }
 
  deleteTweet(tweetId:Number){
    console.log(tweetId);
    return this.http.delete("http://localhost:51702/api/tweet/deletetweet" + "?tweetId=" + tweetId)
  }
  addNewTweet(newTweet: any)
  {
    console.log(newTweet.Content);
    return this.http.post("http://localhost:51702/api/tweet/addnewtweet", newTweet , httpOptions);
}
getAllTweets(userId: Number){
 // url = this.baseUrl + url;
  return this.http.get("http://localhost:51702/api/tweet/getalltweets"+ "?userId=" + userId);
}

  registerUser(body: any) {

    const headers = new HttpHeaders();
    headers.append('content-type', 'false');
    headers.append('mimeType', 'multipart/form-data');
    headers.append('crossDomain', 'true');
    headers.append('processData', 'false');
    // console.log("In Post Request Service"+URL+body)
    console.log("ff");
    return this.http.post("http://localhost:51702/api/user/registerUser", body, { headers: headers });
    }
    LoginUser(url: string, user: any){
      url = this.baseUrl + url;
      return this.http.post(url, user, httpOptions)
      }  


}
