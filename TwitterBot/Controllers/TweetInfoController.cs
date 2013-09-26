using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TweetInfoController : UIViewController
	{
		private Tweet _tweet;

		private UIImageView _userAvatar;
		private UILabel _userNameAndSName;
		private UILabel _tweetText;

		public TweetInfoController (Tweet tweet)
		{
			_tweet = tweet;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			_userAvatar = new UIImageView (_tweet.UserAvatar);
			_userNameAndSName = new UILabel ();
			_tweetText = new UILabel ();

			_userAvatar.Frame = new System.Drawing.RectangleF (30, 50, 75, 95);
			_userNameAndSName.Frame = new System.Drawing.RectangleF (110, 50, 200, 80);
			_tweetText.Frame = new System.Drawing.RectangleF (30, 120, 300, 300);

			_userNameAndSName.Text = _tweet.UserName;
			_tweetText.Text = _tweet.TweetText;

			View.AddSubview (_userAvatar);
			View.AddSubview (_userNameAndSName);
			View.AddSubview (_tweetText);
		}
	}
}

