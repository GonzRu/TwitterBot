using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class MainController : UINavigationController
	{
		private UITabBarController _rootTabBar;
		private UIViewController _tab1, _tab2, _tab3, _tab4;
		private AboutController _aboutController;

		public MainController ()
		{
			_aboutController = new AboutController ();
			_rootTabBar = new UITabBarController ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_tab1 = new TweetsController ();
			_tab1.TabBarItem = new UITabBarItem (UITabBarSystemItem.History, 0);
			_tab2 = new TweetsController ();
			_tab2.TabBarItem = new UITabBarItem (UITabBarSystemItem.Contacts, 0);
			_tab3 = new TweetsController ();
			_tab3.TabBarItem = new UITabBarItem (UITabBarSystemItem.Downloads, 0);
			_tab4 = new TweetsController ();
			_tab4.TabBarItem = new UITabBarItem (UITabBarSystemItem.Favorites, 0);

			_rootTabBar.ViewControllers = new UIViewController[] { _tab1, _tab2, _tab3, _tab4 };
		
			_rootTabBar.NavigationItem.RightBarButtonItem = new UIBarButtonItem ("Инфо", 
			                                                                     UIBarButtonItemStyle.Done, 
			                                                                     new EventHandler ((sender,e) => 
			                  {
				this.PushViewController(_aboutController, true);
			}));


			this.PushViewController (_rootTabBar, true);
		}
	}
}