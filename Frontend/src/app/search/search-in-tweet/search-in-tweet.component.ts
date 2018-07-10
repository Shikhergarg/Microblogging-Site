import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../../services/http-service/http.service'
import { Constants } from '../../constant';
import { User } from '../../users/users.component';
import { ActivatedRoute } from '@angular/router'; 

@Component({
  selector: 'app-search-in-tweet',
  templateUrl: './search-in-tweet.component.html',
  styleUrls: ['./search-in-tweet.component.css']
})
export class SearchInTweetComponent implements OnInit {
  searchedTweets: any[];
constructor(private _http: HttpService, private route: ActivatedRoute) { }

ngOnInit() {
this.route.params.subscribe(params => {
this.GetAllSearchedTweets(params['text']);
});
}

GetAllSearchedTweets(searchText: string) {
// return this.http.get(this.baseUrl+url+"?searchText="+searchText)
// this._http.GetAllSearchedTweets(Constants.SearchConstants.Urls.GetAllSearchedTweets,searchText)
// .subscribe((event:Tweet[])=>{
// this.searchedTweets=event
// })
this._http.GetRequest(Constants.SearchConstants.Urls.GetAllSearchedTweets + searchText)
.subscribe((event: Tweet[]) => {
this.searchedTweets = event;
})
}
}

export interface Tweet {
Content: string
PostDate: Date
UserId: number
LikeCount: number
DislikeCount: number
UpdateDate: Date
} 