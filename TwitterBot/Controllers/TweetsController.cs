using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace TwitterBot
{
	public class TweetsController : UITableViewController
	{
		private string _hashTag;

		private LoadingAlertView _loadingAlertView;
		private TweetInfoController _tweetInfoController;
		private UIRefreshControl _refreshControl;

		public TweetsController (string hashTag)
		{
			_hashTag = hashTag;

			_loadingAlertView = new LoadingAlertView (View.Frame);
			_tweetInfoController = new TweetInfoController ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;

			_refreshControl = new UIRefreshControl ();
			_refreshControl.ValueChanged += UpdateStarted;
			RefreshControl = _refreshControl;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			TabBarController.Title = _hashTag;

			if (!UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) {
				var f = TableView.Superview.Frame;
				if (f.Y != 0) {
					f.Height += f.Y;
					f.Y = 0;
					TableView.Superview.Frame = f;
				}
			}

			if (TableView.Source == null) {
				var _source = new TweetsTableViewSource (_hashTag, this);
				_source.TweetDownloadStarted += TweetsDonwloadStarted;
				_source.TweetDownloadEnded += TweetsDownloadEnded;
				_source.TableCellSelected += TableCellSelected;
				_source.NetworkConnectionError += NetworkConnectionError;
				_source.JsonParseError += JsonParseError;
				_source.LoadData ();
				TableView.Source = _source;
			}
		}

		private void TweetsDonwloadStarted (bool isRefresh)
		{
			if (!isRefresh) {
				View.AddSubview (_loadingAlertView);
				_loadingAlertView.Show ();
			}
		}

		private void TweetsDownloadEnded (bool isRefresh)
		{
			if (!isRefresh) {
				_loadingAlertView.DismissWithClickedButtonIndex (0, true);
				_loadingAlertView.RemoveFromSuperview ();
			} else
				_refreshControl.EndRefreshing ();

			TableView.ReloadData ();
		}

		private void NetworkConnectionError ()
		{
			new UIAlertView ("Ошибка", "Нет подключения к интернету", null, "ok", null).Show ();
		}

		private void JsonParseError ()
		{
			new UIAlertView ("Ошибка", "Ошибка входящих данных", null, "ok", null).Show ();
		}

		private void TableCellSelected (int row)
		{
			var source = TableView.Source as TweetsTableViewSource;

			if (row != source.TweetsList.Count) {
				_tweetInfoController.ShowNewTweetInfo (source.TweetsList [row]);
				this.TabBarController.NavigationController.PushViewController (_tweetInfoController, true);
			} else
				source.LoadData ();
		}

		private void UpdateStarted (object sender, EventArgs e)
		{
			var source = TableView.Source as TweetsTableViewSource;

			source.RefreshData ();
		}
	}
}

