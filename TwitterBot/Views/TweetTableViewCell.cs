using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;

namespace TwitterBot
{
	public class TweetTableViewCell : UITableViewCell, IImageUpdated
	{
		private UILabel _userNameLabel;
		private UILabel _userTweetTextLabel;
		private UILabel _userTweetPostTimeLabel;
		private UIImageView _userAvatarView;

		private Uri _userAvatarUri;

		public TweetTableViewCell (string reuseIdentifier)
			: base(UITableViewCellStyle.Subtitle, reuseIdentifier)
		{
			_userNameLabel = new UILabel ();
			_userTweetTextLabel = new UILabel ();
			_userTweetPostTimeLabel = new UILabel ();
			_userAvatarView = new UIImageView ();

			this.ContentView.AddSubview (_userNameLabel);
			this.ContentView.AddSubview (_userTweetTextLabel);
			this.ContentView.AddSubview (_userTweetPostTimeLabel);
			this.ContentView.AddSubview (_userAvatarView);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			System.Drawing.RectangleF b = ContentView.Bounds;

			float iconWidth = b.Height;
			float iconHeight = b.Height;
			float rightPadding = 10.0f;
			float leftPadding = 10.0f;
			float totalPadding = leftPadding + rightPadding;
			float postDayWidth = 30.0f;

			_userNameLabel.Font = UIFont.FromName ("HelveticaNeue-Bold", 17f);
			_userTweetPostTimeLabel.Font = UIFont.FromName ("HelveticaNeue", 11f);
			_userTweetTextLabel.Font = UIFont.FromName ("HelveticaNeue", 13f);

			_userTweetPostTimeLabel.TextAlignment = UITextAlignment.Right;

			_userNameLabel.TextColor = UIColor.Black;
			_userTweetTextLabel.TextColor = UIColor.FromRGB (89, 89, 89);
			_userTweetPostTimeLabel.TextColor = UIColor.FromRGB (89, 89, 89);

			_userAvatarView.Frame = new System.Drawing.RectangleF (b.Left,
			                                                       b.Top,
			                                                       iconWidth,
			                                                       iconHeight);
			_userNameLabel.Frame = new System.Drawing.RectangleF (b.Left + iconWidth + leftPadding, 
			                                                      b.Top, 
			                                                      b.Width - iconWidth - totalPadding - postDayWidth,
			                                                      b.Height / 2);
			_userTweetTextLabel.Frame = new System.Drawing.RectangleF (b.Left + iconWidth + leftPadding,
			                                                               b.Height / 2,
			                                                               b.Width - iconWidth - totalPadding,
			                                                               b.Height / 2);
			_userTweetPostTimeLabel.Frame = new System.Drawing.RectangleF (b.Width - postDayWidth - rightPadding,
			                                                               b.Top,
			                                                               postDayWidth,
			                                                               b.Height / 2);
		}

		public void UpdateCell (Tweet tweet)
		{
			_userAvatarUri = tweet.UserAvatarUrl;

			_userNameLabel.Text = tweet.UserName;
			_userTweetTextLabel.Text = tweet.TweetText;

			var d = DateTime.Now - tweet.PostTweetTime;
			if (d.Days != 0)
				_userTweetPostTimeLabel.Text = d.Days.ToString () + " д";
			else if (d.Hours != 0)
				_userTweetPostTimeLabel.Text = d.Hours.ToString () + " ч";
			else if (d.Minutes != 0)
				_userTweetPostTimeLabel.Text = d.Minutes.ToString () + " м";
			else
				_userTweetPostTimeLabel.Text = d.Seconds.ToString () + " с";

			var img = ImageLoader.DefaultRequestImage (tweet.UserAvatarUrl, this);

			if (img == null)
				img = UIImage.FromBundle ("Content/Main/avatar.png");
			else
				img = img.SetMask ("Content/Main/mask_avatar_mini.png");
			_userAvatarView.Image = img;
		}

		public void UpdatedImage (Uri uri)
		{
			if (_userAvatarUri == uri)
				_userAvatarView.Image = ImageLoader.DefaultRequestImage (uri, null).SetMask ("Content/Main/mask_avatar_mini.png");
		}
	}
}

