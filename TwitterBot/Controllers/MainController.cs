using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class MainController : UINavigationController
	{
		private TwitterTabBarController _rootTabBar;

		public MainController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_rootTabBar = new TwitterTabBarController ();
			this.PushViewController (_rootTabBar, false);
		}
	}
}