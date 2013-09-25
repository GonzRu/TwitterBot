using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class Tweet
	{
		public Tweet (string userName, string tweetText, UIImage userAvatar)
		{
			UserName = userName;
			TweetText = tweetText;
			UserAvatar = userAvatar;
		}

		public string UserName { get; set; }

		public string TweetText { get; set; }

		public UIImage UserAvatar { get; set; }

		public DateTime PostTweetTime { get; set; }
	}
}