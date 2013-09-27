using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TweetInfoController : UIViewController
	{
		private UIImageView _userAvatar;
		private UILabel _userNameAndSName;
		private UILabel _tweetText;

		public TweetInfoController ()
		{
			_userNameAndSName = new UILabel ();
			_tweetText = new UILabel ();
			_userAvatar = new UIImageView ();

			_userAvatar.Frame = new System.Drawing.RectangleF (30, 50, 75, 95);
			_userNameAndSName.Frame = new System.Drawing.RectangleF (110, 50, 200, 80);
			_tweetText.Frame = new System.Drawing.RectangleF (30, 120, 300, 300);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			View.AddSubview (_userAvatar);
			View.AddSubview (_userNameAndSName);
			View.AddSubview (_tweetText);
		}

		public void ShowNewTweetInfo(Tweet t)
		{
			_userNameAndSName.Text = t.UserName;
			_tweetText.Text = t.TweetText;

			if (t.UserAvatar != null)
				_userAvatar = new UIImageView (t.UserAvatar);
			else
				_userAvatar = new UIImageView (new UIImage("Content/Main/avatar.png"));
		}
	}
}

