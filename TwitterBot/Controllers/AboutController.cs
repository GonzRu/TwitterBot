using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class AboutController : UIViewController
	{
		private const string _aboutText = "111111111111111111111111111111111111111111111111111111111111111111111111111";
		private const string _telNumber = "777-77-77";
		private const string _email = "123@mail.ru";

		private UIImageView _logo;
		private UILabel _aboutLabel;
		private UIButton _btnCall;
		private UIButton _btnEmail;

		public AboutController ()
		{
			_logo = new UIImageView ();
			_aboutLabel = new UILabel ();
			_btnCall = new UIButton ();
			_btnEmail = new UIButton ();

			_logo.Frame = new System.Drawing.RectangleF (50, 10, 100, 60);
			_aboutLabel.Frame = new System.Drawing.RectangleF (10, 80, 170, 150);
			_btnCall.Frame = new System.Drawing.RectangleF (30, 200, 60, 240);
			_btnEmail.Frame = new System.Drawing.RectangleF (90, 200, 120, 240);

			_aboutLabel.Text = _aboutText;
			_btnCall.SetTitle ("Phone", UIControlState.Normal);
			_btnEmail.SetTitle ("Mail", UIControlState.Normal);

			_btnCall.BackgroundColor = UIColor.Gray;
			_btnEmail.BackgroundColor = UIColor.Gray;

			_btnCall.TouchUpInside += (object sender, EventArgs e) => 
			{
				UIApplication.SharedApplication.OpenUrl (new Uri("tel:" + _telNumber));
			};

			_btnEmail.TouchUpInside += (object sender, EventArgs e) => 
			{
				//MFMailComposeViewController _mailController;
			};
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			View.AddSubview (_logo);
			View.AddSubview (_aboutLabel);
			View.AddSubview (_btnCall);
			View.AddSubview (_btnEmail);

			//this.NavigationController.PushViewController (new UIViewController (), true);
		}


	}
}

