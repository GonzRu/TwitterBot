using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using TwitterBot;

public class TweetsTableViewSource : UITableViewSource
{
	private const string TWEET_ID = "Tweet";
	private const string MORE_TWEETS_ID = "More Tweets";
	private const int COUNT_OF_TWEETS_TO_DOWNLOAD = 7;

	private TweetsDownloader _tweetsDownloader;		
	private List<Tweet> _tweetsList;
	
	public event Action TweetDownloadStarted;
	public event Action TweetDownloadEnded;	
	public event Action<Tweet> TableCellSelected;
	
	public TweetsTableViewSource(string hashTag, TweetsController root)
	{
		_tweetsDownloader = new TweetsDownloader(hashTag);
	}

	public override int RowsInSection (UITableView tableview, int section)
	{
		if (_tweetsList == null)
			return 0;
		return _tweetsList.Count + 1;
	}

	public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		int row = indexPath.Row;
		UITableViewCell cell;

		if (row == _tweetsList.Count) {
			cell = tableView.DequeueReusableCell (MORE_TWEETS_ID);

			if (cell == null)
				cell = new MoreTweetsTableViewCell (MORE_TWEETS_ID);
		} else {
			cell = tableView.DequeueReusableCell (TWEET_ID);

			if (cell == null)
				cell = new TweetTableViewCell (TWEET_ID);

			(cell as TweetTableViewCell).UpdateCell (_tweetsList [row]);
		}

		return cell;
	}

	public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		int row = indexPath.Row;

		if (row != _tweetsList.Count)
			OnTableCellSelected (_tweetsList [row]);
		else
			LoadData ();
	}

	public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		if (indexPath.Row != _tweetsList.Count)
			return 44;
		else
			return 70;
	}

	public async void LoadData ()
	{
		OnTweetDownloadStarted ();
		var l = await _tweetsDownloader.GetNextNTweetsAsync (COUNT_OF_TWEETS_TO_DOWNLOAD);
		if (_tweetsList == null)
			_tweetsList = l;
		else
			_tweetsList.AddRange (l);
		OnTweetDownloadEnded ();
	}

	protected virtual void OnTweetDownloadStarted ()
	{
		if (TweetDownloadStarted != null)
			TweetDownloadStarted ();
	}

	protected virtual void OnTweetDownloadEnded ()
	{
		if (TweetDownloadEnded != null)
			TweetDownloadEnded ();
	}
	protected virtual void OnTableCellSelected (Tweet t)
	{
		if (TableCellSelected != null)
			TableCellSelected (t);
	}
}