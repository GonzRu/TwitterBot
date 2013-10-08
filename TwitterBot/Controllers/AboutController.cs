using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.MessageUI;

namespace TwitterBot
{
	public class AboutController : UIViewController
	{
		private const string ABOUT_COMPANY_TEXT = "Нам не стыдно за выпускаемые продукты, все они сделаны с вниманием к деталям. Пользователи это ценят, многие наши приложения попадают в топы AppStore и получают высокие оценки. \n\nМы любим своих заказчиков и решаем их задачи. На письма и телефон отвечаем быстро, по праздникам и выходным, делаем работу в срок и никуда не пропадаем.\nЗакажите разработку сейчас! ";
		private const string TELEPHONE = "777-77-77";
		private const string EMAIL = "123@mail.ru";

		private const int BUTTON_WIDTH = 100;
		private const int BUTTON_HEIGHT = 54;

		private UIScrollView _scrollView;
		private UIImageView _logo;
		private UITextView _aboutTextView;
		private UIButton _btnCall;
		private UIButton _btnEmail;

		public AboutController ()
		{
			_scrollView = new UIScrollView ();
			_logo = new UIImageView (UIImage.FromFile ("Content/Info/logo.png"));
			_aboutTextView = new UITextView ();
			_btnCall = UIButton.FromType (UIButtonType.Custom);
			_btnEmail = UIButton.FromType (UIButtonType.Custom);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			var f = View.Frame;
			f.Y -= 20;

			// Scroll View
			_scrollView.BackgroundColor = UIColor.White;
			_scrollView.Frame = f;
			_scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			_scrollView.ScrollEnabled = true;
			View.AddSubview (_scrollView);

			// Company logo
			_logo.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			_scrollView.AddSubview (_logo);

			// About company text
			_aboutTextView.Text = ABOUT_COMPANY_TEXT;
			_aboutTextView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			_aboutTextView.Editable = false;
			_aboutTextView.ScrollEnabled = false;
			_scrollView.AddSubview (_aboutTextView);

			// Call button
			_btnCall.SetBackgroundImage (UIImage.FromFile ("Content/Info/button.png").StretchableImage (11, 0), UIControlState.Normal);
			_btnCall.SetBackgroundImage (UIImage.FromFile ("Content/Info/button_pressed.png").StretchableImage (11, 0), UIControlState.Highlighted);
			_btnCall.SetImage (UIImage.FromFile ("Content/Info/icon_phone.png"), UIControlState.Normal & UIControlState.Highlighted);
			_btnCall.ImageEdgeInsets = new UIEdgeInsets (0, 0, 10, 0);
			_btnCall.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			_btnCall.TouchUpInside += CallTelephoneNumberButtonPushed;
			_scrollView.AddSubview (_btnCall);

			// Email button
			_btnEmail.SetBackgroundImage (UIImage.FromFile ("Content/Info/button.png").StretchableImage (11, 0), UIControlState.Normal);
			_btnEmail.SetBackgroundImage (UIImage.FromFile ("Content/Info/button_pressed.png").StretchableImage (11, 0), UIControlState.Highlighted);
			_btnEmail.SetImage (UIImage.FromFile ("Content/Info/icon_mail.png"), UIControlState.Normal & UIControlState.Highlighted);
			_btnEmail.ImageEdgeInsets = new UIEdgeInsets (0, 0, 10, 0);
			_btnEmail.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			_btnEmail.TouchUpInside += SendEmailButtonPushed;
			_scrollView.AddSubview (_btnEmail);
		}

		public override void WillAnimateSecondHalfOfRotation (UIInterfaceOrientation fromInterfaceOrientation, double duration)
		{
			base.WillAnimateSecondHalfOfRotation (fromInterfaceOrientation, duration);

			SetFrames ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			SetFrames ();
		}

		private void SetFrames ()
		{
			float leftPadding = 10f;
			float rightPadding = 10f;
			float topPadding = 5f;

			_logo.Frame = new System.Drawing.RectangleF (View.Center.X - _logo.Image.Size.Width / 2,
			                                             topPadding,
			                                             _logo.Image.Size.Width,
			                                             _logo.Image.Size.Height);


			_aboutTextView.Frame = new System.Drawing.RectangleF (leftPadding,
			                                                      _logo.Frame.Bottom + topPadding,
			                                                      View.Bounds.Width - leftPadding - rightPadding,
			                                                      _aboutTextView.ContentSize.Height);

			_btnCall.Frame = new System.Drawing.RectangleF (View.Center.X - 3 * BUTTON_WIDTH / 2,
			                                                _logo.Frame.Bottom + topPadding + _aboutTextView.ContentSize.Height + topPadding,
			                                                BUTTON_WIDTH,
			                                                BUTTON_HEIGHT);

			_btnEmail.Frame = new System.Drawing.RectangleF (View.Center.X + BUTTON_WIDTH / 2,
			                                                 _logo.Frame.Bottom + topPadding + _aboutTextView.ContentSize.Height + topPadding,
			                                                 BUTTON_WIDTH,
			                                                 BUTTON_HEIGHT);

			if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Unknown)
				_scrollView.ContentSize = new System.Drawing.SizeF (View.Frame.Width, View.Frame.Height);
			else
				_scrollView.ContentSize = new System.Drawing.SizeF (View.Frame.Width, _btnCall.Frame.Bottom + 30);
		}

		private void CallTelephoneNumberButtonPushed (object sender, EventArgs e)
		{
			if (!UIApplication.SharedApplication.OpenUrl (new Uri("tel:" + TELEPHONE)))
				new UIAlertView("Error", "Cann't call", null, "Ok", null).Show();
		}

		private void SendEmailButtonPushed (object sender, EventArgs e)
		{
			if (MFMailComposeViewController.CanSendMail)
			{
				MFMailComposeViewController m = new MFMailComposeViewController();
				m.SetSubject("To Touch Instinct!");
				m.Finished += SendEmailFinished;
				PresentViewController(m, true, null);
			}
			else
				new UIAlertView("Error", "Cann't send email", null, "Ok", null).Show();
		}

		private void SendEmailFinished (Object o, MFComposeResultEventArgs e)
		{
			this.DismissViewController (true, null);
		}
	}
}

