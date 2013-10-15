using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;
using System.Drawing;

namespace TwitterBot
{
	public class TweetInfoController : UIViewController, IImageUpdated
	{
		private UIImageView _userAvatar;
		private UILabel _userName;
		private UILabel _viaWeb;
		private UITextView _userTweetText;
		private UIImageView _separateLineImage;
		private UILabel _userTweetPostTime;
		private UILabel _userTweetShortUrl;

		private string _url;

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

			View.BackgroundColor = UIColor.White;

			_userAvatar.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                   topPadding,
			                                                   avatarWidth,
			                                                   avatarHeight);

			_userName.Frame = new System.Drawing.RectangleF (2 * leftPadding + avatarWidth,
			                                                 topPadding + avatarHeight / 3,
			                                                 View.Frame.Width - rightPadding - 2 * leftPadding - avatarWidth,
			                                                 avatarHeight / 3);

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
			_userTweetText.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;

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
			_url = t.UserAvatarUrl.ToString ();
			_userName.Text = t.UserName;
			_userTweetText.Text = t.TweetText;
			_userTweetPostTime.Text = t.PostTweetTime.ToShortDateString ();
			_userTweetShortUrl.Text = "http://bit.ly/X2IfBq";

			SetFrames ();

			var img = ImageLoader.DefaultRequestImage (t.UserAvatarUrl, this);
			if (img != null)
				_userAvatar.Image = img;
			else
				_userAvatar.Image = UIImage.FromBundle ("Content/Main/avatar.png");
		}

		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation (toInterfaceOrientation, duration);

			SetFrames ();
		}

		public void UpdatedImage (Uri uri)
		{
			if (_url == uri.ToString ())
				_userAvatar.Image = ImageLoader.DefaultRequestImage (uri, null);
		}

		private void SetFrames ()
		{
			float leftPadding = 18f;
			float rightPadding = 18f;
			float topPadding = 10f;
			float timeAndUrlHeight = 20;

			_userTweetText.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                      _userAvatar.Frame.Bottom + topPadding,
			                                                      View.Frame.Width - leftPadding - rightPadding,
			                                                      _userTweetText.ContentSize.Height);
			_userTweetText.SizeToFit ();



			_separateLineImage.Frame = new RectangleF (leftPadding,
			                                           _userTweetText.Frame.Bottom + topPadding,
			                                           _separateLineImage.Image.Size.Width,
			                                           _separateLineImage.Image.Size.Height);

			_userTweetPostTime.Frame = new RectangleF (leftPadding,
			                                           _separateLineImage.Frame.Bottom + topPadding,
			                                           _separateLineImage.Frame.Width / 3,
			                                           timeAndUrlHeight);

			_userTweetShortUrl.Frame = new RectangleF (leftPadding + _userTweetPostTime.Frame.Width + leftPadding,
			                                           _separateLineImage.Frame.Bottom + topPadding,
			                                           _separateLineImage.Frame.Width,
			                                           timeAndUrlHeight);
		}
	}
}

