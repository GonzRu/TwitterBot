using System;
using MonoTouch.UIKit;

namespace TwitterBot
{
	public class LoadingAlertView : UIAlertView {
		UIActivityIndicatorView activitySpinner;

		public LoadingAlertView (System.Drawing.RectangleF frame) : base (frame)
		{
			// configurable bits
			AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin;

			Title = "TwitterBot";
			Message = "Загрузка данных...";

			// derive the center x and y
			float centerX = Frame.Width / 2;
			float centerY = Frame.Height / 2;

			// create the activity spinner, center it horizontall and put it 5 points above center x
			activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new System.Drawing.RectangleF (
				centerX - (activitySpinner.Frame.Width / 2) ,
				centerY + Frame.Height / 4 - activitySpinner.Frame.Height / 2,
				activitySpinner.Frame.Width ,
				activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview (activitySpinner);
			activitySpinner.StartAnimating ();
		}

		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		/*
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 0; },
			() => { RemoveFromSuperview(); }
			);
		}*/
	}
}

