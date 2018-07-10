import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingsComponent } from './followings/followings.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { PlaygroundComponent } from './playground/playground.component';
import { SearchComponent } from './search/search.component';
import { LoginregisterComponent } from './loginregister/loginregister.component';
import { SignupComponent } from './signup/signup.component';
import { SearchInTweetComponent } from './search/search-in-tweet/search-in-tweet.component';
import { SearchInUSerComponent } from './search/search-in-user/search-in-user.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'loginregister',
        pathMatch: 'full'
    },
    {
        path: 'loginregister',
        component: LoginregisterComponent
    },
    {
        path: 'signup',
        component: SignupComponent
    },
    {
        path: 'User',
        component: UsersComponent
    },
    {
        path: 'Follower',
        component: FollowersComponent
    },
    {
        path: 'Following',
        component: FollowingsComponent
    },
    {
        path: 'Analytics',
        component: AnalyticsComponent
    },
    {
        path: 'PlayGround',
        component: PlaygroundComponent
    },
    {
        path:'Search',
        component: SearchComponent,
        children:[
            {path: 'playground/:text',component:SearchInTweetComponent},
            {path: 'users/:text',component:SearchInUSerComponent}
        ]
    }
];

export const routing = RouterModule.forRoot(routes);