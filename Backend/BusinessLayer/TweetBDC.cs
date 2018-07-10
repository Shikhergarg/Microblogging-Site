using System;
using System.Collections.Generic;
using DataAccessLayer.Entity;
using Shared.DataTransferObject;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repository;
using Shared.Validation;
using FinalExitTest.BDC.Validations;
using BusinessLayer.Validations.Validator;
using ExitTest.Shared.DTO;

namespace BusinessLayer
{
	public class TweetBDC
	{
		private TweetRepository _tweetRepo { get; set; }
		private HashRepository _hashRepo { get; set; }
		private TweetHashMapperRepository _tweetHashRepo { get; set; }

		/// <summary>
		/// Constructor of tweet BDC 
		/// to initialize the paramenters
		/// </summary>
		public TweetBDC()
		{
			this._tweetRepo = new TweetRepository();
			this._hashRepo = new HashRepository();
			this._tweetHashRepo = new TweetHashMapperRepository();
		}


		/// <summary>
		/// Forward list of all tweet 
		/// from TweetRepo's GetAllTweet 
		/// provided user's ID
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<TweetDTO> GetAllTweets(int userId)
		{
			//manipulations
			return this._tweetRepo.GetAllTweets(userId);
		}

		/// <summary>
		/// Forward Data From TweetRepo's
		/// AddNewTweet provided Tweet Data
		/// and user who posted
		/// </summary>
		/// <param name="newTweet"></param>
		/// <returns></returns>
		public List<ErrorDTO> AddNewTweet(string content, int userId)
		{
			//manipulations
			List<ErrorDTO> errorList = null;

			List<string> hash = new List<string>();
			for (int i = 0; i < content.Length; i++)
			{
				if (content[i] == '#')
				{
					string temp = "";
					int j;
					for (j = i + 1; j < content.Length; j++)
					{
						if (content[j] == ' ')
							break;
						temp = temp + content[j];
					}
					hash.Add(temp);
					i = j;
				}
			}

			TweetDTO tweet = new TweetDTO
			{
				Content = content,
				PostDate = DateTime.Now,
				UserId = userId, //Alert
				LikeCount = 0,
				DislikeCount = 0,
				UpdateDate = DateTime.Now
			};
			ExitTestValidationResult validationResult = Validator<TweetValidator, TweetDTO>.Validate(tweet, ruleSet: "Compose Tweet");
			if (validationResult.IsValid)
			{
				int tweetId = this._tweetRepo.AddNewTweet(tweet);
				this._hashRepo.AddNewTweetHash(hash, tweetId);
			}
			else
			{
				errorList = new List<ErrorDTO>();
				ErrorDTO errorDTO = new ErrorDTO();
				foreach (var item in validationResult.Errors)
				{
					errorDTO.ErrorMessage = item.ErrorMessage;
					errorDTO.PropertyName = item.PropertyName;
					errorList.Add(errorDTO);
				}
			}

			return errorList;

		}


		public bool deleteTweet(int tweetId)
		{
			this._tweetRepo.deleteTweet(tweetId);
			List<TweetHashMapper> tweetHashMappers = this._tweetHashRepo.updateTweetHashMapper(tweetId);
			return this._hashRepo.updateHash(tweetHashMappers, tweetId);

		}

		public string GetMostTweetingUser()
		{
			return this._tweetRepo.GetMostTweetingUser();
		}

		public int GetTodayTweetsCount()
		{
			return this._tweetRepo.GetTodayTweetsCount();
		}

		public TweetDTO GetMostLikedTweets()
		{
			return this._tweetRepo.GetMostLikedTweet();
		}
		public bool DislikeTweet(int userId, int tweetId)
		{
			return this._tweetRepo.DislikeTweet(userId, tweetId);
		}
		public bool LikeTweet(int userId, int tweetId)
		{
			return this._tweetRepo.LikeTweet(userId, tweetId);
		}
		public void editTweet(int tweetId, string Content)
		{
			//manipulations

			List<string> hash = new List<string>();
			for (int i = 0; i < Content.Length; i++)
			{
				if (Content[i] == '#')
				{
					string temp = "";
					int j;
					for (j = i + 1; j < Content.Length; j++)
					{
						if (Content[j] == ' ')
							break;
						temp = temp + Content[j];
					}
					hash.Add(temp);
					i = j;
				}
			}
			//this._tweetRepo.addhash()
			string oldcontent = this._tweetRepo.gettweet(tweetId);
			List<TweetHashMapper> tweetHashMappers = this._tweetHashRepo.updateTweetHashMapper(tweetId);
			this._hashRepo.updateHashforedit(tweetHashMappers, tweetId);
			this._tweetRepo.updateTweet(tweetId, Content);
			this._hashRepo.addhash(hash, tweetId);
			
			

		}
	}
}
