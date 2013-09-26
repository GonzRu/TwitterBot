using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class MainController : UINavigationController
	{
		private TwitterTabBarController _rootTabBar;

		public MainController ()
		{
			_rootTabBar = new TwitterTabBarController ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.PushViewController (_rootTabBar, true);
		}
	}
}