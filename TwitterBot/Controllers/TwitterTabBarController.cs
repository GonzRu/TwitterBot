using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class TwitterTabBarController : UITabBarController
	{
		private string [] HASH_TAG_COLLECTION = {"#Twitter", "#Dribbble", "#Apple", "#Github"};

		private UIViewController _tab1, _tab2, _tab3, _tab4;
		private AboutController _aboutController;

		public TwitterTabBarController ()
		{
			_tab1 = new TweetsController (HASH_TAG_COLLECTION[0]);
			_tab1.TabBarItem = new UITabBarItem (HASH_TAG_COLLECTION[0], UIImage.FromFile ("Content/TabBar/icon_twitter.png"), 0);
			_tab2 = new TweetsController (HASH_TAG_COLLECTION[1]);
			_tab2.TabBarItem = new UITabBarItem (HASH_TAG_COLLECTION[1], UIImage.FromFile ("Content/TabBar/icon_dribbble.png"), 0);
			_tab3 = new TweetsController (HASH_TAG_COLLECTION[2]);
			_tab3.TabBarItem = new UITabBarItem (HASH_TAG_COLLECTION[2], UIImage.FromFile ("Content/TabBar/icon_apple.png"), 0);
			_tab4 = new TweetsController (HASH_TAG_COLLECTION[3]);
			_tab4.TabBarItem = new UITabBarItem (HASH_TAG_COLLECTION[3], UIImage.FromFile ("Content/TabBar/icon_github.png"), 0);

			this.ViewControllers = new UIViewController[] { _tab1, _tab2, _tab3, _tab4 };

			_aboutController = new AboutController ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (
				"Инфо", 
				UIBarButtonItemStyle.Done,
				AboutCompanyButtonPushed
			);
		}

		private void AboutCompanyButtonPushed (Object sender, EventArgs e)
		{
			this.NavigationController.PushViewController (_aboutController, true);
		}
	}
}

