using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.MessageUI;

namespace TwitterBot
{
	public class AboutController : UIViewController
	{
		private const string ABOUT_COMPANY_TEXT = "\nНам не стыдно за выпускаемые продукты, все они сделаны с вниманием к деталям. Пользователи это ценят, многие наши приложения попадают в топы AppStore и получают высокие оценки. \n\nМы любим своих заказчиков и решаем их задачи. На письма и телефон отвечаем быстро, по праздникам и выходным, делаем работу в срок и никуда не пропадаем.\nЗакажите разработку сейчас! ";
		private const string TELEPHONE = "777-77-77";
		private const string EMAIL = "123@mail.ru";

		private const int BUTTON_WIDTH = 70;
		private const int BUTTON_HEIGHT = 20;

		private UIImageView _logo;
		private UITextView _aboutTextView;
		private UIButton _btnCall;
		private UIButton _btnEmail;

		public AboutController ()
		{
			_logo = new UIImageView (new UIImage("Content/logo.png"));
			_aboutTextView = new UITextView ();
			_btnCall = new UIButton ();
			_btnEmail = new UIButton ();

			_aboutTextView.Text = ABOUT_COMPANY_TEXT;
			_aboutTextView.Editable = false;
			_aboutTextView.ScrollEnabled = false;
			_btnCall.SetTitle ("Phone", UIControlState.Normal);
			_btnEmail.SetTitle ("Mail", UIControlState.Normal);

			_logo.Frame = new System.Drawing.RectangleF (View.Center.X - 90, 10, 180, 63);
			_aboutTextView.Frame = new System.Drawing.RectangleF (10, 80, View.Bounds.Width - 20, 180);
			_btnCall.Frame = new System.Drawing.RectangleF (10, View.Bounds.Height - BUTTON_HEIGHT - 20, BUTTON_WIDTH, BUTTON_HEIGHT);
			_btnEmail.Frame = new System.Drawing.RectangleF (View.Bounds.Width - BUTTON_WIDTH - 10, View.Bounds.Height - BUTTON_HEIGHT - 20, BUTTON_WIDTH, BUTTON_HEIGHT);

			_logo.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			_aboutTextView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			_btnCall.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
			_btnEmail.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleLeftMargin;

			_btnCall.BackgroundColor = UIColor.Gray;
			_btnEmail.BackgroundColor = UIColor.Gray;

			_btnCall.TouchUpInside += CallTelephoneNumberButtonPushed;
			_btnEmail.TouchUpInside += SendEmailButtonPushed;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			View.AddSubview (_logo);
			View.AddSubview (_aboutTextView);
			View.AddSubview (_btnCall);
			View.AddSubview (_btnEmail);
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

