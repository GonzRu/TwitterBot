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
	public List<Tweet> TweetsList { get; set; }
	
	public event Action TweetDownloadStarted;
	public event Action TweetDownloadEnded;	
	public event Action NetworkConnectionError;
	public event Action JsonParseError;
	public event Action<int> TableCellSelected;
	
	public TweetsTableViewSource(string hashTag, TweetsController root)
	{
		_tweetsDownloader = new TweetsDownloader(hashTag);
	}

	public override int RowsInSection (UITableView tableview, int section)
	{
		if (TweetsList == null)
			return 0;
		return TweetsList.Count + 1;
	}

	public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		int row = indexPath.Row;
		UITableViewCell cell;

		if (row == TweetsList.Count) {
			cell = tableView.DequeueReusableCell (MORE_TWEETS_ID);

			if (cell == null)
				cell = new MoreTweetsTableViewCell (MORE_TWEETS_ID);
		} else {
			cell = tableView.DequeueReusableCell (TWEET_ID);

			if (cell == null)
				cell = new TweetTableViewCell (TWEET_ID);

			(cell as TweetTableViewCell).UpdateCell (TweetsList [row]);
		}

		return cell;
	}

	public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		OnTableCellSelected (indexPath.Row);
	}

	public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
	{
		if (indexPath.Row != TweetsList.Count)
			return 44;
		else
			return 70;
	}

	public async void LoadData ()
	{
		OnTweetDownloadStarted ();

		try
		{
			var l = await _tweetsDownloader.GetNextNTweetsAsync (COUNT_OF_TWEETS_TO_DOWNLOAD);
			if (TweetsList == null)
				TweetsList = l;
			else
				TweetsList.AddRange (l);
		}
		catch (Newtonsoft.Json.JsonReaderException) {
			OnJsonParseError ();
			if (TweetsList == null)
				TweetsList = new List<Tweet> ();
		}
		catch (Exception) {
			OnNetworkConnectionError ();
			if (TweetsList == null)
				TweetsList = new List<Tweet> ();
		}

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

	protected virtual void OnNetworkConnectionError ()
	{
		if (NetworkConnectionError != null)
			NetworkConnectionError ();
	}

	protected virtual void OnJsonParseError ()
	{
		if (JsonParseError != null)
			JsonParseError ();
	}

	protected virtual void OnTableCellSelected (int row)
	{
		if (TableCellSelected != null)
			TableCellSelected (row);
	}
}