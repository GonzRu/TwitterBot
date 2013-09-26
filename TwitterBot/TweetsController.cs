using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

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
		}
	}
}

