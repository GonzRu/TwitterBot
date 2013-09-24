using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class MainController : UIViewController
	{
		public MainController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;
		}
	}
}

