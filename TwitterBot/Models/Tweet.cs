using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class Tweet
	{
		public string UserName { get; set; }

		public string TweetText { get; set; }

		public UIImage UserAvatar { get; set; }

		public DateTime PostTweetTime { get; set; }

		public Uri UserAvatarUrl { get; set; }

		public string ShortUrl { get; set; }
	}
}