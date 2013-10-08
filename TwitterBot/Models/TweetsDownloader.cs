using System;
using System.Collections.Generic;
using Xamarin.Auth;
using Xamarin.Social;
using Xamarin.Social.Services;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TwitterBot
{
	public class TweetsDownloader
	{
		private string _hashtag;
		private Twitter5Service _twitter;
		private Account _acc;

		private string _maxId = null;

		public TweetsDownloader (string hastag)
		{
			_hashtag = hastag;

			_twitter = new Twitter5Service ();

			var accTask = _twitter.GetAccountsAsync ();
			accTask.Wait ();
			var accounts = accTask.Result;
			_acc = accounts.First ();
		}

		public List<Tweet> GetNextNTweets (int countOfTweets)
		{
			var req = _twitter.CreateRequest ("GET", new Uri (GetUrlRequest(countOfTweets)), _acc);

			var respTask = req.GetResponseAsync ();
			respTask.Wait ();
			var resp = respTask.Result.GetResponseText();

			return ParseJsonToTweetsList (resp);
		}

		async public System.Threading.Tasks.Task<List<Tweet>> GetNextNTweetsAsync(int countOfTweets)
		{
			var req = _twitter.CreateRequest ("GET", new Uri (GetUrlRequest(countOfTweets)), _acc);

			var r = await req.GetResponseAsync ();
			var resp = r.GetResponseText();

			return ParseJsonToTweetsList (resp);
		}

		private string GetUrlRequest(int countOfTweets)
		{
			string hastTag = _hashtag.Substring (1);

			if (String.IsNullOrEmpty (_maxId))
				return "https://api.twitter.com/1.1/search/tweets.json?q=@" + hastTag + "&count=" + countOfTweets.ToString ();
			else
				return "https://api.twitter.com/1.1/search/tweets.json" + _maxId + "&count=" + countOfTweets.ToString ();
		}

		private List<Tweet> ParseJsonToTweetsList(string jsonStr)
		{
			List<Tweet> list = new List<Tweet> ();

			try
			{
				JObject ob = JObject.Parse (jsonStr);

				JArray o = (JArray)ob["statuses"];

				foreach (var token in o) {
					Tweet t = new Tweet ();

					t.UserName = (string)token.SelectToken ("user").SelectToken ("name");
					t.TweetText = (string)token.SelectToken ("text");
					t.PostTweetTime = DateTime.ParseExact ((string)token.SelectToken ("created_at"), "ddd MMM dd HH:mm:ss zzz yyyy", System.Globalization.CultureInfo.InvariantCulture);
					t.UserAvatarUrl = new Uri ((string)token.SelectToken ("user").SelectToken ("profile_image_url"));

					list.Add (t);
				}

				_maxId = (string)ob["search_metadata"]["next_results"];
			}
			catch {
				throw new JsonReaderException ("Error parse id_str");
			}

			return list;
		}
	}
}

