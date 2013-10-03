using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;

namespace TwitterBot
{
	public class TweetInfoController : UIViewController
	{
		private UIImageView _userAvatar;
		private UILabel _userName;
		private UILabel _viaWeb;
		private UITextView _userTweetText;
		private UIImageView _separateLineImage;
		private UILabel _userTweetPostTime;
		private UILabel _userTweetShortUrl;


		public TweetInfoController ()
		{
			_userAvatar = new UIImageView ();
			_userName = new UILabel ();
			_viaWeb = new UILabel ();
			_userTweetText = new UITextView ();
			_separateLineImage = new UIImageView (UIImage.FromFile ("Content/Tweets/line.png"));
			_userTweetPostTime = new UILabel ();
			_userTweetShortUrl = new UILabel ();

			float leftPadding = 18f;
			float rightPadding = 18f;
			float topPadding = 25f;
			float avatarWidth = 64f;
			float avatarHeight = 64f;
			float textHeight = 100f;

			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile("Content/Tweets/bg.png"));

			_userAvatar.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                   topPadding,
			                                                   avatarWidth,
			                                                   avatarHeight);
			_userName.Frame = new System.Drawing.RectangleF (2 * leftPadding + avatarWidth,
			                                                 topPadding + avatarHeight / 3,
			                                                 View.Frame.Width - rightPadding - 2 * leftPadding - avatarWidth,
			                                                 avatarHeight / 3);
			_userTweetText.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                      2 * topPadding + avatarHeight,
			                                                      View.Frame.Width - leftPadding - rightPadding,
			                                                      textHeight);
			_viaWeb.Frame = new System.Drawing.RectangleF (2 * leftPadding + avatarWidth,
			                                               topPadding + (4 * avatarHeight) / 5,
			                                               50,
			                                               avatarHeight / 5);


			_userName.Font = UIFont.FromName ("HelveticaNeue-Bold", 16f);
			_userTweetText.Font = UIFont.FromName ("HelveticaNeue", 12f);
			_userTweetPostTime.Font = UIFont.FromName ("HelveticaNeue", 10f);
			_userTweetShortUrl.Font = UIFont.FromName ("HelveticaNeue", 10f);
			_viaWeb.Font = UIFont.FromName ("HelveticaNeue-Bold", 12f);

			_userName.TextColor = UIColor.FromRGB (68, 100, 143);
			_viaWeb.TextColor = UIColor.FromRGB (41, 41, 41);
			_userTweetText.TextColor = UIColor.FromRGB (41, 41, 41);
			_userTweetPostTime.TextColor = UIColor.FromRGB (77, 77, 77);
			_userTweetShortUrl.TextColor = UIColor.FromRGB (77, 77, 77);

			_userName.BackgroundColor = UIColor.Clear;
			_userTweetText.BackgroundColor = UIColor.Clear;
			_userAvatar.BackgroundColor = UIColor.Clear;
			_viaWeb.BackgroundColor = UIColor.Clear;
			_userTweetPostTime.BackgroundColor = UIColor.Clear;
			_userTweetShortUrl.BackgroundColor = UIColor.Clear;

			Title = "Твит";
			_viaWeb.Text = "via Web";

			_userTweetText.Editable = false;
			_userTweetText.ScrollEnabled = false;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.AddSubview (_userAvatar);
			View.AddSubview (_userName);
			View.AddSubview (_viaWeb);
			View.AddSubview (_userTweetText);
			View.AddSubview (_separateLineImage);
			View.AddSubview (_userTweetPostTime);
			View.AddSubview (_userTweetShortUrl);
		}

		public void ShowNewTweetInfo(Tweet t)
		{
			_userName.Text = t.UserName;
			_userTweetText.Text = t.TweetText;
			_userTweetPostTime.Text = t.PostTweetTime.ToShortDateString ();
			_userTweetShortUrl.Text = "http://bit.ly/X2IfBq";

			var frame = _userTweetText.Frame;
			frame.Height = _userTweetText.ContentSize.Height;
			_userTweetText.Frame = frame;

			float leftPadding = 18f;
			float timeAndUrlHeight = 20;

			_separateLineImage.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                          _userTweetText.Frame.Bottom + 20f,
			                                                          _separateLineImage.Image.Size.Width,
			                                                          _separateLineImage.Image.Size.Height);
			_userTweetPostTime.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                          _separateLineImage.Frame.Bottom + 10f,
			                                                          _separateLineImage.Frame.Width / 3,
			                                                          timeAndUrlHeight);
			_userTweetShortUrl.Frame = new System.Drawing.RectangleF (leftPadding + _userTweetPostTime.Frame.Width + leftPadding,
			                                                          _separateLineImage.Frame.Bottom + 10f,
			                                                          _separateLineImage.Frame.Width,
			                                                          timeAndUrlHeight);

			_userAvatar.Image = ImageLoader.DefaultRequestImage (t.UserAvatarUrl, null);
		}
	}
}

