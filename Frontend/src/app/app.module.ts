import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpService } from './services/http-service/http.service';
import { AppComponent } from './app.component';
import { UsersComponent } from './users/users.component';
import { routing } from './route';
import { DataTableModule } from 'primeng/datatable';
import { DataScrollerModule } from 'primeng/datascroller';
import { DataViewModule } from 'primeng/dataview';
import {BreadcrumbModule} from 'primeng/breadcrumb';
import {MenuItem} from 'primeng/api';
import { HttpClientModule } from '@angular/common/http';
import {TabMenuModule} from 'primeng/tabmenu';
import { DataListModule } from 'primeng/datalist';
import { FollowersComponent } from './followers/followers.component';
import { FollowingsComponent } from './followings/followings.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { PlaygroundComponent } from './playground/playground.component';
import { TweetComponent } from './tweet/tweet.component';
import { SearchComponent } from './search/search.component';
import { SearchInUSerComponent } from './search/search-in-user/search-in-user.component';
import { SearchInTweetComponent } from './search/search-in-tweet/search-in-tweet.component';
import { LoginregisterComponent } from './loginregister/loginregister.component';
import { SignupComponent } from './signup/signup.component';
import { DropdownModule } from 'primeng/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CardModule } from 'primeng/card'; 
import { DialogModule } from 'primeng/dialog'
import { GrowlModule } from 'primeng/growl'
@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    FollowersComponent,
    FollowingsComponent,
    AnalyticsComponent,
    PlaygroundComponent,
    TweetComponent,
    SearchComponent,
    SearchInUSerComponent,
    SearchInTweetComponent,
    LoginregisterComponent,
    SignupComponent
  ],
  imports: [
    GrowlModule,
    DialogModule,
    BrowserModule,
    FormsModule,
    routing,
    DataTableModule,
    DataScrollerModule,
    DataViewModule,
    DataListModule,
    HttpClientModule,
    BreadcrumbModule,
    TabMenuModule,
    GrowlModule,
    BrowserAnimationsModule,
    DropdownModule,
    CardModule
  ],
  providers: [HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
