export namespace Constants {
    export const CommonConstants ={
        ApiBaseUrl : 'http://localhost:51702/api/',
        ServerPath : 'http://localhost:51702/'
    }
    export namespace UserConstants {
        export const Urls = {
            GetAllUsers: 'user/GetAllUser',
            LoginUser:'user/Login',
            RegisterUser:'user/RegisterUser'            
        }
    }
    export namespace TweetConstants {
        export const Urls = {
            GetAllTweets: 'tweet/GetAllTweets',
            AddNewTweet: 'tweet/AddNewTweet',
            DeleteTweet: 'tweet/DeleteTweet'
        }
    }
    export namespace FollowersConstants {
        export const Urls = {
            GetAllFollowers:'follow/getallfollowers',
            GetFollowerCount: 'follow/GetFollowerCount',
        }
    }
    export namespace FollowingsConstants {
        export const Urls = {
            GetAllFollowings:'follow/getallfollowing',
            GetFollowingCount: 'follow/GetFollowingCount',
            UnFollow:'follow/UnfollowUser',
            Follow:'follow/FollowUser'
        }
    }
    export namespace AnalyticsConstants {
        export const Urls = {
            GetMostTrendingHashtag:'hash/GetMostTrendingHashtag',
            GetMostTweetingUser:'tweet/GetMostTweetingUser',
            GetMostLikedTweets: 'tweet/getmostlikedtweets',
            GetTodayTweetsCount:'tweet/GetTodayTweetsCount'
        }
    }
    export namespace SearchConstants{
        export const Urls={
            GetAllSearchedTweets:'hash/GetAllSearchedTweets?searchText=',
            GetAllSearchUsers:'hash/GetAllSearchUsers?searchText='
        }
    }
}