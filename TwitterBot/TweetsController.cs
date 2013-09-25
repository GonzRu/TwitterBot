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

			TableView.Source = new TweetsTableViewSource (_hashTag, this);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			this.TabBarController.Title = _hashTag;
		}

		public class TweetsTableViewSource : UITableViewSource
		{
			private const string TweetId = "Tweet";
			private List<Tweet> _tweetsList;
			private TweetsController _root;

			public TweetsTableViewSource(string hashTag, TweetsController root)
			{
				_tweetsList = new List<Tweet> 
				{
					new Tweet("111", "sadasdasdddddddddddddddddddddddddddddddddddddddddddddfdddddddddddddddddddddsdasddasd", UIImage.FromFile ("Main/avatar.png")), 
					new Tweet("222", "dfsdfsdfooowowowowooeiocksdkcdmckdvjfdlkajdflkajsdflkajsdflkjsdflkajsd;lfkjasd;lkfjaslkd", UIImage.FromFile ("Main/avatar.png"))
				};
				_root = root;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return _tweetsList.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				int row = indexPath.Row;

				UITableViewCell cell = tableView.DequeueReusableCell (TweetId);

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, TweetId);

				Tweet tweet = _tweetsList [row];

				cell.TextLabel.Text = tweet.UserName;
				cell.DetailTextLabel.Text = tweet.TweetText;
				cell.ImageView.Image = tweet.UserAvatar;

				return cell;
			}

			public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				//new UIAlertView ("Нажатие на твит", indexPath.Row.ToString(), null, "ok", null).Show ();
				_root.TabBarController.NavigationController.PushViewController (new TweetInfoController (_tweetsList [indexPath.Row]), true);
			}
		}
	}
}

