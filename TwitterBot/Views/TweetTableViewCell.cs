using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TweetTableViewCell : UITableViewCell
	{
		private UILabel _UserNameLabel;
		private UILabel _UserTweetTextLabel;
		private UILabel _UserTweetPostTimeLabel;
		private UIImageView _UserAvatarView;

		public TweetTableViewCell (Tweet tweet, string reuseIdentifier)
			: base(UITableViewCellStyle.Subtitle, reuseIdentifier)
		{
			_UserNameLabel = new UILabel ();
			_UserTweetTextLabel = new UILabel ();
			_UserTweetPostTimeLabel = new UILabel ();
			_UserAvatarView = new UIImageView ();

			_UserNameLabel.Text = tweet.UserName;
			_UserTweetTextLabel.Text = tweet.TweetText;
			_UserTweetPostTimeLabel.Text = (DateTime.Now - tweet.PostTweetTime).Days.ToString () + "ч";
			_UserAvatarView.Image = UIImage.FromFile ("Content/Main/avatar.png");


			_UserNameLabel.Font = UIFont.FromName ("HelveticaNeue-Bold", 17f);
			_UserTweetPostTimeLabel.Font = UIFont.FromName ("HelveticaNeue", 11f);
			_UserTweetTextLabel.Font = UIFont.FromName ("HelveticaNeue", 13f);


			_UserNameLabel.TextColor = UIColor.Black;
			_UserTweetTextLabel.TextColor = UIColor.FromRGB (89, 89, 89);
			_UserTweetPostTimeLabel.TextColor = UIColor.FromRGB (89, 89, 89);

			this.ContentView.AddSubview (_UserNameLabel);
			this.ContentView.AddSubview (_UserTweetTextLabel);
			this.ContentView.AddSubview (_UserTweetPostTimeLabel);
			this.ContentView.AddSubview (_UserAvatarView);
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
			float postDayWidth = 20.0f;

			_UserAvatarView.Frame = new System.Drawing.RectangleF (b.Left, b.Top, iconWidth, iconHeight);
			_UserNameLabel.Frame = new System.Drawing.RectangleF (b.Left + iconWidth + leftPadding, 
			                                                      b.Top, 
			                                                      b.Width - iconWidth - totalPadding - postDayWidth,
			                                                      b.Height / 2);
			_UserTweetTextLabel.Frame = new System.Drawing.RectangleF (b.Left + iconWidth + leftPadding,
			                                                               b.Height / 2,
			                                                               b.Width - iconWidth - totalPadding,
			                                                               b.Height / 2);
			_UserTweetPostTimeLabel.Frame = new System.Drawing.RectangleF (b.Width - postDayWidth - rightPadding,
			                                                               b.Top,
			                                                               postDayWidth,
			                                                               b.Height / 2);
		}
	}
}

