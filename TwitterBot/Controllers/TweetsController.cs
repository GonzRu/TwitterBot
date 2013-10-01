using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class TweetsController : UITableViewController
	{
		private string _hashTag;

		public TweetsController (string hashTag)
		{
			_hashTag = hashTag;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.Source = new TweetsTableViewSource (_hashTag, this);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			TabBarController.Title = _hashTag;
		}
	}
}

