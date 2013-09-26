using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TweetsDownloader
	{
		private string _hashtag;

		public TweetsDownloader (string hastag)
		{
			_hashtag = hastag;
		}

		public List<Tweet> GetNextNTweets (int countOfTweets)
		{
			return new List<Tweet> {
				new Tweet("1111111111111111", "sdlkjfsldkjflksdjflksdjfkls", UIImage.FromFile("Content/Main/avatar.png")),
				new Tweet("2222222222222222", "fsdfsdfsdfsdfsdfsdfsdfdsfsdf", UIImage.FromFile("Content/Main/avatar.png")),
				new Tweet("3333333333333", "fsdfsdfsdsfdfsdfsdfdfsdfsdfsdfsdfdsfsdf", UIImage.FromFile("Content/Main/avatar.png")),
				new Tweet("4444444444", "fsdfsdf444444sdfsdfsdfsdfsdfdsfsdf", UIImage.FromFile("Content/Main/avatar.png"))
			};
		}
	}
}

