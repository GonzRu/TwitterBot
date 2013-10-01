using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using TwitterBot;

public class TweetsTableViewSource : UITableViewSource
{
	private const string TWEET_ID = "Tweet";
	private const string MORE_TWEETS_ID = "MoreTweets";

	private TweetsController _root;
	private TweetInfoController _tweetInfo;

	private TweetsDownloader _tweetsDownloader;		
	private List<Tweet> _tweetsList;

	public TweetsTableViewSource(string hashTag, TweetsController root)
	{
		_tweetsDownloader = new TweetsDownloader(hashTag);
		_tweetInfo = new TweetInfoController();
		_root = root;

		_tweetsList = _tweetsDownloader.GetNextNTweets(10);
	}

	public override int RowsInSection (UITableView tableview, int section)
	{
		return _tweetsList.Count + 1;
	}

	public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		int row = indexPath.Row;
		UITableViewCell cell;

		if (row == _tweetsList.Count) {
			cell = new UITableViewCell (UITableViewCellStyle.Subtitle, MORE_TWEETS_ID);
			cell.TextLabel.Text = "Показать ещё";
		} else {
			cell = tableView.DequeueReusableCell (TWEET_ID);

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, TWEET_ID);

			Tweet tweet = _tweetsList [row];

			cell.TextLabel.Text = tweet.UserName;
			cell.DetailTextLabel.Text = tweet.TweetText;
			cell.ImageView.Image = tweet.UserAvatar;
		}

		return cell;
	}

	public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		int row = indexPath.Row;

		if (row != _tweetsList.Count) {
			_tweetInfo.ShowNewTweetInfo (_tweetsList [indexPath.Row]);
			_root.TabBarController.NavigationController.PushViewController (_tweetInfo, true);
		}
		else {
			_tweetsList.AddRange (_tweetsDownloader.GetNextNTweets (10));
			tableView.ReloadData ();
		}
	}


}