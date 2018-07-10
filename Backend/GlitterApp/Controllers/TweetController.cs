//finalproject
using System;
using System.Collections.Generic;
using BusinessLayer;
using Shared.DataTransferObject;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using GlitterApp.Models;
using ExitTest.Shared.DTO;

namespace GlitterApp.Controllers
{
	public class TweetController : ApiController
	{
		private TweetBDC _tweetBDC { get; set; }

		public TweetController()
		{
			this._tweetBDC = new TweetBDC();
		}


		[HttpGet]
		public List<TweetDTO> GetAllTweets([FromUri]int userId)
		{
			//int userId = 1;
			return this._tweetBDC.GetAllTweets(userId);
		}
		[HttpGet]
		public TweetDTO GetMostLikedTweets()
		{
			return this._tweetBDC.GetMostLikedTweets();
		}
		[HttpPost]
		public List<ErrorDTO> AddNewTweet([FromBody]TweetDTO tweetDTO)
		{
			//Debug.WriteLine("Tweet content is "+tweetContent);

			return this._tweetBDC.AddNewTweet(tweetDTO.Content,tweetDTO.UserId);
			
		}
		[HttpDelete]
		public void DeleteTweet([FromUri] int tweetId)
		{
			//int tweetId = 1;
			this._tweetBDC.deleteTweet(tweetId);
			//return true;
		}
		[HttpGet]
		public string GetMostTweetingUser()
		{
			return this._tweetBDC.GetMostTweetingUser();
		}

		public int GetTodayTweetsCount()
		{
			return this._tweetBDC.GetTodayTweetsCount();
		}
		[HttpPost]
		public bool DislikeTweet([FromBody] TweetDTO tweetDTO)
		{
			//int userId = 1;
			return this._tweetBDC.DislikeTweet(tweetDTO.UserId, tweetDTO.TweetId);
		}
		[HttpPost]
		public bool LikeTweet([FromBody] TweetDTO tweetDTO)
		{
			//int userId = 1;
			return this._tweetBDC.LikeTweet(tweetDTO.UserId, tweetDTO.TweetId);
		}
		[HttpPost]
		public bool editTweet ([FromBody] TweetDTO finTweet)
		{
			this._tweetBDC.editTweet(finTweet.TweetId, finTweet.Content);
			return true;
		}
	}
}